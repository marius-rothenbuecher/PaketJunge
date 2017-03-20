using System;
using PcapDotNet.Packets;

namespace PaketJunge.ViewModel
{
    public class EmptyLayer4ViewModel : Layer4ViewModel
    {
        public override ILayer GetSegment()
        {
            throw new NotImplementedException();
        }
    }
}
