using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using AndroidX.AppCompat.App;
using INetApp.Droid.Services;
using INetApp.Extensions;
using INetApp.Services;
using INetApp.Resources;
using System.Collections.Generic;

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
            Tasks.RunDelay(0, InvokeMainActivity);
        }
        protected override void OnResume()
        {
            base.OnResume();
            Xamarin.Forms.DependencyService.RegisterSingleton<IDeviceService>(new DeviceService(this));
            if (Intent.Extras != null)
            {
                PushNotificationAndroid pushNotificationAndroid = new PushNotificationAndroid(this);
                Xamarin.Forms.DependencyService.RegisterSingleton<IPushNotification>(pushNotificationAndroid);
                IDictionary<string, string> data = new Dictionary<string, string>();
                foreach (string key in Intent.Extras.KeySet())
                {
                    object value = Intent.Extras.Get(key);
                    data.Add(key, value.ToString());
                    Log.Debug("SplashActivity", "Key: {0} Value: {1}", key, value.ToString());
                }
                pushNotificationAndroid.OnPushAction(data);
            }
        }
        private void InvokeMainActivity()
        {
            StartActivity(new Intent(this, typeof(MainActivity)));
        }     
    }
}