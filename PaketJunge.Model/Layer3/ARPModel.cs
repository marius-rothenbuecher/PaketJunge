using System;
using System.Collections.Generic;
using PcapDotNet.Packets.Arp;

namespace PaketJunge.Model
{
	public class ARPModel
	{
		public static List<string> GetOperations()
		{
			var operations = Enum.GetValues(typeof(ArpOperation));
			var operationList = new List<string>();

			foreach (var operation in operations)
				operationList.Add(operation.ToString());

			operationList.Sort();

			return operationList;
		}
	}
}
