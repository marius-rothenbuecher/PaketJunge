using System;
using System.Collections.Generic;
using PcapDotNet.Packets.Arp;

namespace PaketJunge.Model
{
	public class ARPModel
	{
		public static List<string> GetOperations()
		{
			Array valuesEthernet = Enum.GetValues(typeof(ArpOperation));
			var operationList = new List<string>();

			foreach (ArpOperation type in valuesEthernet)
				operationList.Add(type.ToString());

			operationList.Sort();

			return operationList;
		}
	}
}
