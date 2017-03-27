using System;
using System.Collections.Generic;
using PaketJunge.Model;
using PaketJunge.Packets.PROFINET.DCP;
using PcapDotNet.Packets;

namespace PaketJunge.ViewModel
{
    public class PROFINETDCPViewModel : Layer3ViewModel
    {
        public ushort DCPBlockLength { get { return this.dcpBlockLength; } set { SetField<ushort>(ref this.dcpBlockLength, value, nameof(this.DCPBlockLength)); } }
        private ushort dcpBlockLength;

        public byte DCPBlockOption { get { return this.dcpBlockOption; } set { SetField<byte>(ref this.dcpBlockOption, value, nameof(this.DCPBlockOption)); } }
        private byte dcpBlockOption;

        public byte DCPBlockSubOption { get { return this.dcpBlockSubOption; } set { SetField<byte>(ref this.dcpBlockSubOption, value, nameof(this.DCPBlockSubOption)); } }
        private byte dcpBlockSubOption;

        public ushort DCPDataLength { get { return this.dcpDataLength; } set { SetField<ushort>(ref this.dcpDataLength, value, nameof(this.DCPDataLength)); } }
        private ushort dcpDataLength;

        public List<string> DCPServiceIds { get { return this.dcpServiceIds; } set { SetField<List<string>>(ref this.dcpServiceIds, value, nameof(this.DCPServiceIds)); } }
        private List<string> dcpServiceIds;

        public string SelectedDCPServiceId { get { return this.selectedDCPServiceId; } set { SetField<string>(ref this.selectedDCPServiceId, value, nameof(this.SelectedDCPServiceId)); } }
        private string selectedDCPServiceId;

        public List<string> DCPServiceTypes { get { return this.dcpServiceTypes; } set { SetField<List<string>>(ref this.dcpServiceTypes, value, nameof(this.DCPServiceTypes)); } }
        private List<string> dcpServiceTypes;

        public string SelectedDCPServiceType { get { return this.selectedDCPServiceType; } set { SetField<string>(ref this.selectedDCPServiceType, value, nameof(this.SelectedDCPServiceType)); } }
        private string selectedDCPServiceType;

        public List<string> DCPTypes { get { return this.dcpTypes; } set { SetField<List<string>>(ref this.dcpTypes, value, nameof(this.DCPTypes)); } }
        private List<string> dcpTypes;

        public string SelectedDCPType { get { return this.selectedDCPType; } set { SetField<string>(ref this.selectedDCPType, value, nameof(this.SelectedDCPType)); } }
        private string selectedDCPType;

        public ushort ResponseDelay { get { return this.responseDelay; } set { SetField<ushort>(ref this.responseDelay, value, nameof(this.ResponseDelay)); } }
        private ushort responseDelay;

        public uint Xid { get { return this.xid; } set { SetField<uint>(ref this.xid, value, nameof(this.Xid)); } }
        private uint xid;

        public PROFINETDCPViewModel()
        {
            this.dcpServiceIds = PROFINETDCPModel.GetDCPServiceIds();
            this.dcpServiceTypes = PROFINETDCPModel.GetDCPServiceTypes();
            this.dcpTypes = PROFINETDCPModel.GetDCPTypes();

            this.selectedDCPServiceId = DCPServiceId.Identify.ToString();
            this.selectedDCPServiceType = DCPServiceType.Request.ToString();
            this.selectedDCPType = DCPType.IdentRequest.ToString();
            this.xid = 16777217;
            this.responseDelay = 1;
            this.dcpDataLength = 4;
            this.dcpBlockOption = 255;
            this.dcpBlockSubOption = 255;
            this.dcpBlockLength = 0;
        }

        public override ILayer GetPacket()
        {
            return new PROFINETDCPLayer()
            {
                DCPBlockLength = this.DCPBlockLength,
                DCPBlockOption = this.DCPBlockOption,
                DCPBlockSubOption = this.DCPBlockSubOption,
                DCPDataLength = this.DCPDataLength,
                DCPServiceId = (DCPServiceId)Enum.Parse(typeof(DCPServiceId), this.SelectedDCPServiceId),
                DCPServiceType = (DCPServiceType)Enum.Parse(typeof(DCPServiceType), this.SelectedDCPServiceType),
                DCPType = (DCPType)Enum.Parse(typeof(DCPType), this.SelectedDCPType),
                ResponseDelay = this.ResponseDelay,
                Xid = this.Xid
            };
        }
    }
}
