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
            AndroidNotificationManager.Activate(this);
            Log.Debug("SplashActivity" , "InstanceID token: " + AndroidNotificationManager.GetToken()); 
            //InvokeMainActivity();

        }
        protected override void OnResume()
        {
            base.OnResume();
            Xamarin.Forms.DependencyService.RegisterSingleton<IDeviceService>(new DeviceService(this));
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

        #region Push
        private static readonly string TAG = "MainActivity";

        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;
        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
}

            NotificationChannel channel = new NotificationChannel(CHANNEL_ID,
                                                  "FCM Notifications",
                                                  NotificationImportance.Default)
            {

                Description = "Firebase Cloud Messages appear in this channel"
            };

            NotificationManager notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        //public bool IsPlayServicesAvailable()
        //{
        //    int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
        //    if (resultCode != ConnectionResult.Success)
        //    {
        //        if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
        //        {
        //            Console.WriteLine(GoogleApiAvailability.Instance.GetErrorString(resultCode));
        //        }
        //        else
        //        {
        //            Console.WriteLine("This device is not supported");
        //            Finish();
        //        }
        //        return false;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Google Play Services is available.");
        //        return true;
        //    }
        //}
        #endregion
    }
}