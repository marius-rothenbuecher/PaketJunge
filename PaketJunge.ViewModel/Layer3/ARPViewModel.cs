using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using PaketJunge.Model;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Ethernet;

namespace PaketJunge.ViewModel
{
    public class ARPViewModel : Layer3ViewModel
    {
        public string SourceMAC { get { return this.sourceMAC; } set { SetField<string>(ref this.sourceMAC, value, nameof(this.SourceMAC)); } }
        private string sourceMAC;

        public string DestinationMAC { get { return this.destinationMAC; } set { SetField<string>(ref this.destinationMAC, value, nameof(this.DestinationMAC)); } }
        private string destinationMAC;

        public string SourceIP { get { return this.sourceIP; } set { SetField<string>(ref this.sourceIP, value, nameof(this.SourceIP)); } }
        private string sourceIP;

        public string DestinationIP { get { return this.destinationIP; } set { SetField<string>(ref this.destinationIP, value, nameof(this.DestinationIP)); } }
        private string destinationIP;

        public List<string> Types { get { return this.types; } set { SetField<List<string>>(ref this.types, value, nameof(this.Types)); } }
        private List<string> types;

        public List<string> Operations { get { return this.operations; } set { SetField<List<string>>(ref this.operations, value, nameof(this.Operations)); } }
        private List<string> operations;

        public string SelectedType { get { return this.selectedType; } set { SetField<string>(ref this.selectedType, value, nameof(this.SelectedType)); } }
        private string selectedType;

        public string SelectedOperation { get { return this.selectedOperation; } set { SetField<string>(ref this.selectedOperation, value, nameof(this.SelectedOperation)); } }
        private string selectedOperation;

        public ARPViewModel()
        {
            this.types = EthernetModel.GetPacketTypes();
            this.operations = ARPModel.GetOperations();
            this.sourceMAC = "00:00:00:00:00:00";
            this.destinationMAC = "00:00:00:00:00:00";
            this.sourceIP = "127.0.0.1";
            this.destinationIP = "127.0.0.1";
            this.selectedOperation = ArpOperation.None.ToString();
            this.selectedType = EthernetType.IpV4.ToString();
        }
        
        public override ILayer GetPacket()
        {
            string sourceMac = this.SourceMAC.Replace("-", string.Empty).Replace(":", string.Empty);
            string destinationMac = this.DestinationMAC.Replace("-", string.Empty).Replace(":", string.Empty);

            byte[] sourceMacAsByteStream = this.StringToByteArray(sourceMac);
            byte[] destinationMacAsByteStream = this.StringToByteArray(destinationMac);

            return new ArpLayer()
            {
                Operation = (ArpOperation)Enum.Parse(typeof(ArpOperation), this.SelectedOperation),
                ProtocolType = (EthernetType)Enum.Parse(typeof(EthernetType), this.SelectedType),
                SenderHardwareAddress = new ReadOnlyCollection<byte>(sourceMacAsByteStream),
                TargetHardwareAddress = new ReadOnlyCollection<byte>(destinationMacAsByteStream),
                SenderProtocolAddress = new ReadOnlyCollection<byte>(IPAddress.Parse(this.SourceIP).GetAddressBytes()),
                TargetProtocolAddress = new ReadOnlyCollection<byte>(IPAddress.Parse(this.DestinationIP).GetAddressBytes()),
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
