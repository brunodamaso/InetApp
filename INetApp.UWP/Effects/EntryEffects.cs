using System;
using System.ComponentModel;
using INetApp;
using INetApp.Extensions;
using INetApp.Services;
using Xamarin.Forms;
using EntryEffectForms = INetApp.Effects.EntryEffect;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms.PlatformConfiguration;
using INetApp.UWP.Effects;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Entry), typeof(EntryEffect))]
[assembly: Xamarin.Forms.ExportEffect(typeof(INetApp.UWP.Effects.EntryEffect), "EntryEffect")]
namespace INetApp.UWP.Effects
{
    class EntryEffect : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> args)
        {
            base.OnElementChanged(args);

            if (Control != null)
            {
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
            }
        }
    }
}


