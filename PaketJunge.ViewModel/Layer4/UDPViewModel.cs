using System;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Transport;

namespace PaketJunge.ViewModel
{
	public class UDPViewModel : Layer4ViewModel
	{
		public ushort DestinationPort { get { return this.destinationPort; } set { SetField<ushort>(ref this.destinationPort, value, "DestinationPort"); } }
		private ushort destinationPort;

		public ushort SourcePort { get { return this.sourcePort; } set { SetField<ushort>(ref this.sourcePort, value, "SourcePort"); } }
		private ushort sourcePort;
		
		public override ILayer GetSegment()
		{
			return new UdpLayer()
			{
				SourcePort = this.SourcePort,
				DestinationPort = this.DestinationPort
			};
		}
	}
}
