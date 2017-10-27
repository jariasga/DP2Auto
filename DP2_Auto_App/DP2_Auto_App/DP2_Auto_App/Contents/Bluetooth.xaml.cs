using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE;
using System.Collections.ObjectModel;
using Plugin.BLE.Abstractions.Exceptions;
using System.Diagnostics;

namespace DP2_Auto_App.Contents
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Bluetooth : ContentPage
	{
        ObservableCollection<IDevice> deviceList;
        IDevice device;
        IBluetoothLE ble;
        IAdapter adapter;

        public Bluetooth ()
		{
			InitializeComponent ();

            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            deviceList = new ObservableCollection<IDevice>();
            lv.ItemsSource = deviceList;
        }

        private void lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lv.SelectedItem == null) return;
            device = lv.SelectedItem as IDevice;
        }

        private void btnStatus_Clicked(object sender, EventArgs e)
        {
            // Check BlueTooth Status: BluetoothState.Off
            this.DisplayAlert("Estado", ble.State.ToString(), "Ok");
            Debug.WriteLine($"The bluetooth state changed to {ble.State}");
        }

        private async void btnScanDev_Clicked(object sender, EventArgs e)
        {
            adapter.ScanTimeout = 10000;
            adapter.ScanMode = ScanMode.Balanced;

            deviceList.Clear();
            adapter.DeviceDiscovered += (s, a) =>
            {
                deviceList.Add(a.Device);
                Debug.WriteLine($"New device found {a.Device.Name}");
            };
            await adapter.StartScanningForDevicesAsync();
            //await adapter.StopScanningForDevicesAsync();
        }

        private async void btnConnect_Clicked(object sender, EventArgs e)
        {
            try
            {
                await adapter.ConnectToDeviceAsync(device);
            }
            catch (DeviceConnectionException a)
            {
                await this.DisplayAlert("Error", a.Message , "Ok");
            }
        }

        private async void btnConnectKnow_Clicked(object sender, EventArgs e)
        {
            try
            {
                await adapter.ConnectToKnownDeviceAsync(new Guid("guid"));
            }
            catch (DeviceConnectionException a)
            {
                await this.DisplayAlert("Error", a.Message, "Ok");
            }
        }

        IList<IService> services;
        IService service;
        private async void btnGetSrvs_Clicked(object sender, EventArgs e)
        {
            services = await device.GetServicesAsync();
            service = await device.GetServiceAsync(Guid.Parse("ffe0ecd2-3d16-4f8d-90de-e89e7fc396a5"));
            //service.
        }

        IList<ICharacteristic> characteristics;
        ICharacteristic characteristic;
        private async void btnGetCaract_Clicked(object sender, EventArgs e)
        {
            characteristics = await service.GetCharacteristicsAsync();
            characteristic = await service.GetCharacteristicAsync(Guid.Parse("d8de624e-140f-4a22-8594-e2216b84a5f2"));
            //characteristic.update    -- Sensores cardiacos, etc......
        }

        private async void btnGetRW_Clicked(object sender, EventArgs e)
        {
            var bytes = await characteristic.ReadAsync();
            await characteristic.WriteAsync(bytes);
        }

        private async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            characteristic.ValueUpdated += (o, args) =>
            {
                var bytes = args.Characteristic.Value;
            };
            await characteristic.StartUpdatesAsync();
            //await characteristic.StopUpdatesAsync();
        }

        IList<IDescriptor> descriptors;
        IDescriptor descriptor;
        private async void btnGetDescriptors_Clicked(object sender, EventArgs e)
        {
            descriptors = await characteristic.GetDescriptorsAsync();
            descriptor = await characteristic.GetDescriptorAsync(Guid.Parse("d8de624e-140f-4a22-8594-e2216b84a5f2"));
            //descriptor.
        }

        private async void btnGetDecsRW_Clicked(object sender, EventArgs e)
        {
            var bytes = await descriptor.ReadAsync();
            await descriptor.WriteAsync(bytes);
        }
    }
}