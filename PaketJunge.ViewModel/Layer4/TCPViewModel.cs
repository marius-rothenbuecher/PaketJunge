using System;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Transport;

namespace PaketJunge.ViewModel
{
	public class TCPViewModel : Layer4ViewModel
	{
		public ushort DestinationPort { get { return this.destinationPort; } set { SetField<ushort>(ref this.destinationPort, value, "DestinationPort"); } }
		private ushort destinationPort;

		public ushort SourcePort { get { return this.sourcePort; } set { SetField<ushort>(ref this.sourcePort, value, "SourcePort"); } }
		private ushort sourcePort;

		public bool Ack { get { return this.ack; } set { SetField<bool>(ref this.ack, value, "Ack"); } }
		private bool ack;

		public bool Syn { get { return this.syn; } set { SetField<bool>(ref this.syn, value, "Syn"); } }
		private bool syn;

		public bool Rst { get { return this.rst; } set { SetField<bool>(ref this.rst, value, "Rst"); } }
		private bool rst;

		public bool Urg { get { return this.urg; } set { SetField<bool>(ref this.urg, value, "Urg"); } }
		private bool urg;

		public bool Fin { get { return this.fin; } set { SetField<bool>(ref this.fin, value, "Fin"); } }
		private bool fin;

		public bool Psh { get { return this.psh; } set { SetField<bool>(ref this.psh, value, "Psh"); } }
		private bool psh;

		public uint SequenceNumber { get { return this.sequenceNumber; } set { SetField<uint>(ref this.sequenceNumber, value, "SequenceNumber"); } }
		private uint sequenceNumber;

		public uint AcknowledgmentNumber { get { return this.acknowledgmentNumber; } set { SetField<uint>(ref this.acknowledgmentNumber, value, "AcknowledgmentNumber"); } }
		private uint acknowledgmentNumber;

		public ushort UrgentPointer { get { return this.urgentPointer; } set { SetField<ushort>(ref this.urgentPointer, value, "UrgentPointer"); } }
		private ushort urgentPointer;

		public ushort WindowSize { get { return this.windowSize; } set { SetField<ushort>(ref this.windowSize, value, "WindowSize"); } }
		private ushort windowSize;

		public override ILayer GetSegment()
		{
			return new TcpLayer()
			{
				SourcePort = this.SourcePort,
				DestinationPort = this.DestinationPort,
				ControlBits = GetTcpControlBits(),
				AcknowledgmentNumber = this.AcknowledgmentNumber,
				SequenceNumber = this.SequenceNumber,
				UrgentPointer = this.UrgentPointer,
				Window = this.WindowSize
			};
		}

		private TcpControlBits GetTcpControlBits()
		{
			return (this.Ack ? TcpControlBits.Acknowledgment : 0) |
				(this.Fin ? TcpControlBits.Fin : 0) |
				(this.Psh ? TcpControlBits.Push : 0) |
				(this.Rst ? TcpControlBits.Reset : 0) |
				(this.Syn ? TcpControlBits.Synchronize : 0) |
				(this.Urg ? TcpControlBits.Urgent : 0);
		}
	}
}
