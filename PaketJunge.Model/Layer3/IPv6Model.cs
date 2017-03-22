using System;
using System.Collections.Generic;
using PcapDotNet.Packets.IpV4;

namespace PaketJunge.Model
{
	public class IPv6Model
	{
		public static List<string> GetProtocols()
		{
			Array values = Enum.GetValues(typeof(IpV4Protocol));
			var protocolList = new List<string>();

			foreach (IpV4Protocol type in values)
				protocolList.Add(type.ToString());

			protocolList.Sort();

			return protocolList;
		}
	}
}
