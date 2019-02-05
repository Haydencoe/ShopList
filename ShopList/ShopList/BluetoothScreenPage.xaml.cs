using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

using Plugin.BLE;

using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;


namespace ShopList
{

    public static class ConnectedDevice
    {

       public static IDevice globalDevice;

    }

        public partial class BluetoothScreenPage : ContentPage
    {

        IAdapter adapter;
        IBluetoothLE bluetoothBLE;
        ObservableCollection<IDevice> list;
        IDevice device;



        public BluetoothScreenPage()
        {
            InitializeComponent();

            bluetoothBLE = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;

            list = new ObservableCollection<IDevice>();
            DevicesList.ItemsSource = list;


        }


        private async void searchDevice(object sender, EventArgs and)
        {
            if (bluetoothBLE.State == BluetoothState.Off)
            {
                await DisplayAlert(" Attention ", " Bluetooth disabled ", " OK ");
            }
            else
            {
                list.Clear();

                adapter.ScanTimeout = 10000;
                adapter.ScanMode = ScanMode.Balanced;


                adapter.DeviceDiscovered += (obj, a) =>
                {
                    if (!list.Contains(a.Device))
                        list.Add(a.Device);
                };

                await adapter.StartScanningForDevicesAsync();

            }

        }

        private async void DevicesList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            device = DevicesList.SelectedItem as IDevice;

            var result = await DisplayAlert(" WARNING ", " Do you want to connect to this device? ", " Connect ", " Cancel ");

            if (!result)
                return;

            // Stop Scanner
            await adapter.StopScanningForDevicesAsync();

            try
            {
                await adapter.ConnectToDeviceAsync(device);

                await DisplayAlert("Connection", "Status: " + device.State, " OK ");

                if (device.State == Plugin.BLE.Abstractions.DeviceState.Connected)
                {
                    ConnectedDevice.globalDevice = device;

                    await Application.Current.MainPage.Navigation.PopModalAsync(true);
                }

            }
            catch (DeviceConnectionException ex)
            {
                await DisplayAlert(" Error ", ex.Message, " OK ");
            }

        }



    }
}
