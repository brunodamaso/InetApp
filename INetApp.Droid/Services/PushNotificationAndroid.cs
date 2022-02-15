using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Graphics;
using Android.OS;
//using Android.Support.V4.App;
using AndroidX.Core.App;
using INetApp.Droid.Activities;
using INetApp.Services;
using Xamarin.Essentials;
using AndroidApp = Android.App.Application;

namespace INetApp.Droid.Services
{
    public class PushNotificationAndroid : PushService, IPushNotification
    {
        private const string channelId = "10001";
        private const string channelDescription = "The default channel for notifications.";
        private const int pendingIntentId = 0;

        public const string TitleKey = "title";
        public const string MessageKey = "message";
        private static bool channelInitialized = false;
        private int messageId = -1;
        private static NotificationManager manager;
        private readonly Context context;

        public PushNotificationAndroid()
        {
        }
        public PushNotificationAndroid(Context _context)
        {
            context = _context;
            Activate();
        }
        private void Activate()
        {
            IsPlayServicesAvailable(context);
            CreateNotificationChannel();
        }

        public string GetToken()
        {
            return Preferences.Get("TokenFirebase", "FireBase no esta inicializado");
        }

        private static void CreateNotificationChannel()
        {
            manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                //Java.Lang.String channelNameJava = new Java.Lang.String(channelName);
                NotificationChannel channel = new NotificationChannel(channelId, "Notificaciones generales", NotificationImportance.High)
                {
                    Description = channelDescription,
                };
                channel.EnableLights(true);
                channel.LightColor = Color.Red;
                channel.SetVibrationPattern(new long[] { 100, 250, 100, 250, 100, 250 });
                channel.EnableVibration(true);

                manager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }
        private static bool IsPlayServicesAvailable(Context splashActivity)
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(splashActivity);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    Console.WriteLine(GoogleApiAvailability.Instance.GetErrorString(resultCode));
                }
                else
                {
                    Console.WriteLine("This device is not supported");
                    //Finish();
                }
                return false;
            }
            else
            {
                Console.WriteLine("Google Play Services is available.");
                return true;
            }
        }

        public int CrearNotificacionLocal(string pTitle, string pBody, IDictionary<string, string> data)
        {
            //todo NotificationsListenerService
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            messageId++;

            Intent intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            intent.PutExtra(TitleKey, pTitle);
            intent.PutExtra(MessageKey, pBody);
            foreach (var item in data)
            {
                intent.PutExtra(item.Key, item.Value.ToString());
            }
            intent.AddFlags(ActivityFlags.ClearTop);

            PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, pendingIntentId, intent, PendingIntentFlags.UpdateCurrent);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(pTitle)
                .SetContentText(pBody)
                .SetAutoCancel(true)
                .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.ic_launcher))
                .SetSmallIcon(Resource.Drawable.ic_launcher)
                .SetLights(Color.Blue, 500, 500)
                .SetVibrate(new long[] { 100, 250, 100, 250, 100, 250 })
                //                .SetNumber(numerOrders)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            Notification notification = builder.Build();
            manager.Notify(messageId, notification);

            return messageId;
        }

        public override bool OnPushAction(IDictionary<string, string> token)
        {
            return base.OnPushAction(token);
        }
    }
}