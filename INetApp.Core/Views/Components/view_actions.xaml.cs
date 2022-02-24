using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INetApp.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View_Actions : ContentView
    {
        public static readonly BindableProperty RefuseProperty = BindableProperty.Create(nameof(Refuse), typeof(ICommand), typeof(Button), null);

        public ICommand Refuse
        {
            get => (ICommand)GetValue(RefuseProperty);
            set => SetValue(RefuseProperty, value);
        }
        public static readonly BindableProperty ApproveProperty = BindableProperty.Create(nameof(Approve), typeof(ICommand), typeof(Button), null);

        public ICommand Approve
        {
            get => (ICommand)GetValue(ApproveProperty);
            set => SetValue(ApproveProperty, value);
        }

        public View_Actions()
        {
            InitializeComponent();
        }
    }
}