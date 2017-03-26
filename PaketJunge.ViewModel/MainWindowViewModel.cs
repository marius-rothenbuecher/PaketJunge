using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows;
using PaketJunge.Model;
using PaketJunge.Model.Layer4;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using SharpPcap;

namespace PaketJunge.ViewModel
{
    // TODO: add presets
    public class MainWindowViewModel : NotifyProperty
    {
        [DllImport("iphlpapi.dll")]
        public static extern int SendARP(int destinationIp, int sourceIp, [Out] byte[] macAddress, ref int macLength);

        public Layer1ViewModel Layer1 { get => this.layer1; set { SetField(ref this.layer1, value, nameof(this.Layer1)); } }
        private Layer1ViewModel layer1;

        public Layer2ViewModel Layer2 { get => this.layer2; set { SetField(ref this.layer2, value, nameof(this.Layer2)); } }
        private Layer2ViewModel layer2;

        public Layer3ViewModel Layer3 { get => this.layer3; set { SetField(ref this.layer3, value, nameof(this.Layer3)); } }
        private Layer3ViewModel layer3;

        public Layer4ViewModel Layer4 { get => this.layer4; set { SetField(ref this.layer4, value, nameof(this.Layer4)); } }
        private Layer4ViewModel layer4;

        public Layer5ViewModel Layer5 { get { return this.layer5; } set { SetField(ref this.layer5, value, nameof(this.Layer5)); } }
        private Layer5ViewModel layer5;

        public Layer6ViewModel Layer6 { get { return this.layer6; } set { SetField(ref this.layer6, value, nameof(this.Layer6)); } }
        private Layer6ViewModel layer6;

        public Layer7ViewModel Layer7 { get { return this.layer7; } set { SetField(ref this.layer7, value, nameof(this.Layer7)); } }
        private Layer7ViewModel layer7;

        public ICaptureDevice Device { get; set; }

        public RelayCommand<object> SendCommand { get; set; }

        public RelayCommand<object> Layer7ToDataCommand { get; set; }

        public MainWindowViewModel()
        {
            this.Layer1 = new DeviceViewModel();
            this.ClearLayers(2);

            this.SendCommand = new RelayCommand<object>(this.Send);
            this.Layer7ToDataCommand = new RelayCommand<object>(this.Layer7ToData);

            var deviceViewModel = (DeviceViewModel)this.Layer1;
            deviceViewModel.PropertyChanged += this.DeviceViewModelPropertyChanged;
        }

        public void Send(object sender)
        {
            try
            {
                PacketBuilder packetBuilder;

                if (this.Layer2.GetType() == typeof(EmptyLayer2ViewModel))
                    packetBuilder = new PacketBuilder(this.Layer7.GetProtocolDataUnit());
                else if (this.Layer3.GetType() == typeof(EmptyLayer3ViewModel))
                    packetBuilder = new PacketBuilder(this.Layer2.GetFrame(), this.Layer7.GetProtocolDataUnit());
                else if (this.Layer4.GetType() == typeof(EmptyLayer4ViewModel))
                    packetBuilder = new PacketBuilder(this.Layer2.GetFrame(), this.Layer3.GetPacket(), this.Layer7.GetProtocolDataUnit());
                else
                    packetBuilder = new PacketBuilder(this.Layer2.GetFrame(), this.Layer3.GetPacket(), this.Layer4.GetSegment(), this.Layer7.GetProtocolDataUnit());

                var packet = packetBuilder.Build(DateTime.UtcNow);

                this.Device = this.OpenDevice();

                this.Device.SendPacket(packet.Buffer);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Message:\r\n{e.Message}\r\n\r\nStacktrace:\r\n{e.StackTrace}");
            }
            finally
            {
                this.Device?.Close();
            }
        }

        private void Layer7ToData(object obj)
        {
            try
            {
                var pdu = this.Layer7.GetProtocolDataUnit();
                byte[] buffer = new byte[pdu.Length];

                pdu.Write(buffer, 0, pdu.Length, null, null);

                this.Layer7 = new DataViewModel(this.ByteArrayToString(buffer));
            }
            catch (Exception e)
            {
                MessageBox.Show($"Message:\r\n{e.Message}\r\n\r\nStacktrace:\r\n{e.StackTrace}");
            }
        }

        private ICaptureDevice OpenDevice()
        {
            var device = CaptureDeviceList.Instance[this.Layer1.GetDevice()];
            device.Open();

            return device;
        }

        private string GetMacAsString(string macAsByteStream)
        {
            if (macAsByteStream.Length != 12)
                return "00:11:22:33:44:55";

            string macAsString = string.Empty;

            for (int i = 0; i < 5; i++)
                macAsString += macAsByteStream.Substring(i * 2, 2) + ":";

            macAsString += macAsByteStream.Substring(10, 2);

            return macAsString;
        }

        public string ByteArrayToString(byte[] buffer)
        {
            string hex = BitConverter.ToString(buffer);
            return hex.Replace("-", string.Empty);
        }

        private void ClearLayers(int layer)
        {
            if (layer <= 1)
                this.Layer1 = new EmptyLayer1ViewModel();
            if (layer <= 2)
                this.Layer2 = new EmptyLayer2ViewModel();
            if (layer <= 3)
                this.Layer3 = new EmptyLayer3ViewModel();
            if (layer <= 4)
                this.Layer4 = new EmptyLayer4ViewModel();
            if (layer <= 5)
                this.Layer5 = new EmptyLayer5ViewModel();
            if (layer <= 6)
                this.Layer6 = new EmptyLayer6ViewModel();
            if (layer <= 7)
                this.Layer7 = new DataViewModel();
        }

