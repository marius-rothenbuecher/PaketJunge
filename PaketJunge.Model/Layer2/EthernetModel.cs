using System;
using System.Collections.Generic;
using PcapDotNet.Packets.Ethernet;

namespace PaketJunge.Model
{
	public class EthernetModel
	{
		public static List<string> GetPacketTypes()
		{
			Array valuesEthernet = Enum.GetValues(typeof(EthernetType));
			var packetTypeList = new List<string>();

			foreach (EthernetType type in valuesEthernet)
				packetTypeList.Add(type.ToString());

			packetTypeList.Sort();

			return packetTypeList;
		}
	}
}
