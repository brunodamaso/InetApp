namespace INetApp.Services
{
    /// <summary>
    /// Device service.
    /// </summary>
    public interface IDeviceService
    {
        /// <summary>
        /// Gets the phone identifier.
        /// </summary>
        /// <value>The phone identifier.</value>
        string DispositivoID { get; }

        /// <summary>
        /// Gets the app identifier.
        /// </summary>
        /// <value>The app identifier.</value>
        string AppID { get; }

        /// <summary>
        /// Gets the app version.
        /// </summary>
        /// <value>The app version.</value>
        string AppVersion { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the version so.
        /// </summary>
        /// <value>The version so.</value>
        string VersionSO { get; }

        /// <summary>
        /// Gets the platform.
        /// </summary>
        /// <value>The platform.</value>
        string Platform { get; }

        /// <summary>
        /// Gets the name of the app.
        /// </summary>
        /// <value>The name of the app.</value>
        string AppName { get; }

        /// <summary>
        /// Gets the top safe area.
        /// </summary>
        /// <value>The top safe area.</value>
        double TopSafeArea { get; }

        /// <summary>
        /// Gets the botton safe area.
        /// </summary>
        /// <value>The botton safe area.</value>
        double BottonSafeArea { get; }

        /// <summary>
        /// Show debug data with logService
        /// </summary>
        /// <returns>The debug data.</returns>
        string GetDebugData();

        /// <summary>
        /// Shows the setting screen app.
        /// </summary>
        void ShowSettingScreenApp();

        /// <summary>
        /// Gets the user agent.
        /// </summary>
        /// <value>The user agent.</value>
        string UserAgent { get; }

        /// <summary>
        /// Closes the app.
        /// </summary>
        void CloseApp();

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:XamarinFormsLibrary.Services.DeviceService"/> has gps.
        /// </summary>
        /// <value><c>true</c> if has gps; otherwise, <c>false</c>.</value>
        bool HasGps { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:XamarinFormsLibrary.Services.DeviceService"/> has biometric.
        /// </summary>
        /// <value><c>true</c> if has biometric; otherwise, <c>false</c>.</value>
        bool HasBiometric { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:XamarinFormsLibrary.Services.DeviceService"/> has notifications.
        /// </summary>
        /// <value><c>true</c> if has notifications; otherwise, <c>false</c>.</value>
        bool HasNotifications { get; }
    }
}
