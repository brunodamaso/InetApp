using System;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Graphics;
using Android.OS;
//using Android.Support.V4.App;
using AndroidX.Core.App;
using Firebase.Iid;
using INetApp.Droid.Activities;
using AndroidApp = Android.App.Application;

namespace INetApp.Droid.Services
{
    public class AndroidNotificationManager
    {
        private const string channelId = "default";
        private const string channelName = "Default";
        private const string channelDescription = "The default channel for notifications.";
        private const int pendingIntentId = 0;

        public const string TitleKey = "title";
        public const string MessageKey = "message";
        private static bool channelInitialized = false;
        private int messageId = -1;
        private static NotificationManager manager;
        
        public static void Activate(SplashActivity splashActivity)
        {
            IsPlayServicesAvailable(splashActivity);
            CreateNotificationChannel();
        }

        public static string GetToken()
        {
            try
            {
                return FirebaseInstanceId.Instance.Token;
            }
            catch (Exception)
            {
                return "No FireBase no esta inicializado";
            }
        }
        private static void CreateNotificationChannel()
        {
            manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                Java.Lang.String channelNameJava = new Java.Lang.String(channelName);
                NotificationChannel channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                manager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }
        private static bool IsPlayServicesAvailable(SplashActivity splashActivity)
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
        
        public int CrearNotificacionLocal(string pTitle, string pBody, int numerOrders)
        {

            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            messageId++;

            Intent intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            intent.PutExtra(TitleKey, pTitle);
            intent.PutExtra(MessageKey, pBody);
            intent.AddFlags(ActivityFlags.ClearTop);

            PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, pendingIntentId, intent, PendingIntentFlags.UpdateCurrent);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(pTitle)
                .SetContentText(pBody)
                .SetAutoCancel(true)
                .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.ic_launcher))
                .SetSmallIcon(Resource.Drawable.ic_launcher)
                .SetLights(Color.Blue ,500 ,500)
                .SetVibrate(new long[] { 100, 250, 100, 250, 100, 250 })
                .SetNumber(numerOrders)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            Notification notification = builder.Build();
            manager.Notify(messageId, notification);

            return messageId;
        }
    }
}