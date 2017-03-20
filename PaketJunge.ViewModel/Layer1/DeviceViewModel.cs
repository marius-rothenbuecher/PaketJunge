using System;
using System.Collections.Generic;
using PaketJunge.Model;

namespace PaketJunge.ViewModel
{
	public class DeviceViewModel : Layer1ViewModel
	{
		public List<string> Devices { get { return this.devices; } set { SetField<List<string>>(ref this.devices, value, "Devices"); } }
		private List<string> devices;

		public List<string> Standards { get { return this.standards; } set { SetField<List<string>>(ref this.standards, value, "Standards"); } }
		private List<string> standards;

		public ushort SelectedDevice { get { return this.selectedDevice; } set { SetField<ushort>(ref this.selectedDevice, value, "SelectedDevice"); } }
		private ushort selectedDevice;

		public string SelectedStandard { get { return this.selectedStandard; } set { SetField<string>(ref this.selectedStandard, value, "SelectedStandard"); } }
		private string selectedStandard;

		public DeviceViewModel()
		{
			this.devices = DeviceModel.GetDevices();
			this.standards = DeviceModel.GetStandards();
		}

		public override ushort GetDevice()
		{
			return this.SelectedDevice;
		}
	}
}
