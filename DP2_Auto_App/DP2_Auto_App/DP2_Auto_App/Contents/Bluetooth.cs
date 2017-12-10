using DP2_Auto_App;
using System;
using Xamarin.Forms;

namespace BuetoothToArduinoTest
{

    public class MainPage : ContentPage
    {
        private BluetoothViewModel ViewModel => _viewModel ?? (_viewModel = new BluetoothViewModel());
        private BluetoothViewModel _viewModel;
        public MainPage()
        {


        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.GetPairedDevices();
            Content = GetLayout();
        }
        private StackLayout GetLayout()
        {
            var header = new Label
            {
                FontSize = 16,
                Text = "Select a Device"
            };
            var deviceSelect = new Picker();
            foreach (var device in ViewModel.ListOfDevices)
            {
                deviceSelect.Items.Add(device);
            }
            var connectButton = new Button
            {
                Text = "connect"
            };
            connectButton.Clicked += ConnectButton_Clicked;
            var patternPicker = new Picker
            {
                Items = { "Color Wipe", "Theater Chase", "Rainbow Cycle", "Rainbow Chase", "Stop" }
            };
            patternPicker.SelectedIndexChanged += PatternPicker_SelectedIndexChanged;
            var sendButton = new Button
            {
                Text = "Send",
            };
            sendButton.Clicked += SendButton_Clicked;
            deviceSelect.SelectedIndexChanged += DeviceSelect_SelectedIndexChanged;
            var disconnectButton = new Button
            {
                Text = "disconnect"
            };
            disconnectButton.Clicked += DisconnectButton_Clicked;
            return new StackLayout
            {
                Children =
                {
                    header,
                    deviceSelect,
                    connectButton,
                    patternPicker,
                    sendButton,
                    disconnectButton,
                }
            };
        }
        private void PatternPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var patternPicker = (Picker)sender;
            ViewModel.Message = patternPicker.SelectedIndex.ToString();
        }
        private void ConnectButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.Connect();
        }
        private void DisconnectButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.Disconnect();
        }
        private void SendButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.Send();
        }
        private void DeviceSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            var deviceSelect = (Picker)sender;
            ViewModel.SelectedBthDevice = ViewModel.ListOfDevices[deviceSelect.SelectedIndex];
        }
    }
}
