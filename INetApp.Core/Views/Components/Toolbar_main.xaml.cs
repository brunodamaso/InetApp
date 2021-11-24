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

        public static readonly BindableProperty SelectedTabProperty = BindableProperty.Create("SelectedTab", typeof(int), typeof(int), 0);

        public int SelectedTab
        {
            get => (int)GetValue(SelectedTabProperty);
            set => SetValue(SelectedTabProperty, value);
        }

        private void TabView_SelectionChanged(object sender, Xamarin.CommunityToolkit.UI.Views.TabSelectionChangedEventArgs e)
        {
            TabView tabselected = (TabView)sender;
            this.SelectedTab = tabselected.SelectedIndex;
        }
    }
}