using System;
using System.Collections.Generic;
using PaketJunge.Model;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;

namespace PaketJunge.ViewModel
{
	public class IPv4ViewModel : Layer3ViewModel
	{
		public string SourceIP { get { return this.sourceIP; } set { SetField<string>(ref this.sourceIP, value, "SourceIP"); } }
		private string sourceIP;

		public string DestinationIP { get { return this.destinationIP; } set { SetField<string>(ref this.destinationIP, value, "DestinationIP"); } }
		private string destinationIP;

		public ushort DiffServ { get { return this.diffServ; } set { SetField<ushort>(ref this.diffServ, value, "DiffServ"); } }
		private ushort diffServ;

		public int FragmentOffset { get { return this.fragmentOffset; } set { SetField<int>(ref this.fragmentOffset, value, "FragmentOffset"); } }
		private int fragmentOffset;

		public ushort Id { get { return this.id; } set { SetField<ushort>(ref this.id, value, "Id"); } }
		private ushort id;

		public ushort TimeToLive { get { return this.timeToLive; } set { SetField<ushort>(ref this.timeToLive, value, "TimeToLive"); } }
		private ushort timeToLive;

		public List<string> Protocols { get { return this.protocols; } set { SetField<List<string>>(ref this.protocols, value, "Protocols"); } }
		private List<string> protocols;

		public string SelectedProtocol { get { return this.selectedProtocol; } set { SetField<string>(ref this.selectedProtocol, value, "SelectedProtocol"); } }
		private string selectedProtocol;

		public IPv4ViewModel()
		{
			this.sourceIP = "127.0.0.1";
			this.destinationIP = "127.0.0.1";
			this.protocols = IPv4Model.GetProtocols();
            this.timeToLive = 128;
		}

		//TODO: fragment flags?
		public override ILayer GetPacket()
		{
			var ipv4Layer = new IpV4Layer()
			{
				Source = new IpV4Address(this.SourceIP),
				CurrentDestination = new IpV4Address(this.DestinationIP),
				TypeOfService = (byte)this.DiffServ,
				Identification = this.Id,
				Ttl = (byte)this.TimeToLive,
				Protocol = (IpV4Protocol)Enum.Parse(typeof(IpV4Protocol), this.SelectedProtocol)
			};

			return ipv4Layer;
		}
	}
}
