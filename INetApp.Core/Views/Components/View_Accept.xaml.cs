using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INetApp.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View_Accept : ContentView
    { 
        public static readonly BindableProperty ApproveProperty = BindableProperty.Create(nameof(Approve), typeof(Command), typeof(Button), null);

        public Command Approve
        {
            get => (Command)GetValue(ApproveProperty);
            set => SetValue(ApproveProperty, value);
        }

        public View_Accept()
        {
            InitializeComponent();
        }
    }
}