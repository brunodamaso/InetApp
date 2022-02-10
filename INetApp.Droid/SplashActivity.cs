using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Nfc;
using Android.OS;
using Android.Util;
using AndroidX.AppCompat.App;
using INetApp.Droid.Services;
using INetApp.Extensions;
using INetApp.Services;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Media;
using Firebase;

namespace INetApp.Droid.Activities
{
    [Activity(
        Label = "INetApp",
        Icon = "@drawable/notif_large_icon",
        Theme = "@style/Theme.Splash",
        NoHistory = true,
        MainLauncher = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //new PushNotificationAndroid(this);
            //InvokeMainActivity();

        }
        protected override void OnResume()
        {
            base.OnResume();
            Xamarin.Forms.DependencyService.RegisterSingleton<IDeviceService>(new DeviceService(this));
            Xamarin.Forms.DependencyService.RegisterSingleton<IPushNotification>(new PushNotificationAndroid(this));
            if (Intent.Extras != null)
            {
                foreach (string key in Intent.Extras.KeySet())
                {
                    object value = Intent.Extras.Get(key);
                    Log.Debug("SplashActivity", "Key: {0} Value: {1}", key, value.ToString());
                }
            }
            Tasks.RunDelay(0, InvokeMainActivity);
        }

        private void InvokeMainActivity()
        {
            StartActivity(new Intent(this, typeof(MainActivity)));
        }     
    }
}