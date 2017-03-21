﻿using System;
using System.Collections.Generic;
using PaketJunge.Model;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;

namespace PaketJunge.ViewModel
{
	public class EthernetViewModel : Layer2ViewModel
	{
		public string SourceMAC { get { return this.sourceMAC; } set { SetField<string>(ref this.sourceMAC, value, "SourceMAC"); } }
		private string sourceMAC;

		public string DestinationMAC { get { return this.destinationMAC; } set { SetField<string>(ref this.destinationMAC, value, "DestinationMAC"); } }
		private string destinationMAC;

		public List<string> Types { get { return this.types; } set { SetField<List<string>>(ref this.types, value, "Types"); } }
		private List<string> types;

		public string SelectedType { get { return this.selectedType; } set { SetField<string>(ref this.selectedType, value, "SelectedType"); } }
		private string selectedType;

		public EthernetViewModel()
		{
			this.destinationMAC = "00:11:22:33:44:55";
			this.types = EthernetModel.GetPacketTypes();
		}

		public override ILayer GetFrame()
		{
			return new EthernetLayer()
			{
				Source = new MacAddress(this.SourceMAC.Replace("-", ":")),
				Destination = new MacAddress(this.DestinationMAC.Replace("-", ":")),
				EtherType = (EthernetType)Enum.Parse(typeof(EthernetType), this.SelectedType)
			};
		}
	}
}
