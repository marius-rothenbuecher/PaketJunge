using System;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Transport;

namespace PaketJunge.ViewModel
{
	public class TCPViewModel : Layer4ViewModel
	{
		public ushort DestinationPort { get { return this.destinationPort; } set { SetField<ushort>(ref this.destinationPort, value, nameof(this.DestinationPort)); } }
		private ushort destinationPort;

		public ushort SourcePort { get { return this.sourcePort; } set { SetField<ushort>(ref this.sourcePort, value, nameof(this.SourcePort)); } }
		private ushort sourcePort;

		public bool Ack { get { return this.ack; } set { SetField<bool>(ref this.ack, value, nameof(this.Ack)); } }
		private bool ack;

		public bool Syn { get { return this.syn; } set { SetField<bool>(ref this.syn, value, nameof(this.Syn)); } }
		private bool syn;

		public bool Rst { get { return this.rst; } set { SetField<bool>(ref this.rst, value, nameof(this.Rst)); } }
		private bool rst;

		public bool Urg { get { return this.urg; } set { SetField<bool>(ref this.urg, value, nameof(this.Urg)); } }
		private bool urg;

		public bool Fin { get { return this.fin; } set { SetField<bool>(ref this.fin, value, nameof(this.Fin)); } }
		private bool fin;

		public bool Psh { get { return this.psh; } set { SetField<bool>(ref this.psh, value, nameof(this.Psh)); } }
		private bool psh;

		public uint SequenceNumber { get { return this.sequenceNumber; } set { SetField<uint>(ref this.sequenceNumber, value, nameof(this.SequenceNumber)); } }
		private uint sequenceNumber;

		public uint AcknowledgmentNumber { get { return this.acknowledgmentNumber; } set { SetField<uint>(ref this.acknowledgmentNumber, value, nameof(this.AcknowledgmentNumber)); } }
		private uint acknowledgmentNumber;

		public ushort UrgentPointer { get { return this.urgentPointer; } set { SetField<ushort>(ref this.urgentPointer, value, nameof(this.UrgentPointer)); } }
		private ushort urgentPointer;

		public ushort WindowSize { get { return this.windowSize; } set { SetField<ushort>(ref this.windowSize, value, nameof(this.WindowSize)); } }
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
