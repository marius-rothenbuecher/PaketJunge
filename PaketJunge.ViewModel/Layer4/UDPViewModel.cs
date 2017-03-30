using System;
using System.Collections.Generic;
using PaketJunge.Model.Layer4;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Transport;

namespace PaketJunge.ViewModel
{
	public class UDPViewModel : Layer4ViewModel
	{
		public ushort DestinationPort { get { return this.destinationPort; } set { SetField<ushort>(ref this.destinationPort, value, nameof(this.DestinationPort)); } }
		private ushort destinationPort;

		public ushort SourcePort { get { return this.sourcePort; } set { SetField<ushort>(ref this.sourcePort, value, nameof(this.SourcePort)); } }
		private ushort sourcePort;

        public List<string> Protocols { get { return this.protocols; } set { SetField<List<string>>(ref this.protocols, value, nameof(this.Protocols)); } }
        private List<string> protocols;

        public string SelectedProtocol { get { return this.selectedProtocol; } set { SetField<string>(ref this.selectedProtocol, value, nameof(this.SelectedProtocol)); } }
        private string selectedProtocol;

        public UDPViewModel()
        {
            this.protocols = UDPModel.GetProtocols();
            this.selectedProtocol = nameof(UDPProtocol.None);
        }

        public override ILayer GetSegment()
		{
			return new UdpLayer()
			{
                CalculateChecksumValue = true,
				SourcePort = this.SourcePort,
				DestinationPort = this.DestinationPort
			};
		}
	}
}
