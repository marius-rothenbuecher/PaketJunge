using System;
using PcapDotNet.Packets;

namespace PaketJunge.ViewModel
{
    public class EmptyLayer3ViewModel : Layer3ViewModel
    {
        public override ILayer GetPacket()
        {
            throw new NotImplementedException();
        }
    }
}
