using System;
using System.ComponentModel;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Text.Method;
using Android.Widget;
using INetApp.Droid.Services;
using INetApp;
using INetApp.Extensions;
using INetApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using EntryEffectForms = INetApp.Effects.EntryEffect;

[assembly: Xamarin.Forms.ExportEffect(typeof(INetApp.Droid.Effects.EntryEffect), "EntryEffect")]
namespace INetApp.Droid.Effects
{
    /// <summary>
    /// Entry effect.
    /// </summary>
    [Android.Runtime.Preserve(AllMembers = true)]
    public class EntryEffect : PlatformEffect
    {
        private Drawable originalBackground;
        private View viewNextFocus;

        /// <summary>
        /// Ons the attached.
        /// </summary>
        protected override void OnAttached()
        {
            if (originalBackground == null)
                originalBackground = Control?.Background;

            this.SetBorder((bool)Element.GetValue(EntryEffectForms.HasBorderProperty));
            this.SetNextControl((View)Element.GetValue(EntryEffectForms.NextControlFocusProperty));
            this.setSelectedTextOnFocus((bool)Element.GetValue(EntryEffectForms.SelectedTextOnFocusProperty));
            this.SetRightImage(EntryEffectForms.GetRightImage(Element));
            this.SetLeftImage(EntryEffectForms.GetLeftImage(Element));
            this.SetHasNumericSamsung(EntryEffectForms.GetHasNumericSamsung(Element));
        }

        /// <summary>
        /// Ons the detached.
        /// </summary>
        protected override void OnDetached()
        {
            originalBackground = null;

            this.SetNextControl(null);
            this.setSelectedTextOnFocus(false);
        }

        /// <summary>
        /// Ons the element property changed.
        /// </summary>
        /// <param name="args">Arguments.</param>
        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName.Equals(EntryEffectForms.HasBorderProperty.PropertyName))
                this.SetBorder((bool)Element.GetValue(EntryEffectForms.HasBorderProperty));
            else if (args.PropertyName.Equals(EntryEffectForms.NextControlFocusProperty.PropertyName))
                this.SetNextControl((View)Element.GetValue(EntryEffectForms.NextControlFocusProperty));
            else if (args.PropertyName.Equals(EntryEffectForms.LeftImageProperty.PropertyName))
                this.setSelectedTextOnFocus((bool)Element.GetValue(EntryEffectForms.LeftImageProperty));
            else if (args.PropertyName.Equals(EntryEffectForms.RightImageProperty.PropertyName))
                this.setSelectedTextOnFocus((bool)Element.GetValue(EntryEffectForms.RightImageProperty));
            else if (args.PropertyName.Equals(EntryEffectForms.HasNumericSamsungProperty.PropertyName))
                this.SetHasNumericSamsung((bool)Element.GetValue(EntryEffectForms.HasNumericSamsungProperty));
        }

        #region Private 

        private void SetBorder(bool hasBorder)
        {
            if (hasBorder == false)
            {
                var shape = new ShapeDrawable(new RectShape());
                shape.Paint.Alpha = 0;
                shape.Paint.SetStyle(Paint.Style.Stroke);
                this.Control.SetBackground(shape);
            }
            else
            {
                this.Control.SetBackground(originalBackground);
            }
        }

        private void SetNextControl(View view)
        {
            viewNextFocus = view;

            if (this.Control is EditText current)
            {
                if (viewNextFocus == null)
                    current.EditorAction -= Control_EditorAction;
                else
                    current.EditorAction += Control_EditorAction;
            }
        }

        void Control_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            try { viewNextFocus?.Focus(); }
            catch (Exception) { }
        }

        private void setSelectedTextOnFocus(bool v)
        {
            if (this.Control is EditText current)
                current.SetSelectAllOnFocus(v);
        }

        private void SetRightImage(string val)
        {
            if (this.Control is EditText current)
            {
                if (!val.IsNullOrEmpty() && this.Control.Context.GetDrawable(val) is Drawable image)
                    current.SetCompoundDrawablesWithIntrinsicBounds(null, null, image, null);
                else
                    current.SetCompoundDrawablesWithIntrinsicBounds(null, null, null, null);
            }
        }

        private void SetLeftImage(string val)
        {
            if (this.Control is EditText current)
            {
                if (!val.IsNullOrEmpty() && this.Control.Context.GetDrawable(val) is Drawable image)
                    current.SetCompoundDrawablesWithIntrinsicBounds(image, null, null, null);
                else
                    current.SetCompoundDrawablesWithIntrinsicBounds(null, null, null, null);
            }
        }

        private void SetHasNumericSamsung(bool val)
        {
            if (this.Control is EditText current && val && this.HasSamsung())
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    current.KeyListener = DigitsKeyListener.GetInstance(current.Resources.Configuration.Locale, false, true);
                else
#pragma warning disable CS0618 // Type or member is obsolete
                    current.KeyListener = DigitsKeyListener.GetInstance(false, true);
#pragma warning restore CS0618 // Type or member is obsolete

                current.InputType = Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal;
            }
        }

        private bool HasSamsung()
        {
            var result = false;
            try
            {
                //result = I4FormsApp.Current.Locator.GetInstance<IDeviceService>() is ServiceForDevice service && service.IsSamsungDevice;
            }
            catch (Exception)
            {
                Console.WriteLine("not get DeviceService for get Samsung.");
            }

            return result;
        }

        #endregion
    }
}
