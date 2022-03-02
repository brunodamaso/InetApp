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
    public partial class ItemTableRow : ContentView
    {
    
        public ItemTableRow()
        {
            InitializeComponent();
        }
        private void Label_Focused(object sender, FocusEventArgs e)
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                Entry entry = (Entry)sender;
                entry.CursorPosition = 0;
                entry.SelectionLength = entry.Text != null ? entry.Text.Length : 0;

            });
        }
    }
}