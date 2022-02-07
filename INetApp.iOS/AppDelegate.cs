using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Foundation;
using INetApp.iOS.Extensions;
using INetApp.iOS.Services;
using INetApp.Services.Push;
using UIKit;
using UserNotifications;
using Xamarin.Essentials;

namespace INetApp.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            PushBootstrap.Begin(() => new DeviceInstallationService());
            if (DeviceInstallationService().NotificationsSupported)
            {
                UNUserNotificationCenter.Current.RequestAuthorization(
                        UNAuthorizationOptions.Alert |
                        UNAuthorizationOptions.Badge |
                        UNAuthorizationOptions.Sound,
                        (approvalGranted, error) =>
                        {
                            if (approvalGranted && error == null)
                            {
                                RegisterForRemoteNotifications();
                            }
                        });
            }

            LoadApplication(new App());
            using (NSDictionary userInfo = options?.ObjectForKey(UIApplication.LaunchOptionsRemoteNotificationKey) as NSDictionary)
            {
                ProcessNotificationActions(userInfo);
            }

            return base.FinishedLaunching(app, options);
        }

        private IPushNotificationActionService _notificationActionService;
        private INotificationRegistrationService _notificationRegistrationService;
        private IDeviceInstallationService _deviceInstallationService;

        private IPushNotificationActionService NotificationActionService()
        {
            return _notificationActionService ?? (_notificationActionService =
                ServiceContainer.Resolve<IPushNotificationActionService>());
        }

        private INotificationRegistrationService NotificationRegistrationService()
        {
            return _notificationRegistrationService ?? (_notificationRegistrationService =
                ServiceContainer.Resolve<INotificationRegistrationService>());
        }

        private IDeviceInstallationService DeviceInstallationService()
        {
            return _deviceInstallationService ?? (_deviceInstallationService =
                ServiceContainer.Resolve<IDeviceInstallationService>());
        }

        private void RegisterForRemoteNotifications()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                UIUserNotificationSettings pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert |
                    UIUserNotificationType.Badge |
                    UIUserNotificationType.Sound,
                    new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            });
        }

        private Task CompleteRegistrationAsync(NSData deviceToken)
        {
            DeviceInstallationService().Token = deviceToken.ToHexString();
            return NotificationRegistrationService().RefreshRegistrationAsync();
        }

        private void ProcessNotificationActions(NSDictionary userInfo)
        {
            if (userInfo == null)
            {
                return;
            }

            try
            {
                NSString actionValue = userInfo.ObjectForKey(new NSString("action")) as NSString;

                if (!string.IsNullOrWhiteSpace(actionValue?.Description))
                {
                    NotificationActionService().TriggerAction(actionValue.Description);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            CompleteRegistrationAsync(deviceToken).ContinueWith((task)
                   =>
            {
                if (task.IsFaulted)
                {
                    throw task.Exception;
                }
            });
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            ProcessNotificationActions(userInfo);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            Debug.WriteLine(error.Description);
        }
    }
}
