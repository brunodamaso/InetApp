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
        private readonly PushNotificationAndroid androidNotification = new PushNotificationAndroid();
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
            foreach (var item in message.Data)
            {
                Log.Debug(TAG, "Notification Message Data: key " + item.Key +" Valor " +item.Value);
            }
            androidNotification.CrearNotificacionLocal(message.GetNotification().Title, message.GetNotification().Body ,message.Data);
        }
        public override void OnNewToken(string token)
        {
            Console.WriteLine("[Token]:  " + token);
            base.OnNewToken(token);

            Preferences.Set("TokenRegsitration", token);
        }
        
    }
}