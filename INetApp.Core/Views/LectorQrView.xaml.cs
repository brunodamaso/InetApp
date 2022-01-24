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
            //var scan = new ZXingScannerPage();

            //Navigation.PushModalAsync(scan);
            //string aa;
            //scan.OnScanResult += (result) =>
            //{
            //    Device.BeginInvokeOnMainThread(async () =>
            //    {
            //        await Navigation.PopModalAsync();
            //        if (!string.IsNullOrEmpty(result.Text))
            //            aa = result.Text;
            //    });
            //};
        }

        private void Scanner_OnScanResult(ZXing.Result result)
        {
            Scanner.IsAnalyzing = false;
            Scanner.IsScanning = false;
            Scanner.IsEnabled = false;            
        }
    }
}