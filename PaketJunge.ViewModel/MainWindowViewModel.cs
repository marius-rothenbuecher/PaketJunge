using System;
using System.ComponentModel;
using System.Windows;
using PaketJunge.Model;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using SharpPcap;

namespace PaketJunge.ViewModel
{
    public class MainWindowViewModel : NotifyProperty
    {
        public Layer1ViewModel Layer1 { get => this.layer1; set { SetField(ref this.layer1, value, "Layer1"); } }
        private Layer1ViewModel layer1;

        public Layer2ViewModel Layer2 { get => this.layer2; set { SetField(ref this.layer2, value, "Layer2"); } }
        private Layer2ViewModel layer2;

        public Layer3ViewModel Layer3 { get => this.layer3; set { SetField(ref this.layer3, value, "Layer3"); } }
        private Layer3ViewModel layer3;

        public Layer4ViewModel Layer4 { get => this.layer4; set { SetField(ref this.layer4, value, "Layer4"); } }
        private Layer4ViewModel layer4;

        public Layer5ViewModel Layer5 { get { return this.layer5; } set { SetField(ref this.layer5, value, "Layer5"); } }
        private Layer5ViewModel layer5;

        public Layer6ViewModel Layer6 { get { return this.layer6; } set { SetField(ref this.layer6, value, "Layer6"); } }
        private Layer6ViewModel layer6;

        public Layer7ViewModel Layer7 { get { return this.layer7; } set { SetField(ref this.layer7, value, "Layer7"); } }
        private Layer7ViewModel layer7;

        public ICaptureDevice Device { get; set; }

        public RelayCommand<object> SendCommand { get; set; }

        public MainWindowViewModel()
        {
            this.layer1 = new DeviceViewModel();
            this.layer2 = new EmptyLayer2ViewModel();
            this.layer3 = new EmptyLayer3ViewModel();
            this.layer4 = new EmptyLayer4ViewModel();
            this.layer5 = new EmptyLayer5ViewModel();
            this.layer6 = new EmptyLayer6ViewModel();
            this.layer7 = new DataViewModel();
            this.SendCommand = new RelayCommand<object>(Send);

            var deviceViewModel = (DeviceViewModel)this.layer1;
            deviceViewModel.PropertyChanged += this.DeviceViewModelPropertyChanged;
        }

        // TODO: implement function for saving templates
        public void Send(object sender)
        {
            try
            {
                PacketBuilder packetBuilder;

                if (this.Layer2 == null)
                    packetBuilder = new PacketBuilder(this.Layer7.GetData());
                else if (this.Layer3 == null)
                    packetBuilder = new PacketBuilder(this.Layer2.GetFrame(), this.Layer7.GetData());
                else if (this.Layer4 == null)
                    packetBuilder = new PacketBuilder(this.Layer2.GetFrame(), this.Layer3.GetPacket(), this.Layer7.GetData());
                else
                    packetBuilder = new PacketBuilder(this.Layer2.GetFrame(), this.Layer3.GetPacket(), this.Layer4.GetSegment(), this.Layer7.GetData());

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

        private void DeviceViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string standard = ((DeviceViewModel)sender).SelectedStandard;

            if (standard == StandardType.Ethernet2.ToString())
            {
                this.Layer2 = new EthernetViewModel();

                string macAsByteStream = this.OpenDevice()?.MacAddress?.ToString();
                string mac = this.GetMacAsString(macAsByteStream);

                EthernetViewModel ethernetViewModel = (EthernetViewModel)this.layer2;
                ethernetViewModel.SourceMAC = mac;
                ethernetViewModel.PropertyChanged += EthernetViewModelPropertyChanged;
            }
            else
            {
                this.Layer2 = null;
                this.Layer3 = null;
                this.Layer4 = null;
                this.Layer5 = null;
                this.Layer6 = null;
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
                    this.Layer4 = null;

                    IPv4ViewModel ipv4ViewModel = (IPv4ViewModel)this.layer3;
                    ipv4ViewModel.PropertyChanged += IPv4ViewModelPropertyChanged;
                }
                else if (type == EthernetType.IpV6.ToString())
                {
                    this.layer3 = new IPv6ViewModel();
                    this.Layer4 = null;

                    IPv6ViewModel ipv6ViewModel = (IPv6ViewModel)this.layer3;
                    ipv6ViewModel.PropertyChanged += IPv6ViewModel_PropertyChanged;
                }
                else if (type == EthernetType.Arp.ToString())
                {
                    this.Layer3 = new ARPViewModel();
                    this.Layer4 = null;
                }
                else
                {
                    this.Layer3 = null;
                    this.Layer4 = null;
                }
            }
        }

        private void IPv4ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IPv4ViewModel.SelectedProtocol))
            {
                string protocol = ((IPv4ViewModel)sender).SelectedProtocol;

                if (protocol == IpV4Protocol.Tcp.ToString())
                    this.Layer4 = new TCPViewModel();
                else if (protocol == IpV4Protocol.Udp.ToString())
                    this.Layer4 = new UDPViewModel();
                else if (protocol == IpV4Protocol.InternetControlMessageProtocol.ToString())
                    this.Layer4 = new ICMPViewModel();
                else
                    this.Layer4 = null;
            }
            else if (e.PropertyName == nameof(IPv4ViewModel.DestinationIP))
            {

            }
        }

        private void IPv6ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IPv4ViewModel.SelectedProtocol))
            {
                string protocol = ((IPv6ViewModel)sender).SelectedProtocol;

                if (protocol == IpV4Protocol.Tcp.ToString())
                    this.Layer4 = new TCPViewModel();
                else if (protocol == IpV4Protocol.Udp.ToString())
                    this.Layer4 = new UDPViewModel();
                else
                    this.Layer4 = null;
            }
        }
    }
}
