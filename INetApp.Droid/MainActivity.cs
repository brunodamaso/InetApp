using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Firebase.Iid;
using INetApp.Droid.Services;
using INetApp.NFC;
using INetApp.Services;
using INetApp.Services.Push;
using Xamarin.Forms.Platform.Android;

namespace INetApp.Droid.Activities
{
    [Activity(
        Label = "INetApp",
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme",
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    [IntentFilter(new[] { NfcAdapter.ActionNdefDiscovered }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = "*/*")]

    public class MainActivity : FormsAppCompatActivity, Android.Gms.Tasks.IOnSuccessListener
    {
        private IPushNotificationActionService _notificationActionService;
        private IDeviceInstallationService _deviceInstallationService;

        private IPushNotificationActionService NotificationActionService
            => _notificationActionService ??
                (_notificationActionService =
                ServiceContainer.Resolve<IPushNotificationActionService>());

        private IDeviceInstallationService DeviceInstallationService
            => _deviceInstallationService ??
                (_deviceInstallationService =
                ServiceContainer.Resolve<IDeviceInstallationService>());

        protected override void OnCreate(Bundle bundle)
        {
            //FormsAppCompatActivity.ToolbarResource = Resource.Layout.Toolbar;
            //FormsAppCompatActivity.TabLayoutResource = Resource.Layout.Tabs;

            base.OnCreate(bundle);

            CrossNFC.Init(this);

            Xamarin.Essentials.Platform.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::ZXing.Net.Mobile.Forms.Android.Platform.Init();

            SupportActionBar.SetDisplayShowHomeEnabled(true); // Show or hide the default home button
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowCustomEnabled(true); // Enable overriding the default toolbar layout
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            Xamarin.Forms.DependencyService.RegisterSingleton<IDeviceService>(new DeviceService(this));

            PushBootstrap.Begin(() => new DeviceInstallationService());
            if (DeviceInstallationService.NotificationsSupported)
            {
                FirebaseInstanceId.GetInstance(Firebase.FirebaseApp.Instance)
                    .GetInstanceId()
                    .AddOnSuccessListener(this);
            }

            LoadApplication(new App());

            ProcessNotificationActions(Intent);

            Window window = Window;
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.SetStatusBarColor(Android.Graphics.Color.Rgb(0, 166, 156));
        }

        /// <summary>
        /// FFImageLoading image service preserves in heap memory of the device every image newly downloaded 
        /// from url. In order to avoid application crash, you should reclaim memory in low memory situations.
        /// </summary>
        public override void OnTrimMemory([GeneratedEnum] TrimMemory level)
        {
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            base.OnTrimMemory(level);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnResume()
        {
            base.OnResume();
            CrossNFC.OnResume();
        }
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            CrossNFC.OnNewIntent(intent);
            ProcessNotificationActions(intent);
        }
        public void OnSuccess(Java.Lang.Object result)
        {
            DeviceInstallationService.Token = result.Class.GetMethod("getToken").Invoke(result).ToString();
        }

        private void ProcessNotificationActions(Intent intent)
        {
            try
            {
                if (intent?.HasExtra("action") == true)
                {
                    string action = intent.GetStringExtra("action");

                    if (!string.IsNullOrEmpty(action))
                    {
                        NotificationActionService.TriggerAction(action);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}

