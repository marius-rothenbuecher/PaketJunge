using System;
using PcapDotNet.Packets;

namespace PaketJunge.Packets.PROFINET
{
    public class PROFINETLayer : SimpleLayer
    {
        public PROFINETLayer()
        {
            this.DCPBlockLength = 0;
            this.DCPBlockOption = 0;
            this.DCPBlockSubOption = 0;
            this.DCPDataLength = 0;
            this.DCPServiceId = DCPServiceId.Identify;
            this.DCPServiceType = DCPServiceType.Request;
            this.DCPType = DCPType.IdentRequest;
        }

        public override int Length => 40;

        public override DataLinkKind? DataLink => DataLinkKind.Ethernet;

        public DCPType DCPType { get; set; }

        public DCPServiceId DCPServiceId { get; set; }

        public DCPServiceType DCPServiceType { get; set; }

        public uint Xid { get; set; }

        public ushort ResponseDelay { get; set; }

        public ushort DCPDataLength { get; set; }

        public byte DCPBlockOption { get; set; }

        public byte DCPBlockSubOption { get; set; }

        public ushort DCPBlockLength { get; set; }

        public override bool Equals(Layer other)
        {
            return this == other;
        }

        public override void Finalize(byte[] buffer, int offset, int payloadLength, ILayer nextLayer)
        {
        }

        protected override void Write(byte[] buffer, int offset)
        {
            buffer[0 + offset] = (byte)((ushort)this.DCPType >> 8);
            buffer[1 + offset] = (byte)((ushort)this.DCPType);
            buffer[2 + offset] = (byte)this.DCPServiceId;
            buffer[3 + offset] = (byte)this.DCPServiceType;
            buffer[4 + offset] = (byte)(this.Xid >> 24);
            buffer[5 + offset] = (byte)(this.Xid >> 16);
            buffer[6 + offset] = (byte)(this.Xid >> 8);
            buffer[7 + offset] = (byte)(this.Xid);
            buffer[8 + offset] = (byte)(this.ResponseDelay >> 8);
            buffer[9 + offset] = (byte)(this.ResponseDelay);
            buffer[10 + offset] = (byte)(this.DCPDataLength >> 8);
            buffer[11 + offset] = (byte)(this.DCPDataLength);
            buffer[12 + offset] = this.DCPBlockOption;
            buffer[13 + offset] = this.DCPBlockSubOption;
            buffer[14 + offset] = (byte)(this.DCPBlockLength >> 8);
            buffer[15 + offset] = (byte)(this.DCPBlockLength);
        }
    }
}
