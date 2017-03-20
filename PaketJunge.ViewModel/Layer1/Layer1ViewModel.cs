using System;

namespace PaketJunge.ViewModel
{
	public abstract class Layer1ViewModel : NotifyProperty
	{
		public abstract ushort GetDevice();
	}
}
