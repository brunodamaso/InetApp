using System;
using System.ComponentModel;
using System.Drawing;
using INetApp.Extensions;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using EntryEffectForms = INetApp.Effects.EntryEffect;

[assembly: Xamarin.Forms.ExportEffect(typeof(INetApp.iOS.Effects.EntryEffect), "EntryEffect")]
namespace INetApp.iOS.Effects
{
    /// <summary>
    /// Entry effect.
    /// </summary>
    public class EntryEffect : PlatformEffect
    {
        private View viewNextFocus;

        /// <summary>
        /// Ons the attached.
        /// </summary>
        protected override void OnAttached()
        {
            this.SetBorder((bool)Element.GetValue(EntryEffectForms.HasBorderProperty));
            this.SetNextControl((View)Element.GetValue(EntryEffectForms.NextControlFocusProperty));
            this.SetSelectedTextOnFocus((bool)Element.GetValue(EntryEffectForms.SelectedTextOnFocusProperty));
            this.SetRightImage(EntryEffectForms.GetRightImage(Element));
            this.SetLeftImage(EntryEffectForms.GetLeftImage(Element));
        }

        /// <summary>
        /// Ons the detached.
        /// </summary>
        protected override void OnDetached()
        {
            this.SetNextControl(null);
            this.SetSelectedTextOnFocus(false);
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
            else if (args.PropertyName.Equals(EntryEffectForms.SelectedTextOnFocusProperty.PropertyName))
                this.SetSelectedTextOnFocus((bool)Element.GetValue(EntryEffectForms.SelectedTextOnFocusProperty));
            else if (args.PropertyName.Equals(EntryEffectForms.LeftImageProperty.PropertyName))
                this.SetSelectedTextOnFocus((bool)Element.GetValue(EntryEffectForms.LeftImageProperty));
            else if (args.PropertyName.Equals(EntryEffectForms.RightImageProperty.PropertyName))
                this.SetSelectedTextOnFocus((bool)Element.GetValue(EntryEffectForms.RightImageProperty));
        }

        #region Private

        private void SetNextControl(View view)
        {
            viewNextFocus = view;

            if (this.Control is UITextField current)
            {
                if (viewNextFocus == null)
                    current.ShouldReturn -= Control_EditorAction;
                else
                    current.ShouldReturn += Control_EditorAction;
            }
        }

        private bool Control_EditorAction(UITextField textField)
        {
            try { viewNextFocus?.Focus(); } catch (Exception) { }

            return true;
        }

        private void SetBorder(bool v)
        {
            if (this.Control is UITextField current)
                current.BorderStyle = v ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
        }

        private void SetSelectedTextOnFocus(bool v)
        {
            if (this.Control is UITextField current)
            {
                if (v)
                    current.EditingDidBegin += Control_editingBegin;
                else
                    current.EditingDidBegin -= Control_editingBegin;
            }
        }

        private void Control_editingBegin(object sender, EventArgs e)
        {
            var active = (bool)Element.GetValue(EntryEffectForms.SelectedTextOnFocusProperty);

            if (active && this.Control is UITextField current && !current.Text.IsNullOrEmpty())
                current.SelectAll(current);
        }

        private void SetRightImage(string val)
        {
            if (this.Control is UITextField current)
            {
                if (!val.IsNullOrEmpty() && UIImage.FromBundle(val) is UIImage image)
                {
                    current.RightView = new UIImageView(image)
                    {
                        Frame = new RectangleF(10, 0, 25, 20),
                        ContentMode = UIViewContentMode.ScaleAspectFit
                    };
                    current.RightViewMode = UITextFieldViewMode.Always;
                }
                else
                {
                    current.RightView = null;
                    current.RightViewMode = UITextFieldViewMode.Never;
                }
            }
        }

        private void SetLeftImage(string val)
        {
            if (this.Control is UITextField current)
            {
                if (!val.IsNullOrEmpty() && UIImage.FromBundle(val) is UIImage image)
                {
                    current.LeftView = new UIImageView(image)
                    {
                        Frame = new RectangleF(10, 0, 25, 20),
                        ContentMode = UIViewContentMode.ScaleAspectFit
                    };
                    current.LeftViewMode = UITextFieldViewMode.Always;
                }
                else
                {
                    current.LeftView = null;
                    current.LeftViewMode = UITextFieldViewMode.Never;
                }
            }
        }

        #endregion
    }
}
