using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INetApp.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Toolbar_main : ContentView
    {
        public Toolbar_main()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(TabView), 0, BindingMode.TwoWay);

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

    }
}