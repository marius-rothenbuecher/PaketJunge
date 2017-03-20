using System;
using PcapDotNet.Packets;

namespace PaketJunge.ViewModel
{
	public abstract class Layer4ViewModel : NotifyProperty
	{
		public abstract ILayer GetSegment();
	}
}
