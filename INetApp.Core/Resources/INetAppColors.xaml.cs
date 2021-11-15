using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INetApp.Resources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class INetAppColors : ResourceDictionary
    {
        public INetAppColors()
        {
            InitializeComponent();
        }
    }
}