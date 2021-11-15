using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using AndroidX.Core.App;
using AndroidX.Core.Hardware.Fingerprint;
using INetApp.Droid.Services;
using INetApp.Extensions;
using INetApp.Services;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceService))]
namespace INetApp.Droid.Services
{
    public class DeviceService : IDeviceService
    {
        private const string SAMSUNG_NAME = "samsung";
        private readonly Context context;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:XamarinFormsLibrary.Droid.Services.DeviceService"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        public DeviceService(Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the phone identifier.
        /// </summary>
        /// <value>The phone identifier.</value>
        public string DispositivoID
        {
            get
            {
                string androidId = Android.Provider.Settings.Secure.GetString(context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
                string phoneID = androidId;// + this.Manufacturer + this.Model + androidId + this.MacAddress;
                return phoneID;
            }
        }

        /// <summary>
        /// Gets the app identifier.
        /// </summary>
        /// <value>The app identifier.</value>
        public string AppID
        {
            get
            {
                string appVersion = "NOT_APP_ID";

                try
                {
                    PackageInfo pInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
                    appVersion = pInfo.PackageName;
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERR getting app version " + e.Message);
                }

                return appVersion;
            }
        }

        /// <summary>
        /// Gets the app version.
        /// </summary>
        /// <value>The app version.</value>
        public string AppVersion
        {
            get
            {
                string appVersion = "NOT_APP_VERSION";

                try
                {
                    PackageInfo pInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
                    appVersion = pInfo.VersionName;
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERR getting App version " + e.Message);
                }

                return appVersion;
            }
        }

        private string Manufacturer => Build.Manufacturer;

        private string Model => Build.Model;

        private string MacAddress
        {
            get
            {
                WifiManager manager = context.GetSystemService(Context.WifiService) as WifiManager;
                WifiInfo info = manager?.ConnectionInfo;
                return info?.MacAddress ?? "NOT_MAC";
            }
        }

        /// <summary>
        /// If is device samsung !!!!
        /// </summary>
        public bool IsSamsungDevice => this.Manufacturer.Contains(SAMSUNG_NAME, StringComparison.InvariantCultureIgnoreCase) || this.Model.Contains(SAMSUNG_NAME, StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name => this.Manufacturer + " " + this.Model;

        /// <summary>
        /// Gets the version so.
        /// </summary>
        /// <value>The version so.</value>
        public string VersionSO => Build.VERSION.Release;

        /// <summary>
        /// Gets the platform.
        /// </summary>
        /// <value>The platform.</value>
        public string Platform => "Android";

        /// <summary>
        /// Gets the name of the app.
        /// </summary>
        /// <value>The name of the app.</value>
        public string AppName
        {
            get
            {
                ApplicationInfo applicationInfo = context.ApplicationInfo;
                int stringId = applicationInfo.LabelRes;
                return stringId == 0 ? applicationInfo.NonLocalizedLabel.ToString() : context.GetString(stringId);
            }
        }

        /// <summary>
        /// Gets the top safe area.
        /// </summary>
        /// <value>The top safe area.</value>
        public double TopSafeArea => 0;

        /// <summary>
        /// Gets the botton safe area.
        /// </summary>
        /// <value>The botton safe area.</value>
        public double BottonSafeArea => 0;

        /// <summary>
        /// Gets the density of device.
        /// </summary>
        public string Density => context?.Resources?.DisplayMetrics?.DensityDpi.ToString() ?? "Not allowed.";

        /// <summary>
        /// Gets the user agent.
        /// </summary>
        public string UserAgent => Java.Lang.JavaSystem.GetProperty("http.agent");

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:XamarinFormsLibrary.Droid.Services.DeviceService"/> has gps.
        /// </summary>
        /// <value><c>true</c> if has gps; otherwise, <c>false</c>.</value>
        public bool HasGps
        {
            get
            {
                if (context.GetSystemService(Context.LocationService) is LocationManager mgr && !mgr.AllProviders.IsNullOrNotElements())
                {
                    return mgr.AllProviders.Contains(LocationManager.GpsProvider);
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:XamarinFormsLibrary.Droid.Services.DeviceService"/> has biometric.
        /// </summary>
        /// <value><c>true</c> if has biometric; otherwise, <c>false</c>.</value>
        [Obsolete]
        public bool HasBiometric
        {
            get
            {
                FingerprintManagerCompat fingerprintManagerCompat = FingerprintManagerCompat.From(context);

                return fingerprintManagerCompat.IsHardwareDetected;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:XamarinFormsLibrary.Droid.Services.DeviceService"/> has notifications.
        /// </summary>
        /// <value><c>true</c> if has notifications; otherwise, <c>false</c>.</value>
        public bool HasNotifications
        {
            get
            {
                bool result = false;

                try
                {
                    if (NotificationManagerCompat.From(Application.Context) is NotificationManagerCompat nm)
                    {
                        bool enabled = nm.AreNotificationsEnabled();
                        result = enabled;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERR to acces to AreNotificationsEnabled -> {ex}");
                }

                return result;
            }
        }

        /// <summary>
        /// Bytes the array to string.
        /// </summary>
        /// <returns>The array to string.</returns>
        /// <param name="ba">Ba.</param>
        public string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        /// <summary>
        /// Shows the setting screen app.
        /// </summary>
        public void ShowSettingScreenApp()
        {
            Intent intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings,
                                    Android.Net.Uri.Parse($"package:{Application.Context.PackageName}"));
            context.StartActivity(intent);
        }

        /// <summary>
        /// Gets the debug data.
        /// </summary>
        /// <returns>The debug data.</returns>
        public string GetDebugData()
        {
            return $"Device Data: \n{{\nDispositivoID:{this.DispositivoID},\nAppVersion:{this.AppVersion},\nName:{this.Name},\nversionSO:{this.VersionSO},\nplatform:{this.Platform},\nAppName:{this.AppName},\nDensity:{this.Density},\n}}";
        }

        /// <summary>
        /// Closes the app.
        /// </summary>
        public void CloseApp()
        {
            if (context is Activity activity)
            {
                activity.Finish();
                Process.KillProcess(Process.MyPid());
            }
        }

        /// <summary>
        /// Get real Metrics
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static DisplayMetrics GetRealMetrics(Context context)
        {
            DisplayMetrics displayMetrics = new DisplayMetrics();
            // access physical pixels - some devices (e.g. Nexus 9) return a too small pixel height otherwise
            GetWindowManager(context).DefaultDisplay.GetRealMetrics(displayMetrics);
            return displayMetrics;
        }

        /// <summary>
        /// Get display Metrics
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Obsolete]
        public static DisplayMetrics GetDisplayMetrics(Context context)
        {
            DisplayMetrics displayMetrics = new DisplayMetrics();
            // access physical pixels - some devices (e.g. Nexus 9) return a too small pixel height otherwise
            GetWindowManager(context).DefaultDisplay.GetMetrics(displayMetrics);
            return displayMetrics;
        }

        /// <summary>
        /// Get StatusBar Height
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int GetStatusBarHeight(Context context)
        {
            Android.Content.Res.Resources resources = context.Resources;
            int resourceId = resources.GetIdentifier("status_bar_height", "dimen", "android");
            return (resourceId > 0) ? resources.GetDimensionPixelSize(resourceId) : 0;
        }

        /// <summary>
        /// Get NavigationBar Height
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int GetNavigationBarHeight(Context context)
        {
            Android.Content.Res.Resources resources = context.Resources;
            int resourceId = resources.GetIdentifier("navigation_bar_height", "dimen", "android");
            return (resourceId > 0) ? resources.GetDimensionPixelSize(resourceId) : 0;
        }

        private static IWindowManager GetWindowManager(Context context)
        {
            IWindowManager windowManager = context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            return windowManager;
        }
    }
}