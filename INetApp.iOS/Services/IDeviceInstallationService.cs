using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace InetApp.iOS.Services
{
	public interface IDeviceInstallationService
	{
		string Token { get; set; }
		bool NotificationsSupported { get; }
		string GetDeviceId();
		//bd DeviceInstallation GetDeviceInstallation(params string[] tags);
	}
}