using System;
using System.Collections.Generic;
using System.Linq;
using PaketJunge.Model;

namespace PaketJunge.ViewModel
{
	public class DeviceViewModel : Layer1ViewModel
	{
		public List<string> Devices { get { return this.devices; } set { SetField<List<string>>(ref this.devices, value, nameof(this.Devices)); } }
		private List<string> devices;

		public List<string> Standards { get { return this.standards; } set { SetField<List<string>>(ref this.standards, value, nameof(this.Standards)); } }
		private List<string> standards;

		public ushort SelectedDevice { get { return this.selectedDevice; } set { SetField<ushort>(ref this.selectedDevice, value, nameof(this.SelectedDevice)); } }
		private ushort selectedDevice;

		public string SelectedStandard { get { return this.selectedStandard; } set { SetField<string>(ref this.selectedStandard, value, nameof(this.SelectedStandard)); } }
		private string selectedStandard;

		public DeviceViewModel()
		{
			this.devices = DeviceModel.GetDevices();
			this.standards = DeviceModel.GetStandards();
            this.selectedDevice = 0;
            this.selectedStandard = this.Standards.FirstOrDefault();
		}

		public override ushort GetDevice()
		{
			return this.SelectedDevice;
		}
	}
}
