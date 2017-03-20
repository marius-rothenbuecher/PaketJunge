using System;
using System.Collections.Generic;
using PcapDotNet.Packets;

namespace PaketJunge.ViewModel
{
	public class IPv6ViewModel : Layer3ViewModel
	{
		public List<string> Protocols { get { return this.protocols; } set { SetField<List<string>>(ref this.protocols, value, "Protocols"); } }
		private List<string> protocols;

		public string SelectedProtocol { get { return this.selectedProtocol; } set { SetField<string>(ref this.selectedProtocol, value, "SelectedProtocol"); } }
		private string selectedProtocol;

		public override ILayer GetPacket()
		{
			throw new NotImplementedException();
		}
	}
}