        private IPAddress GetDefaultIPv4Gateway()
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(x => x.OperationalStatus == OperationalStatus.Up)
                .SelectMany(x => x.GetIPProperties()?.GatewayAddresses)
                .Where(x => x.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                .Select(x => x?.Address)
                .FirstOrDefault(x => x != null);
        }

        private IPAddress GetAssignedIPv4Address()
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(x => x.OperationalStatus == OperationalStatus.Up)
                .Where(x => x.GetIPProperties() != null)
                .Where(x => x.GetIPProperties().GatewayAddresses.Any(y => y.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork))
                .SelectMany(x => x.GetIPProperties()?.UnicastAddresses)
                .Where(x => x.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                .Select(x => x?.Address)
                .FirstOrDefault(a => a != null);
        }

        private string GetMACAddress(IPAddress ip)
        {
            var ipAddress = ip;
            int length = 6;
            byte[] mac = new byte[length];

            SendARP((int)ipAddress.Address, 0, mac, ref length);

            string macAddress = BitConverter.ToString(mac, 0, length).Replace("-", ":");

            return macAddress;
        }

        private void DeviceViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string standard = ((DeviceViewModel)sender).SelectedStandard;

            if (standard == StandardType.Ethernet2.ToString())
            {
                if (this.Layer2.GetType() == typeof(EmptyLayer2ViewModel) || e.PropertyName == nameof(EthernetViewModel.SelectedType))
                    this.Layer2 = new EthernetViewModel();

                string macAsByteStream = this.OpenDevice()?.MacAddress?.ToString();
                string mac = this.GetMacAsString(macAsByteStream);

                var ethernetViewModel = (EthernetViewModel)this.Layer2;
                ethernetViewModel.SourceMAC = mac;
                ethernetViewModel.PropertyChanged += this.EthernetViewModelPropertyChanged;
            }
            else
            {
                this.ClearLayers(2);
            }
        }

        private void EthernetViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EthernetViewModel.SelectedType))
            {
                string type = ((EthernetViewModel)sender).SelectedType;

                if (type == EthernetType.IpV4.ToString())
                {
                    this.Layer3 = new IPv4ViewModel();
                    this.ClearLayers(4);

                    var ipv4ViewModel = (IPv4ViewModel)this.Layer3;
                    ipv4ViewModel.PropertyChanged += this.IPv4ViewModelPropertyChanged;

                    var defaultGateway = this.GetDefaultIPv4Gateway();
                    var ipAddress = this.GetAssignedIPv4Address();

                    if (defaultGateway != null)
                    {
                        ipv4ViewModel.DestinationIP = defaultGateway.ToString();
                        
                        if (this.Layer2 is EthernetViewModel)
                        {
                            var ethernetViewModel = (EthernetViewModel)this.Layer2;
                            var mac = this.GetMACAddress(defaultGateway);

                            if (mac != null)
                                ethernetViewModel.DestinationMAC = mac;
                        }
                    }

                    if (ipAddress != null)
                        ipv4ViewModel.SourceIP = this.GetAssignedIPv4Address().ToString();
                }
                else if (type == EthernetType.IpV6.ToString())
                {
                    this.Layer3 = new IPv6ViewModel();
                    this.ClearLayers(4);

                    var ipv6ViewModel = (IPv6ViewModel)this.Layer3;
                    ipv6ViewModel.PropertyChanged += this.IPv6ViewModelPropertyChanged;
                }
                else if (type == EthernetType.Arp.ToString())
                {
                    this.Layer3 = new ARPViewModel();
                    this.ClearLayers(4);
                }
                else if (type == EthernetType.PROFINET.ToString())
                {
                    this.Layer3 = new PROFINETViewModel();
                    this.ClearLayers(4);
                }
                else
                {
                    this.ClearLayers(3);
                }
            }
        }

        private void IPv4ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IPv4ViewModel.SelectedProtocol))
            {
                string protocol = ((IPv4ViewModel)sender).SelectedProtocol;

                if (protocol == IpV4Protocol.Tcp.ToString())
                {
                    this.Layer4 = new TCPViewModel();
                }
                else if (protocol == IpV4Protocol.Udp.ToString())
                {
                    this.Layer4 = new UDPViewModel();

                    var udpViewModel = (UDPViewModel)this.Layer4;
                    udpViewModel.SourcePort = 1337;
                    udpViewModel.PropertyChanged += this.UDPViewModelPropertyChanged;
                }
                else if (protocol == IpV4Protocol.InternetControlMessageProtocol.ToString())
                {
                    this.Layer4 = new ICMPViewModel();
                }
                else
                {
                    this.ClearLayers(4);
                }
            }
        }

        private void IPv6ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IPv4ViewModel.SelectedProtocol))
            {
                string protocol = ((IPv6ViewModel)sender).SelectedProtocol;

                if (protocol == IpV4Protocol.Tcp.ToString())
                {
                    this.Layer4 = new TCPViewModel();
                }
                else if (protocol == IpV4Protocol.Udp.ToString())
                {
                    this.Layer4 = new UDPViewModel();

                    var udpViewModel = (UDPViewModel)this.Layer4;
                    udpViewModel.PropertyChanged += this.UDPViewModelPropertyChanged;
                }
                else
                {
                    this.ClearLayers(4);
                }
            }
        }

        private void UDPViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UDPViewModel.SelectedProtocol))
            {
                string protocol = ((UDPViewModel)sender).SelectedProtocol;

                if (protocol == UDPProtocol.DNS.ToString())
                {
                    this.Layer7 = new DNSViewModel();
                    ((UDPViewModel)this.Layer4).DestinationPort = 53;
                }
                else
                {
                    this.Layer7 = new DataViewModel();
                }
            }
        }
    }
}
