using System;

namespace PaketJunge.ViewModel
{
    public class EmptyLayer1ViewModel : Layer1ViewModel
    {
        public override ushort GetDevice()
        {
            throw new NotImplementedException();
        }
    }
}
