using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Foundation;
using INetApp.iOS.Services;
using INetApp.Services;
using LocalAuthentication;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceService))]
namespace INetApp.iOS.Services
{
    public class DeviceService : IDeviceService
    {
        /// <summary>
        /// Gets the phone identifier.
        /// </summary>
        /// <value>The phone identifier.</value>
        public string DispositivoID => UIDevice.CurrentDevice.IdentifierForVendor.AsString();

        /// <summary>
        /// Gets the app identifier.
        /// </summary>
        /// <value>The app identifier.</value>
        public string AppID => NSBundle.MainBundle.InfoDictionary["CFBundleIdentifier"].ToString();

        /// <summary>
        /// Gets the app version.
        /// </summary>
        /// <value>The app version.</value>f
        public string AppVersion => NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name => UIDevice.CurrentDevice.Model + " " + UIDevice.CurrentDevice.SystemVersion;

        /// <summary>
        /// Gets the version so.
        /// </summary>
        /// <value>The version so.</value>
        public string VersionSO => UIDevice.CurrentDevice.SystemVersion;

        /// <summary>
        /// Gets the platform.
        /// </summary>
        /// <value>The platform.</value>
        public string Platform => "IOS";

        /// <summary>
        /// Gets the name of the app.
        /// </summary>
        /// <value>The name of the app.</value>
        public string AppName => NSBundle.MainBundle.InfoDictionary["CFBundleName"].ToString();

        /// <summary>
        /// Gets the top safe area.
        /// </summary>
        /// <value>The top safe area.</value>
        public double TopSafeArea
        {
            get
            {
                var result = UIApplication.SharedApplication?.StatusBarFrame.Height ?? 0d;

                if (UIApplication.SharedApplication?.Delegate?.GetWindow() is UIWindow window)
                {
                    if (UIDevice.CurrentDevice.CheckSystemVersion(11, 11))
                        result = window.SafeAreaInsets.Top;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the botton safe area.
        /// </summary>
        /// <value>The botton safe area.</value>
        public double BottonSafeArea
        {
            get
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 11))
                    return UIApplication.SharedApplication?.Delegate?.GetWindow()?.SafeAreaInsets.Bottom ?? 0d;

                return 0d;
            }
        }

        /// <summary>
        /// Gets the user agent.
        /// </summary>
        /// <value>The user agent.</value>
        public string UserAgent { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:XamarinFormsLibrary.iOS.Services.DeviceService"/> has gps.
        /// </summary>
        /// <value><c>true</c> if has gps; otherwise, <c>false</c>.</value>
        public bool HasGps => true;

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:XamarinFormsLibrary.iOS.Services.DeviceService"/> has biometric.
        /// </summary>
        /// <value><c>true</c> if has biometric; otherwise, <c>false</c>.</value>
        public bool HasBiometric
        {
            get
            {
                var context = new LAContext();
                context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out _);
                return context.BiometryType != LABiometryType.None;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:XamarinFormsLibrary.iOS.Services.DeviceService"/> has notifications.
        /// </summary>
        /// <value><c>true</c> if has notifications; otherwise, <c>false</c>.</value>
        public bool HasNotifications
        {
            get
            {
                var result = false;

                try
                {
                    var types = UIApplication.SharedApplication?.CurrentUserNotificationSettings?.Types;
                    result = types.HasValue && types.Value.HasFlag(UIUserNotificationType.Alert);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"ERR to acces to UIUserNotificationType.Alert -> {ex}");
                }


                return result;
            }
        }

        /// <summary>
        /// Shows the setting screen app.
        /// </summary>
        public void ShowSettingScreenApp()
        {
            UIApplication.SharedApplication.OpenUrl(NSUrl.FromString(UIApplication.OpenSettingsUrlString));
        }

        /// <summary>
        /// Gets the debug data.
        /// </summary>
        /// <returns>The debug data.</returns>
        public string GetDebugData()
        {
            return $"Device Data: \n{{\nDispositivoID:{DispositivoID},\nAppVersion:{AppVersion},\nName:{Name},\nversionSO:{VersionSO},\nplatform:{Platform},\nAppName:{AppName},\nTopSafeArea:{TopSafeArea},\nBottonSafeArea:{BottonSafeArea},\n}}";
        }

        /// <summary>
        /// Closes the app.
        /// </summary>
        public void CloseApp()
        {
            Thread.CurrentThread.Abort();
        }

        internal void Init()
        {
            UIApplication.SharedApplication.InvokeOnMainThread(delegate
            {
                if (new UIWebView().EvaluateJavascript("navigator.userAgent") is string val)
                    this.UserAgent = val;
            });
        }
    }

}