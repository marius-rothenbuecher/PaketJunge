using System;
using System.Collections.Generic;
using System.Linq;
using PaketJunge.Model.Layer4;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Icmp;

namespace PaketJunge.ViewModel
{
    public class ICMPViewModel : Layer4ViewModel
    {
        public List<string> TypesAndCodes { get { return this.typesAndCodes; } set { SetField<List<string>>(ref this.typesAndCodes, value, nameof(this.TypesAndCodes)); } }
        private List<string> typesAndCodes;

        public string SelectedTypeAndCode { get { return this.selectedTypeAndCode; } set { SetField<string>(ref this.selectedTypeAndCode, value, nameof(this.SelectedTypeAndCode)); } }
        private string selectedTypeAndCode;

        public string Data { get { return this.data; } set { SetField<string>(ref this.data, value, nameof(this.Data)); } }
        private string data;

        public ICMPViewModel()
        {
            this.typesAndCodes = ICMPModel.GetTypesAndCodes();
            this.data = "DEADBEEF";
            this.selectedTypeAndCode = IcmpMessageTypeAndCode.Echo.ToString();
        }

        public override ILayer GetSegment()
        {
            // TODO: add sequence and identifier maybe also for IP
            var typeAndCode = (IcmpMessageTypeAndCode)Enum.Parse(typeof(IcmpMessageTypeAndCode), this.SelectedTypeAndCode);
            byte layerCode = (byte)typeAndCode;
            byte layerMessageType = (byte)((ushort)typeAndCode >> 8);

            return new IcmpUnknownLayer()
            {
                Payload = new Datagram(this.StringToByteArray(this.Data)),
                LayerCode = layerCode,
                LayerMessageType = layerMessageType
            };
        }

        private byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
