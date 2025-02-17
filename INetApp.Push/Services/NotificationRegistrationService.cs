﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace INetApp.Services.Push
{
    public class NotificationRegistrationService : INotificationRegistrationService
    {
        private const string RequestUrl = "api/notifications/installations";
        private const string CachedDeviceTokenKey = "cached_device_token";
        private const string CachedTagsKey = "cached_tags";
        private readonly string _baseApiUrl;
        private readonly HttpClient _client;
        private IDeviceInstallationService _deviceInstallationService;

        public NotificationRegistrationService(string baseApiUri, string apiKey)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("apikey", apiKey);

            _baseApiUrl = baseApiUri;
        }

        private IDeviceInstallationService DeviceInstallationService
            => _deviceInstallationService ??
                (_deviceInstallationService = ServiceContainer.Resolve<IDeviceInstallationService>());

        public async Task DeregisterDeviceAsync()
        {
            string cachedToken = await SecureStorage.GetAsync(CachedDeviceTokenKey)
                .ConfigureAwait(false);

            if (cachedToken == null)
            {
                return;
            }

            string deviceId = DeviceInstallationService?.GetDeviceId();

            if (string.IsNullOrWhiteSpace(deviceId))
            {
                throw new Exception("Unable to resolve an ID for the device.");
            }

            await SendAsync(HttpMethod.Delete, $"{RequestUrl}/{deviceId}")
                .ConfigureAwait(false);

            SecureStorage.Remove(CachedDeviceTokenKey);
            SecureStorage.Remove(CachedTagsKey);
        }

        public async Task RegisterDeviceAsync(params string[] tags)
        {
            DeviceInstallation deviceInstallation = DeviceInstallationService?.GetDeviceInstallation(tags);

            await SendAsync<DeviceInstallation>(HttpMethod.Put, RequestUrl, deviceInstallation)
                .ConfigureAwait(false);

            await SecureStorage.SetAsync(CachedDeviceTokenKey, deviceInstallation.PushChannel)
                .ConfigureAwait(false);

            await SecureStorage.SetAsync(CachedTagsKey, JsonConvert.SerializeObject(tags));
        }

        public async Task RefreshRegistrationAsync()
        {
            string cachedToken = await SecureStorage.GetAsync(CachedDeviceTokenKey)
                .ConfigureAwait(false);

            string serializedTags = await SecureStorage.GetAsync(CachedTagsKey)
                .ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(cachedToken) ||
                string.IsNullOrWhiteSpace(serializedTags) ||
                string.IsNullOrWhiteSpace(DeviceInstallationService.Token) ||
                cachedToken == DeviceInstallationService.Token)
            {
                return;
            }

            string[] tags = JsonConvert.DeserializeObject<string[]>(serializedTags);

            await RegisterDeviceAsync(tags);
        }

        private async Task SendAsync<T>(HttpMethod requestType, string requestUri, T obj)
        {
            string serializedContent = null;

            await Task.Run(() => serializedContent = JsonConvert.SerializeObject(obj))
                .ConfigureAwait(false);

            await SendAsync(requestType, requestUri, serializedContent);
        }

        private async Task SendAsync(
            HttpMethod requestType,
            string requestUri,
            string jsonRequest = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(requestType, new Uri($"{_baseApiUrl}{requestUri}"));

            if (jsonRequest != null)
            {
                request.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            }

            HttpResponseMessage response = await _client.SendAsync(request).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }
    }
}
