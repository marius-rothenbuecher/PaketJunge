using System;
using System.Collections.Generic;
using PaketJunge.Model;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV6;
using PcapDotNet.Packets.IpV4;

namespace PaketJunge.ViewModel
{
	public class IPv6ViewModel : Layer3ViewModel
	{
        public string SourceIP { get { return this.sourceIP; } set { SetField<string>(ref this.sourceIP, value, nameof(this.SourceIP)); } }
        private string sourceIP;

        public string DestinationIP { get { return this.destinationIP; } set { SetField<string>(ref this.destinationIP, value, nameof(this.DestinationIP)); } }
        private string destinationIP;

        public int FlowLabel { get { return this.flowLabel; } set { SetField<int>(ref this.flowLabel, value, nameof(this.FlowLabel)); } }
        private int flowLabel;

        public ushort HopLimit { get { return this.hopLimit; } set { SetField<ushort>(ref this.hopLimit, value, nameof(this.HopLimit)); } }
        private ushort hopLimit;

        public ushort TrafficClass { get { return this.trafficClass; } set { SetField<ushort>(ref this.trafficClass, value, nameof(this.TrafficClass)); } }
        private ushort trafficClass;

        public List<string> Protocols { get { return this.protocols; } set { SetField<List<string>>(ref this.protocols, value, nameof(this.Protocols)); } }
		private List<string> protocols;

		public string SelectedProtocol { get { return this.selectedProtocol; } set { SetField<string>(ref this.selectedProtocol, value, nameof(this.SelectedProtocol)); } }
		private string selectedProtocol;

        public IPv6ViewModel()
        {
            this.sourceIP = "fe80::1";
            this.destinationIP = "fe80::1";
            this.protocols = IPv6Model.GetProtocols();
            this.selectedProtocol = nameof(IpV4Protocol.Ip);
            this.hopLimit = 128;
            this.flowLabel = 0;
            this.trafficClass = 0;
        }

        // TODO: add extension heades
        public override ILayer GetPacket()
		{
            return new IpV6Layer()
            {
                CurrentDestination = new IpV6Address(this.DestinationIP),
                ExtensionHeaders = new IpV6ExtensionHeaders(),
                FlowLabel = this.FlowLabel,
                HopLimit = (byte)this.HopLimit,
                NextHeader = (IpV4Protocol)Enum.Parse(typeof(IpV4Protocol), this.SelectedProtocol),
                Source = new IpV6Address(this.SourceIP),
                TrafficClass = (byte)this.TrafficClass
            };
        }
	}
}
