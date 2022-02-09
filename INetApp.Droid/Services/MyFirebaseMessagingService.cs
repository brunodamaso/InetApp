using System;
using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Messaging;
using Xamarin.Essentials;

namespace INetApp.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        private const string TAG = "MyFirebaseMsgService";
        private readonly AndroidNotificationManager androidNotification = new AndroidNotificationManager();
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
            foreach (var item in message.Data)
            {
                Log.Debug(TAG, "Notification Message Data: key " + item.Key +" Valor " +item.Value);
            }
            androidNotification.CrearNotificacionLocal(message.GetNotification().Title, message.GetNotification().Body ,0);
        }
        public override void OnNewToken(string token)
        {
            Console.WriteLine("[Toookennn]:  " + token);
            base.OnNewToken(token);

            Preferences.Set("TokenFirebase", token);
            sedRegisterToken(token);
        }
        public void sedRegisterToken(string token)
        {
            //Tu código para registrar el token a tu servidor y base de datos
        }
    }
}