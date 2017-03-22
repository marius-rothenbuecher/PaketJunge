using System;
using System.Collections.Generic;
using PaketJunge.Model;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;

namespace PaketJunge.ViewModel
{
	public class IPv4ViewModel : Layer3ViewModel
	{
		public string SourceIP { get { return this.sourceIP; } set { SetField<string>(ref this.sourceIP, value, nameof(this.SourceIP)); } }
		private string sourceIP;

		public string DestinationIP { get { return this.destinationIP; } set { SetField<string>(ref this.destinationIP, value, nameof(this.DestinationIP)); } }
		private string destinationIP;

		public ushort DiffServ { get { return this.diffServ; } set { SetField<ushort>(ref this.diffServ, value, nameof(this.DiffServ)); } }
		private ushort diffServ;

		public ushort FragmentOffset { get { return this.fragmentOffset; } set { SetField<ushort>(ref this.fragmentOffset, value, nameof(this.FragmentOffset)); } }
		private ushort fragmentOffset;

		public ushort Id { get { return this.id; } set { SetField<ushort>(ref this.id, value, nameof(this.Id)); } }
		private ushort id;

		public ushort TimeToLive { get { return this.timeToLive; } set { SetField<ushort>(ref this.timeToLive, value, nameof(this.TimeToLive)); } }
		private ushort timeToLive;

        public bool DontFragment { get { return this.dontFragment; } set { SetField<bool>(ref this.dontFragment, value, nameof(this.DontFragment)); } }
        private bool dontFragment;

        public bool MoreFragments { get { return this.moreFragments; } set { SetField<bool>(ref this.moreFragments, value, nameof(this.MoreFragments)); } }
        private bool moreFragments;

        public List<string> Protocols { get { return this.protocols; } set { SetField<List<string>>(ref this.protocols, value, nameof(this.Protocols)); } }
		private List<string> protocols;

		public string SelectedProtocol { get { return this.selectedProtocol; } set { SetField<string>(ref this.selectedProtocol, value, nameof(this.SelectedProtocol)); } }
		private string selectedProtocol;

		public IPv4ViewModel()
		{
			this.sourceIP = "127.0.0.1";
			this.destinationIP = "127.0.0.1";
			this.protocols = IPv4Model.GetProtocols();
            this.selectedProtocol = nameof(IpV4Protocol.Ip);
            this.timeToLive = 128;
		}
        
		public override ILayer GetPacket()
		{
            var fragmentationOptions = IpV4FragmentationOptions.None;

            if (this.DontFragment)
                fragmentationOptions |= IpV4FragmentationOptions.DoNotFragment;

            if (this.MoreFragments)
                fragmentationOptions |= IpV4FragmentationOptions.MoreFragments;

            var ipv4Layer = new IpV4Layer()
            {
                Source = new IpV4Address(this.SourceIP),
                CurrentDestination = new IpV4Address(this.DestinationIP),
                TypeOfService = (byte)this.DiffServ,
                Identification = this.Id,
                Ttl = (byte)this.TimeToLive,
                Protocol = (IpV4Protocol)Enum.Parse(typeof(IpV4Protocol), this.SelectedProtocol),
                Fragmentation = new IpV4Fragmentation(fragmentationOptions, this.FragmentOffset)
			};

			return ipv4Layer;
		}
	}
}
