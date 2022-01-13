using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INetApp.Views
{
    public partial class LectorQRView : ContentPageBase
    {
        public LectorQRView()
        {
            InitializeComponent();
        }

        private void CameraView_MediaCaptured(object sender, Xamarin.CommunityToolkit.UI.Views.MediaCapturedEventArgs e)
        {

        }
    }
}