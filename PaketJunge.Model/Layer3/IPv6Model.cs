using System;
using System.Collections.Generic;
using PcapDotNet.Packets.IpV4;

namespace PaketJunge.Model
{
	public class IPv6Model
	{
		public static List<string> GetProtocols()
		{
			var protcols = Enum.GetValues(typeof(IpV4Protocol));
			var protocolList = new List<string>();

			foreach (var protocol in protcols)
				protocolList.Add(protocol.ToString());

			protocolList.Sort();

			return protocolList;
		}
	}
}
