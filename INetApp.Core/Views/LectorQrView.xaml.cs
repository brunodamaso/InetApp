using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace INetApp.Views
{
    public partial class LectorQRView : ContentPageBase
    {
        public LectorQRView()
        {
            InitializeComponent();
            Scanner.IsTorchOn = true;
        }

        //private void Scanner_OnScanResult(ZXing.Result result)
        //{
        //    Scanner.IsAnalyzing = false;
        //    Scanner.IsScanning = false;
        //    Scanner.IsEnabled = false;
        //}
    }
}