using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using PaketJunge.Model;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Ethernet;

namespace PaketJunge.ViewModel
{
	public class ARPViewModel : Layer3ViewModel
	{
		public string SourceMAC { get { return this.sourceMAC; } set { SetField<string>(ref this.sourceMAC, value, "SourceMAC"); } }
		private string sourceMAC;

		public string DestinationMAC { get { return this.destinationMAC; } set { SetField<string>(ref this.destinationMAC, value, "DestinationMAC"); } }
		private string destinationMAC;

		public string SourceIP { get { return this.sourceIP; } set { SetField<string>(ref this.sourceIP, value, "SourceIP"); } }
		private string sourceIP;

		public string DestinationIP { get { return this.destinationIP; } set { SetField<string>(ref this.destinationIP, value, "DestinationIP"); } }
		private string destinationIP;

		public List<string> Types { get { return this.types; } set { SetField<List<string>>(ref this.types, value, "Types"); } }
		private List<string> types;

		public List<string> Operations { get { return this.operations; } set { SetField<List<string>>(ref this.operations, value, "Operations"); } }
		private List<string> operations;

		public string SelectedType { get { return this.selectedType; } set { SetField<string>(ref this.selectedType, value, "SelectedType"); } }
		private string selectedType;

		public string SelectedOperation { get { return this.selectedOperation; } set { SetField<string>(ref this.selectedOperation, value, "SelectedOperation"); } }
		private string selectedOperation;

		public ARPViewModel()
		{
			this.types = EthernetModel.GetPacketTypes();
			this.operations = ARPModel.GetOperations();
			this.sourceMAC = "00:00:00:00:00:00";
			this.destinationMAC = "00:00:00:00:00:00";
		}

		//TODO: fail
		public override ILayer GetPacket()
		{
			return new ArpLayer()
			{
				Operation = (ArpOperation)Enum.Parse(typeof(ArpOperation), this.SelectedOperation),
				ProtocolType = (EthernetType)Enum.Parse(typeof(EthernetType), this.SelectedType),
				SenderHardwareAddress = new ReadOnlyCollection<byte>(Encoding.Default.GetBytes(this.SourceMAC)),
				TargetHardwareAddress = new ReadOnlyCollection<byte>(Encoding.Default.GetBytes(this.DestinationMAC)),
				SenderProtocolAddress = new ReadOnlyCollection<byte>(IPAddress.Parse(this.SourceIP).GetAddressBytes()),
				TargetProtocolAddress = new ReadOnlyCollection<byte>(IPAddress.Parse(this.DestinationIP).GetAddressBytes()),
			};
		}
	}
}
