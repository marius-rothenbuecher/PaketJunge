using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;

namespace PaketJunge.ViewModel
{
	public abstract class Layer3ViewModel : NotifyProperty
	{
		public abstract ILayer GetPacket();
	}
}
