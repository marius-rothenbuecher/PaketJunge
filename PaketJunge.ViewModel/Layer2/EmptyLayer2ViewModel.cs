using System;
using PcapDotNet.Packets;

namespace PaketJunge.ViewModel
{
    public class EmptyLayer2ViewModel : Layer2ViewModel
    {
        public override ILayer GetFrame()
        {
            throw new NotImplementedException();
        }
    }
}
