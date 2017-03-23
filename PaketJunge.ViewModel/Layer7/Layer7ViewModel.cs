using System;
using PcapDotNet.Packets;

namespace PaketJunge.ViewModel
{
	public abstract class Layer7ViewModel : NotifyProperty
	{
		public abstract ILayer GetProtocolDataUnit();
	}
}
