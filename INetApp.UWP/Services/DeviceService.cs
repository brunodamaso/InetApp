using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INetApp.Services;
using INetApp.UWP.Services;
using Windows.Foundation.Metadata;
using Windows.System.Profile;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceService))]
namespace INetApp.UWP.Services
{
    public class DeviceService : IDeviceService
    {
        private string id = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:XamarinFormsLibrary.Droid.Services.DeviceService"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        public DeviceService() //Context context
        {
            //this.context = context;
        }

        /// <summary>
        /// Gets the phone identifier.
        /// </summary>
        /// <value>The phone identifier.</value>
        public string DispositivoID
        {
            get
            {
                if (id != null)
                    return id;

                try
                {
                    if (ApiInformation.IsTypePresent("Windows.System.Profile.SystemIdentification"))
                    {
                        var systemId = SystemIdentification.GetSystemIdForPublisher();

                        // Make sure this device can generate the IDs
                        if (systemId.Source != SystemIdentificationSource.None)
                        {
                            // The Id property has a buffer with the unique ID
                            var hardwareId = systemId.Id;
                            var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

                            var bytes = new byte[hardwareId.Length];
                            dataReader.ReadBytes(bytes);

                            id = Convert.ToBase64String(bytes);
                        }
                    }

                    if (id == null && ApiInformation.IsTypePresent("Windows.System.Profile.HardwareIdentification"))
                    {
                        var token = HardwareIdentification.GetPackageSpecificToken(null);
                        var hardwareId = token.Id;
                        var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

                        var bytes = new byte[hardwareId.Length];
                        dataReader.ReadBytes(bytes);

                        id = Convert.ToBase64String(bytes);
                    }

                    if (id == null)
                    {
                        id = "unsupported";
                    }

                }
                catch (Exception)
                {
                    id = "unsupported";
                }

                return id;
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
                    appVersion = "";
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
                    appVersion = "";
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERR getting App version " + e.Message);
                }

                return appVersion;
            }
        }

        private string Manufacturer => "";

        private string Model => "";

        private string MacAddress
        {
            get
            {
                //var manager = context.GetSystemService(Context.WifiService) as WifiManager;
                //var info = manager?.ConnectionInfo;
                //return info?.MacAddress ?? "NOT_MAC";
                return "";
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name => this.Manufacturer + " " + this.Model;

        /// <summary>
        /// Gets the version so.
        /// </summary>
        /// <value>The version so.</value>
        public string VersionSO => "";

        /// <summary>
        /// Gets the platform.
        /// </summary>
        /// <value>The platform.</value>
        public string Platform => "UWP";

        /// <summary>
        /// Gets the name of the app.
        /// </summary>
        /// <value>The name of the app.</value>
        public string AppName
        {
            get
            {

                return "";
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
        public string Density => "";

        /// <summary>
        /// Gets the user agent.
        /// </summary>
        public string UserAgent => "";

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:XamarinFormsLibrary.Droid.Services.DeviceService"/> has gps.
        /// </summary>
        /// <value><c>true</c> if has gps; otherwise, <c>false</c>.</value>
        public bool HasGps
        {
            get
            {

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:XamarinFormsLibrary.Droid.Services.DeviceService"/> has biometric.
        /// </summary>
        /// <value><c>true</c> if has biometric; otherwise, <c>false</c>.</value>
        public bool HasBiometric
        {
            get
            {
                return false;

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
                return false;

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
            //var intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings,
            //                        Android.Net.Uri.Parse($"package:{Application.Context.PackageName}"));
            //context.StartActivity(intent);
        }

        /// <summary>
        /// Gets the debug data.
        /// </summary>
        /// <returns>The debug data.</returns>
        public string GetDebugData()
        {
            return $"Device Data: \n{{\nPhoneID:{DispositivoID},\nAppVersion:{AppVersion},\nName:{Name},\nversionSO:{VersionSO},\nplatform:{Platform},\nAppName:{AppName},\nDensity:{Density},\n}}";
        }

        /// <summary>
        /// Closes the app.
        /// </summary>
        public void CloseApp()
        {
            //if (context is Activity activity)
            //{
            //    activity.Finish();
            //    Process.KillProcess(Process.MyPid());
            //}
        }

        /// <summary>
        /// Get real Metrics
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        //public static DisplayMetrics GetRealMetrics(Context context)
        //{
        //    var displayMetrics = new DisplayMetrics();
        //    // access physical pixels - some devices (e.g. Nexus 9) return a too small pixel height otherwise
        //    GetWindowManager(context).DefaultDisplay.GetRealMetrics(displayMetrics);
        //    return displayMetrics;
        //}

        /// <summary>
        /// Get display Metrics
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        //public static DisplayMetrics GetDisplayMetrics(Context context)
        //{
        //    var displayMetrics = new DisplayMetrics();
        //    // access physical pixels - some devices (e.g. Nexus 9) return a too small pixel height otherwise
        //    GetWindowManager(context).DefaultDisplay.GetMetrics(displayMetrics);
        //    return displayMetrics;
        //}

        /// <summary>
        /// Get StatusBar Height
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        //public static int GetStatusBarHeight(Context context)
        //{
        //    var resources = context.Resources;
        //    int resourceId = resources.GetIdentifier("status_bar_height", "dimen", "android");
        //    return (resourceId > 0) ? resources.GetDimensionPixelSize(resourceId) : 0;
        //}

        /// <summary>
        /// Get NavigationBar Height
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        //public static int GetNavigationBarHeight(Context context)
        //{
        //    var resources = context.Resources;
        //    int resourceId = resources.GetIdentifier("navigation_bar_height", "dimen", "android");
        //    return (resourceId > 0) ? resources.GetDimensionPixelSize(resourceId) : 0;
        //}

        //private static IWindowManager GetWindowManager(Context context)
        //{
        //    var windowManager = context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

        //    return windowManager;
        //}
    }

}
