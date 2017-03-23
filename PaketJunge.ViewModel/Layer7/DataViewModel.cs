using System;
using System.Linq;
using System.Text;
using PcapDotNet.Packets;

namespace PaketJunge.ViewModel
{
    public class DataViewModel : Layer7ViewModel
    {
        public bool IsByteStream { get; set; }

        public string Data { get { return this.data; } set { SetField<string>(ref this.data, value, nameof(this.Data)); } }
        private string data;

        public DataViewModel()
        {
            this.data = string.Empty;
        }

        public override ILayer GetProtocolDataUnit()
        {
            if (this.IsByteStream)
                return new PayloadLayer() { Data = new Datagram(this.StringToByteArray(this.Data)) };

            return new PayloadLayer() { Data = new Datagram(Encoding.Default.GetBytes(this.Data)) };

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
