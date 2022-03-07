using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using INetApp.Extensions;
using Xamarin.Forms;

namespace INetApp.Effects
{
    /// <summary>
    /// Entry effect.
    /// </summary>
    public class EntryEffect : BaseEntryEffect<Entry>
    {
        #region HasBorder Property

        /// <summary>
        /// The has border property.
        /// </summary>
        public static readonly BindableProperty HasBorderProperty = BindableProperty.CreateAttached("HasBorder", typeof(bool), typeof(EntryEffect), false, propertyChanged: HasBorderChanged);

        /// <summary>
        /// Gets the has border.
        /// </summary>
        /// <returns><c>true</c>, if has border was gotten, <c>false</c> otherwise.</returns>
        /// <param name="view">View.</param>
        public static bool GetHasBorder(BindableObject view) => (bool)view.GetValue(HasBorderProperty);

        /// <summary>
        /// Sets the has border.
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="border">If set to <c>true</c> border.</param>
        public static void SetHasBorder(BindableObject view, bool border) => view.SetValue(HasBorderProperty, border);

        private static void HasBorderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Entry control && newValue is bool border)
            {
                CreateEffectIfNotExists<EntryEffect>(control);
                EntryEffect.SetHasBorder(control, border);
            }
        }

        #endregion

        #region NextControlFocus Property

        /// <summary>
        /// The next control focus property.
        /// </summary>
        public static readonly BindableProperty NextControlFocusProperty = BindableProperty.CreateAttached("NextControlFocus", typeof(View), typeof(EntryEffect), default(View), propertyChanged: NextControlFocusChanged);

        /// <summary>
        /// Gets the next control focus.
        /// </summary>
        /// <returns>The next control focus.</returns>
        /// <param name="view">View.</param>
        public static View GetNextControlFocus(BindableObject view)
        {
            return (View)view.GetValue(NextControlFocusProperty);
        }

        /// <summary>
        /// Sets the next control focus.
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="control">Control.</param>
        public static void SetNextControlFocus(BindableObject view, View control)
        {
            view.SetValue(NextControlFocusProperty, control);
        }

        private static void NextControlFocusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Entry control && newValue is View view)
            {
                CreateEffectIfNotExists<EntryEffect>(control);
                EntryEffect.SetNextControlFocus(control, view);
            }
        }

        #endregion

        #region SelectedTextOnFocus Property

        /// <summary>
        /// The selected all on focus property.
        /// </summary>
        public static readonly BindableProperty SelectedTextOnFocusProperty = BindableProperty.CreateAttached("SelectedTextOnFocus", typeof(bool), typeof(EntryEffect), default(bool), propertyChanged: SelectedAllOnFocusChanged);

        /// <summary>
        /// Gets the selected all on focus.
        /// </summary>
        /// <returns><c>true</c>, if selected all on focus was gotten, <c>false</c> otherwise.</returns>
        /// <param name="view">View.</param>
        public static bool GetSelectedTextOnFocus(BindableObject view)
        {
            return (bool)view.GetValue(SelectedTextOnFocusProperty);
        }

        /// <summary>
        /// Sets the selected all on focus.
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="val">If set to <c>true</c> value.</param>
        public static void SetSelectedTextOnFocus(BindableObject view, bool val)
        {
            view.SetValue(SelectedTextOnFocusProperty, val);
        }

        private static void SelectedAllOnFocusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Entry control && newValue is bool val)
            {
                CreateEffectIfNotExists<EntryEffect>(control);
                EntryEffect.SetSelectedTextOnFocus(control, val);
            }
        }

        #endregion

        #region RightImage Property

        /// <summary>
        /// The right image property.
        /// </summary>
        public static readonly BindableProperty RightImageProperty = BindableProperty.CreateAttached("RightImage", typeof(string), typeof(EntryEffect), null, propertyChanged: RightImageChanged);

        /// <summary>
        /// Gets the right image.
        /// </summary>
        /// <returns>The right image.</returns>
        /// <param name="view">View.</param>
        public static string GetRightImage(BindableObject view)
        {
            return (string)view.GetValue(RightImageProperty);
        }

        /// <summary>
        /// Sets the right image.
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="val">Value.</param>
        public static void SetRightImage(BindableObject view, string val)
        {
            view.SetValue(RightImageProperty, val);
        }

        private static void RightImageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Entry control && newValue is string val)
            {
                CreateEffectIfNotExists<EntryEffect>(control);
                EntryEffect.SetRightImage(control, val);
            }
        }

        #endregion

        #region LeftImage Property

        /// <summary>
        /// The right image property.
        /// </summary>
        public static readonly BindableProperty LeftImageProperty = BindableProperty.CreateAttached("LeftImage", typeof(string), typeof(EntryEffect), null, propertyChanged: LeftImageChanged);

        /// <summary>
        /// Gets the right image.
        /// </summary>
        /// <returns>The right image.</returns>
        /// <param name="view">View.</param>
        public static string GetLeftImage(BindableObject view)
        {
            return (string)view.GetValue(LeftImageProperty);
        }

        /// <summary>
        /// Sets the right image.
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="val">Value.</param>
        public static void SetLeftImage(BindableObject view, string val)
        {
            view.SetValue(LeftImageProperty, val);
        }

        private static void LeftImageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Entry control && newValue is string val)
            {
                CreateEffectIfNotExists<EntryEffect>(control);
                EntryEffect.SetLeftImage(control, val);
            }
        }

        #endregion

        #region Mask Property

        private IDictionary<int, char> positions;

        /// <summary>
        /// Mask property
        /// </summary>
        public static readonly BindableProperty MaskProperty = BindableProperty.CreateAttached("Mask", typeof(string), typeof(EntryEffect), null, propertyChanged: MaskChanged);

        /// <summary>
        /// Gets Mask entry
        /// </summary>
        /// <returns>The right image.</returns>
        /// <param name="view">View.</param>
        public static string GetMask(BindableObject view)
        {
            return (string)view.GetValue(MaskProperty);
        }

        /// <summary>
        /// Sets Mask entry
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="val">Value.</param>
        public static void SetMask(BindableObject view, string val)
        {
            view.SetValue(LeftImageProperty, val);
        }

        private static void MaskChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Entry control && newValue is string val)
            {
                EntryEffect effect = CreateEffectIfNotExists<EntryEffect>(control);
                EntryEffect.SetMask(control, val);
                effect.SetPositions();
            }
        }

        #endregion

        #region HasNumericSamsung Property

        /// <summary>
        /// The has Numeric for samsung property.
        /// </summary>
        public static readonly BindableProperty HasNumericSamsungProperty = BindableProperty.CreateAttached("HasNumericSamsung", typeof(bool), typeof(EntryEffect), false, propertyChanged: HasNumericSamsungChanged);

        /// <summary>
        /// Gets the has border.
        /// </summary>
        /// <returns><c>true</c>, if has border was gotten, <c>false</c> otherwise.</returns>
        /// <param name="view">View.</param>
        public static bool GetHasNumericSamsung(BindableObject view)
        {
            return (bool)view.GetValue(HasNumericSamsungProperty);
        }

        /// <summary>
        /// Sets the has border.
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="val">If set to <c>true</c> border.</param>
        public static void SetHasNumericSamsung(BindableObject view, bool val)
        {
            view.SetValue(HasNumericSamsungProperty, val);
        }

        private static void HasNumericSamsungChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Entry control && newValue is bool val)
            {
                CreateEffectIfNotExists<EntryEffect>(control);
                EntryEffect.SetHasNumericSamsung(control, val);
            }
        }

        #endregion

        #region CheckTypeText Property

        /// <summary>
        /// The has Numeric for samsung property.
        /// </summary>
        public static readonly BindableProperty CheckTypeTextProperty = BindableProperty.CreateAttached("CheckTypeText", typeof(CheckTypeTextEnum), typeof(EntryEffect), CheckTypeTextEnum.Text, propertyChanged: CheckTypeTextChanged);

        /// <summary>
        /// Gets the has border.
        /// </summary>
        /// <returns><c>true</c>, if has border was gotten, <c>false</c> otherwise.</returns>
        /// <param name="view">View.</param>
        public static CheckTypeTextEnum GetCheckTypeText(BindableObject view)
        {
            return (CheckTypeTextEnum)view.GetValue(CheckTypeTextProperty);
        }

        /// <summary>
        /// Sets the has border.
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="val">If set to <c>true</c> border.</param>
        public static void SetCheckTypeText(BindableObject view, CheckTypeTextEnum val)
        {
            view.SetValue(CheckTypeTextProperty, val);
        }

        private static void CheckTypeTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Entry control && newValue is CheckTypeTextEnum val)
            {
                CreateEffectIfNotExists<EntryEffect>(control);
                EntryEffect.SetCheckTypeText(control, val);
            }
        }

        #endregion

        #region ToDouble Property

        /// <summary>
        /// The Double value in Entry to binding.
        /// </summary>
        public static readonly BindableProperty ToDoubleProperty = BindableProperty.CreateAttached("ToDouble", typeof(double), typeof(EntryEffect), 0d, defaultBindingMode: BindingMode.TwoWay, propertyChanged: ToDoubleChanged);

        /// <summary>
        /// Gets Double value in Entry
        /// </summary>
        /// <returns> value in double</returns>
        /// <param name="view">View.</param>
        public static double GetToDouble(BindableObject view)
        {
            return (double)view.GetValue(ToDoubleProperty);
        }

        /// <summary>
        /// Sets Double value in Entry
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="val">value in double</param>
        public static void SetToDouble(BindableObject view, double val)
        {
            view.SetValue(ToDoubleProperty, val);
        }

        private static void ToDoubleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Entry control && newValue is double val)
            {
                EntryEffect effect = CreateEffectIfNotExists<EntryEffect>(control);
                EntryEffect.SetToDouble(control, val);
                effect.SetDataBindings();
            }
        }

        #endregion

        #region ToInt Property

        /// <summary>
        /// The Integer property for Entry
        /// </summary>
        public static readonly BindableProperty ToIntProperty = BindableProperty.CreateAttached("ToInt", typeof(int), typeof(EntryEffect), 0, defaultBindingMode: BindingMode.TwoWay, propertyChanged: ToIntChanged);

        /// <summary>
        /// Gets Integer value in Entry
        /// </summary>
        /// <returns>value in integer</returns>
        /// <param name="view">View.</param>
        public static int GetToInt(BindableObject view)
        {
            return (int)view.GetValue(ToIntProperty);
        }

        /// <summary>
        /// Sets Integer value in Entry
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="val">value in integer</param>
        public static void SetToInt(BindableObject view, int val)
        {
            view.SetValue(ToIntProperty, val);
        }

        private static void ToIntChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Entry control && newValue is int val)
            {
                EntryEffect effect = CreateEffectIfNotExists<EntryEffect>(control);
                EntryEffect.SetToInt(control, val);
                effect.SetDataBindings();
            }
        }

        #endregion

        #region MaxDecimalsDouble Property

        /// <summary>
        /// The max of decimals for Double value.
        /// </summary>
        public static readonly BindableProperty MaxDecimalsDoubleProperty = BindableProperty.CreateAttached("MaxDecimalsDouble", typeof(int), typeof(EntryEffect), -1, propertyChanged: MaxDecimalsDoubleChanged);

        /// <summary>
        /// Gets the max of decimals for Double value.
        /// </summary>
        /// <returns>max decimals</returns>
        /// <param name="view">View.</param>
        public static int GetMaxDecimalsDouble(BindableObject view)
        {
            return (int)view.GetValue(MaxDecimalsDoubleProperty);
        }

        /// <summary>
        /// Sets the max of decimals for Double value.
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="val">max decimals</param>
        public static void SetMaxDecimalsDouble(BindableObject view, int val)
        {
            view.SetValue(MaxDecimalsDoubleProperty, val);
        }

        private static void MaxDecimalsDoubleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Entry control && newValue is int val)
            {
                CreateEffectIfNotExists<EntryEffect>(control);
                EntryEffect.SetMaxDecimalsDouble(control, val);
            }
        }

        #endregion

        #region CharactersNotAllowed Property

        /// <summary>
        /// Characters Not Allowed Property
        /// </summary>
        public static readonly BindableProperty CharactersNotAllowedProperty = BindableProperty.CreateAttached("CharactersNotAllowed", typeof(IEnumerable<char>), typeof(EntryEffect), null, propertyChanged: CharactersNotAllowedChanged);

        /// <summary>
        /// Gets the list of characters not allowed.
        /// </summary>
        /// <returns>The next control focus.</returns>
        /// <param name="view">View.</param>
        public static IEnumerable<char> GetCharactersNotAllowed(BindableObject view)
        {
            return (IEnumerable<char>)view.GetValue(CharactersNotAllowedProperty);
        }

        /// <summary>
        /// Sets the list of characters not allowed.
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="control">Control.</param>
        public static void SetCharactersNotAllowed(BindableObject view, IEnumerable<char> list)
        {
            view.SetValue(CharactersNotAllowedProperty, list);
        }

        private static void CharactersNotAllowedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Entry control && newValue is IEnumerable<char> val)
            {
                CreateEffectIfNotExists<EntryEffect>(control);
                EntryEffect.SetCharactersNotAllowed(control, val);
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:I4KernelBase.Effects.EntryEffect"/> class.
        /// </summary>
        public EntryEffect() : base("INetApp.EntryEffect")
        {
        }

        /// <summary>
        /// On attached
        /// </summary>
        protected override void OnAttached()
        {
            if (Element != null)
            {
                Element.TextChanged += OnEntryTextChanged;
                Element.Unfocused += OnEntryUnfocused;
            }

            OnEntryTextChanged(Element, null);
            OnEntryUnfocused(Element, null);
            SetDataBindings();
        }

        /// <summary>
        /// On Detached
        /// </summary>
        protected override void OnDetached()
        {
            if (Element != null)
            {
                Element.TextChanged -= OnEntryTextChanged;
                Element.Unfocused -= OnEntryUnfocused;
            }

            base.OnDetached();
        }

        #region Private

        private string MaskFromEffect => Element != null ? EntryEffect.GetMask(Element) : "";
        private CheckTypeTextEnum CheckTypeTextFromEffect => Element != null ? EntryEffect.GetCheckTypeText(Element) : CheckTypeTextEnum.Text;
        private IEnumerable<char> CharactersNotAllowedFromEffect => Element != null ? EntryEffect.GetCharactersNotAllowed(Element) : null;

        private double ToDoubleEffect
        {
            get => Element != null ? EntryEffect.GetToDouble(Element) : 0d;
            set => Element.RunSafe(val => EntryEffect.SetToDouble(Element, value));
        }
        private int ToIntEffect
        {
            get => Element != null ? EntryEffect.GetToInt(Element) : 0;
            set => Element.RunSafe(val => EntryEffect.SetToInt(Element, value));
        }
        private int MaxDecimalsEffect
        {
            get => Element != null ? EntryEffect.GetMaxDecimalsDouble(Element) : 0;
            set => Element.RunSafe(val => EntryEffect.SetMaxDecimalsDouble(Element, value));
        }

        private void SetPositions()
        {
            string mask = MaskFromEffect;

            if (string.IsNullOrEmpty(mask))
            {
                positions = null;
                return;
            }

            Dictionary<int, char> list = new Dictionary<int, char>();
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] != 'X')
                {
                    list.Add(i, mask[i]);
                }
            }

            positions = list;

            OnEntryTextChanged(Element, null);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            bool next = true; // variable que se encarga de dejar seguir adelante 

            if (sender is Entry entry)
            {
                string oldText = args?.OldTextValue ?? "";
                string currentText = entry.Text;

                if (MaskFromEffect is string mask)
                {
                    if (next && (string.IsNullOrWhiteSpace(currentText) || positions.IsNullOrNotElements() || mask.IsNullOrEmpty()))
                    {
                        next = false;
                    }

                    if (next && (mask.StartsWith(">>", StringComparison.InvariantCulture) || mask.StartsWith(">>!", StringComparison.InvariantCulture)))
                    {
                        next = false;
                    }

                    if (next && currentText.Length > mask.Length)
                    {
                        entry.Text = currentText.Remove(currentText.Length - 1);
                        return;
                    }

                    if (next)
                    {
                        foreach (KeyValuePair<int, char> position in positions)
                        {
                            if (currentText.Length >= position.Key + 1)
                            {
                                string value = position.Value.ToString();
                                if (currentText.Substring(position.Key, 1) != value)
                                {
                                    currentText = currentText.Insert(position.Key, value);
                                }
                            }
                        }
                    }

                    if (next && !entry.Text.Equals(currentText))
                    {
                        entry.Text = currentText;
                    }
                }

                currentText = entry.Text; // actualizamos el current por si cambio

                if (CheckTypeTextFromEffect == CheckTypeTextEnum.Double)
                {
                    double number = currentText.ConvertToTypeFormat(CultureInfo.CurrentCulture, defaultValue: double.NaN, safeDecimals: true);

                    if (currentText.IsNullOrEmpty())
                    {
                        number = 0d;
                    }

                    if (MaxDecimalsEffect >= 0 && number.GetNumberOfDecimals() > MaxDecimalsEffect)
                    {
                        entry.Text = args.OldTextValue ?? number.Truncate(MaxDecimalsEffect).ToString();
                    }
                    else if (double.IsNaN(number))
                    {
                        entry.Text = args.OldTextValue;
                    }
                    else
                    {
                        ToDoubleEffect = number;
                    }
                }
                else if (CheckTypeTextFromEffect == CheckTypeTextEnum.Integer)
                {
                    int number = currentText.ConvertToTypeFormat(CultureInfo.CurrentCulture, defaultValue: int.MinValue, safeDecimals: true);

                    if (currentText.IsNullOrEmpty())
                    {
                        number = 0;
                    }

                    if (number == int.MinValue)
                    {
                        entry.Text = args.OldTextValue;
                    }
                    else
                    {
                        ToIntEffect = number;
                    }

                    if (!currentText.IsNullOrNotElements() && (currentText.Contains(",") || currentText.Contains(".")))
                    {
                        entry.Text = args.OldTextValue ?? number.ToString();
                    }
                }
                else if (CheckTypeTextFromEffect == CheckTypeTextEnum.TextNumber)
                {
                    if (!currentText.IsNullOrEmpty())
                    {
                        entry.Text = Regex.IsMatch(currentText, "^([0-9]+)$") ? currentText : args.OldTextValue;
                    }
                }

                currentText = entry.Text; // actualizamos el current por si cambio              

                if (CharactersNotAllowedFromEffect is IEnumerable<char> charsNotAllowed)
                {
                    if (!currentText.IsNullOrEmpty() && !charsNotAllowed.IsNullOrNotElements())
                    {
                        entry.Text = currentText.ToCharArray().All(x => !charsNotAllowed.Contains(x)) ? currentText : args.OldTextValue;
                    }
                }
            }
        }

        private void OnEntryUnfocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry && MaskFromEffect is string mask)
            {
                string text = entry.Text;

                if (string.IsNullOrWhiteSpace(text) || mask.IsNullOrEmpty())
                {
                    return;
                }

                if (mask.StartsWith(">>", StringComparison.InvariantCulture))
                {
                    bool ignore = mask.StartsWith(">>!", StringComparison.InvariantCulture);
                    mask = mask.Replace(">>!", "");
                    mask = mask.Replace(">>", "");
                    int digits = mask.Count(c => c == 'X');
                    string str = mask.Replace("X", "");

                    if (!str.IsNullOrEmpty() && text.Length >= digits && (ignore || !text.Contains(str)))
                    {
                        string result = text.Insert(text.Length - digits, str);
                        entry.Text = result;
                    }
                }
            }
        }

        private void SetDataBindings()
        {
            if (Element != null)
            {
                if (CheckTypeTextFromEffect == CheckTypeTextEnum.Double)
                {
                    Element.Text = ToDoubleEffect.ToString();
                }
                else if (CheckTypeTextFromEffect == CheckTypeTextEnum.Integer)
                {
                    Element.Text = ToIntEffect.ToString();
                }
            }
        }

        #endregion
    }
}
