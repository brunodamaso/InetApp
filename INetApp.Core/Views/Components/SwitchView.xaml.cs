using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INetApp.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwitchView : ContentView
    {
        #region IsToggled Property

        public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(SwitchView), false, BindingMode.TwoWay, null, IsToggledPropertyChangedDelegate);

        public bool IsToggled
        {
            get => (bool)GetValue(IsToggledProperty);
            set => SetValue(IsToggledProperty, value);
        }

        #endregion

        #region Text Property

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SwitchView), string.Empty, BindingMode.OneWay, null, UIPropertyChangedDelegate);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        #endregion

        public SwitchView()
        {
            InitializeComponent();

            UIPropertyChangedDelegate(this, null, null);
        }

        #region Private

        private static void UIPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SwitchView switchView)
            {
                SetImageStyle(switchView);
            }
        }

        private static void IsToggledPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            UIPropertyChangedDelegate(bindable, null, null);
        }

        private void Handle_Tapped(object sender, EventArgs e)
        {
            IsToggled = !IsToggled;

            SetImageStyle(this);
        }

        private static void SetImageStyle(SwitchView switchView)
        {
            ResourceDictionary resources = switchView.Resources;

            if (switchView.IsToggled)
            {
                switchView.Imagen.Style = (Style)resources["SwitchView.Image.Toggled"];
            }
            else
            {
                switchView.Imagen.Style = (Style)resources["SwitchView.Image.NotToggled"];
            }
        }

        #endregion
    }
}
