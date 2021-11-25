
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INetApp.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectAll : ContentView
    {
        public SelectAll()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBox), false, BindingMode.TwoWay);

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }
    }
}