using System;
using PcapDotNet.Packets;

namespace PaketJunge.ViewModel
{
	public abstract class Layer2ViewModel : NotifyProperty
	{
		public abstract ILayer GetFrame();
	}
}
