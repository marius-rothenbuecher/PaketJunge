using System;
using PcapDotNet.Packets;

namespace PaketJunge.ViewModel
{
    public class EmptyLayer7ViewModel : Layer7ViewModel
    {
        public override ILayer GetData()
        {
            throw new NotImplementedException();
        }
    }
}
