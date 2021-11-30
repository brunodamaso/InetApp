using INetApp.ViewModels;
using Xamarin.Forms;

namespace INetApp.Views.Components
{
    public partial class MessageRow : ContentView
    {
        public MessageRow()
        {
            InitializeComponent();

        }

        public static readonly BindableProperty IsRowCheckedProperty = BindableProperty.Create(nameof(IsRowChecked), typeof(bool), typeof(CheckBox), null, BindingMode.TwoWay);

        public bool IsRowChecked
        {
            get => (bool)GetValue(IsRowCheckedProperty);
            set => SetValue(IsRowCheckedProperty, value);
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            try
            {
                CheckBox aa = (CheckBox)sender;
                MessageViewModel vm = aa.Parent.Parent.Parent.Parent.BindingContext as MessageViewModel;
                IsRowChecked = vm.IsRowSelect();
            }
            catch
            {
            }
        }
    }
}