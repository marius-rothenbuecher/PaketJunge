using System;
using System.Collections.Generic;
using PcapDotNet.Packets.IpV4;

namespace PaketJunge.Model
{
	public class IPv4Model
	{
		public static List<string> GetProtocols()
		{
			var protocols = Enum.GetValues(typeof(IpV4Protocol));
			var protocolList = new List<string>();

			foreach (var protcol in protocols)
				protocolList.Add(protcol.ToString());

			protocolList.Sort();

			return protocolList;
		}
	}
}
