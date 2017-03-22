using System;
using System.Collections.Generic;
using PcapDotNet.Packets.Ethernet;

namespace PaketJunge.Model
{
    public class EthernetModel
    {
		public static List<string> GetPacketTypes()
		{
			var types = Enum.GetValues(typeof(EthernetType));
			var typeList = new List<string>();

			foreach (var type in types)
				typeList.Add(type.ToString());

			typeList.Sort();

			return typeList;
		}
	}
}
