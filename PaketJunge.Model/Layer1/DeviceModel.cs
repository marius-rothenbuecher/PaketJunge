using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SharpPcap;

namespace PaketJunge.Model
{
	public class DeviceModel
	{
		public static List<string> GetDevices()
		{
			var devices = CaptureDeviceList.Instance;
			var deviceList = new List<string>();

			for (int i = 0; i < devices.Count; i++)
			{
				// Extracts device description
				string deviceDescription = Regex.Match(devices[i].Description, "'(.*)'").Groups[1].Value;
				deviceList.Add(deviceDescription);
			}

			return deviceList;
		}

		public static List<string> GetStandards()
		{
			Array values = Enum.GetValues(typeof(StandardType));
			var standardList = new List<string>();

			foreach (StandardType type in values)
				standardList.Add(type.ToString());

			return standardList;
		}
	}
}
