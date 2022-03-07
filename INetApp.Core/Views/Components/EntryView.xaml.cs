using System.Windows.Input;
using Xamarin.Forms;

namespace INetApp.Views.Components
{
    public enum ErrorType
    {
        None,
        Error
    }

    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class EntryView : ContentView
    {
        public ResourceDictionary AppResources => App.Current.Resources;

        #region IsFocused Property

        public static new readonly BindableProperty IsFocusedProperty = BindableProperty.Create(nameof(IsFocused), typeof(bool), typeof(EntryView), false, BindingMode.TwoWay, null, UIPropertyChangedDelegate);

        public new bool IsFocused
        {
            get => (bool)GetValue(IsFocusedProperty);
            set => SetValue(IsFocusedProperty, value);
        }

        #endregion

        #region IsEnabled Property

        public static new readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(EntryView), true, BindingMode.OneWay, null, UIPropertyChangedDelegate);

        public new bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        #endregion

        #region Text Property

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryView), "", BindingMode.TwoWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion

        #region Maxlenght Property

        public static readonly BindableProperty MaxLenghtProperty = BindableProperty.Create(nameof(MaxLength), typeof(string), typeof(EntryView), null, BindingMode.TwoWay);

        public string MaxLength
        {
            get => (string)GetValue(MaxLenghtProperty);
            set => SetValue(MaxLenghtProperty, value);
        }

        #endregion

        #region IsNumeric Property
        public static BindableProperty IsNumericProperty = BindableProperty.Create(nameof(IsNumeric), typeof(bool), typeof(EntryView), false, BindingMode.TwoWay, null, UIPropertyChangedDelegate);

        public bool IsNumeric
        {
            get => (bool)GetValue(IsNumericProperty);
            set => SetValue(IsNumericProperty, value);
        }
        #endregion

        #region Placeholder Property

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(EntryView), "", BindingMode.TwoWay);

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        #endregion

        #region Keyboard Property

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(EntryView), Keyboard.Text, BindingMode.TwoWay);

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        #endregion

        #region ReturnType Property

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(EntryView), null, BindingMode.TwoWay);

        public ReturnType ReturnType
        {
            get => (ReturnType)GetValue(ReturnTypeProperty);
            set => SetValue(ReturnTypeProperty, value);
        }

        #endregion

        #region IsPassword Property

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(EntryView), false, BindingMode.OneWay);

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }
        #endregion

        #region Minimum Property

        public static readonly BindableProperty MinimumProperty = BindableProperty.Create(nameof(Minimum), typeof(int?), typeof(EntryView), null, BindingMode.OneWay);

        public int? Minimum
        {
            get => (int?)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }
        #endregion

        #region Maximum Property

        public static readonly BindableProperty MaximumProperty = BindableProperty.Create(nameof(Maximum), typeof(int?), typeof(EntryView), null, BindingMode.OneWay);

        public int? Maximum
        {
            get => (int?)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }
        #endregion

        #region CanDecimal Property

        public static readonly BindableProperty CanDecimalProperty = BindableProperty.Create(nameof(CanDecimal), typeof(bool), typeof(EntryView), false, BindingMode.OneWay);

        public bool CanDecimal
        {
            get => (bool)GetValue(CanDecimalProperty);
            set => SetValue(CanDecimalProperty, value);
        }
        #endregion

        #region ReturnCommand Property

        public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(nameof(ReturnCommand), typeof(ICommand), typeof(EntryView), null, BindingMode.OneWay);

        public ICommand ReturnCommand
        {
            get => (ICommand)GetValue(ReturnCommandProperty);
            set => SetValue(ReturnCommandProperty, value);
        }

        #endregion

        #region ImageStyle Property

        public static readonly BindableProperty ImageStyleProperty = BindableProperty.Create(nameof(ImageStyle), typeof(Style), typeof(EntryView), null, BindingMode.OneWay);

        public Style ImageStyle
        {
            get => (Style)GetValue(ImageStyleProperty);
            set => SetValue(ImageStyleProperty, value);
        }

        #endregion

        #region IsError Property

        public static readonly BindableProperty IsErrorProperty = BindableProperty.Create(nameof(IsError), typeof(bool), typeof(EntryView), false, BindingMode.OneWay, null, UIPropertyChangedDelegate);

        public bool IsError
        {
            get => (bool)GetValue(IsErrorProperty);
            set => SetValue(IsErrorProperty, value);
        }
        #endregion

        #region ErrorText Property

        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(EntryView), "", BindingMode.OneWay);

        public string ErrorText
        {
            get => (string)GetValue(ErrorTextProperty);
            set => SetValue(ErrorTextProperty, value);
        }

        #endregion

        #region ErrorType Property

        public static readonly BindableProperty ErrorTypeProperty = BindableProperty.Create(nameof(ErrorType), typeof(ErrorType), typeof(EntryView), ErrorType.None, BindingMode.OneWay, null, UIPropertyChangedDelegate);

        public ErrorType ErrorType
        {
            get => (ErrorType)GetValue(ErrorTypeProperty);
            set => SetValue(ErrorTypeProperty, value);
        }

        #endregion

        public EntryView()
        {
            InitializeComponent();
            UIPropertyChangedDelegate(this, null, null);

            if (Content is View view)
            {
                if (view.FindByName("TapEntryView") is TapGestureRecognizer TapEntryView && view.FindByName("EntryElement") is Entry _entry)
                {
                    TapEntryView.Tapped += (e, v) =>
                    {
                        if (_entry.IsEnabled && !_entry.IsReadOnly)
                        {
                            Dispatcher.BeginInvokeOnMainThread(() =>
                            {
                                _entry.Focus();
                                _entry.CursorPosition = 0;
                                _entry.SelectionLength = _entry.Text != null ? _entry.Text.Length : 0;
                            });
                        }
                    };
                }
            }
            //if (this.Keyboard == Keyboard.Numeric || this.IsNumeric || this.CanDecimal)
            //{
            //    EntryElement.HorizontalTextAlignment = TextAlignment.End;
            //}
        }

        private static async void UIPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            ResourceDictionary resources = Application.Current.Resources;

            if (bindable is EntryView entryView)
            {
                entryView.EntryElement.IsReadOnly = false;

                if (resources != null)
                {
                    if (!entryView.IsEnabled)
                    {
                        entryView.EntryElement.IsReadOnly = true;
                        SetStyle(entryView, "Disabled");
                    }
                    else if (entryView.IsError)
                    {
                        SetStyle(entryView, "Error");
                    }
                    else if (entryView.IsFocused)
                    {
                        SetStyle(entryView, "Focused");
                    }
                    else
                    {
                        SetStyle(entryView, "Normal");
                    }
                }
            }
        }
        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (Keyboard == Keyboard.Numeric || IsNumeric || CanDecimal)
            {
                Keyboard = Keyboard.Numeric;
                IsNumeric = true;
                EntryElement.HorizontalTextAlignment = TextAlignment.End;
            }
            if (Keyboard == Keyboard.Numeric)
            {
                if (string.IsNullOrEmpty(args.NewTextValue))
                {
                    ((Entry)sender).Text = "";
                    return;
                }
                if (CanDecimal)
                {
                    double _;
                    if (!double.TryParse(args.NewTextValue, out _))
                    {
                        ((Entry)sender).Text = args.OldTextValue;
                    }
                }
                else
                {
                    long _;
                    if (!long.TryParse(args.NewTextValue, out _))
                    {
                        ((Entry)sender).Text = args.OldTextValue;
                    }
                }
                if (Minimum != null)
                {
                    double _;
                    double.TryParse(((Entry)sender).Text, out _);
                    if (_ < Minimum)
                    {
                        ((Entry)sender).Text = args.OldTextValue;
                    }
                }
                if (Maximum != null)
                {
                    double _;
                    double.TryParse(((Entry)sender).Text, out _);
                    if (_ > Maximum)
                    {
                        ((Entry)sender).Text = args.OldTextValue;
                    }
                }
            }
        }
        private static void SetStyle(EntryView entryView, string style)
        {
            ResourceDictionary resources = entryView.Resources;
            //    Application.Current.Resources;

            entryView.EntryElement.Style = (Style)resources[$"EntryView.Entry.{style}"];
            //entryView.PlaceholderElement.Style = (Style)resources[$"EntryView.Placeholder.{style}"];

            //string lineStyle = $"EntryView.Border.{style}";
            //entryView.Line1.Style = (Style)resources[lineStyle];
            //entryView.Line2.Style = (Style)resources[lineStyle];
            //entryView.Line3.Style = (Style)resources[lineStyle];
            //entryView.Line4.Style = (Style)resources[lineStyle];
            //entryView.Line5.Style = (Style)resources[lineStyle];
        }
    }

    //[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    //public partial class EntryViewStyles : ResourceDictionary
    //{
    //    public static string Key => nameof(EntryViewStyles);

    //    public EntryViewStyles()
    //    {
    //        InitializeComponent();
    //    }
    //}
}

