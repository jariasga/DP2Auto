using System;
using Xamarin.Forms;
using DP2_Auto_App.Models;

namespace DP2_Auto_App
{

    public class Bluetooth : ContentPage
    {
        private BluetoothViewModel ViewModel => _viewModel ?? (_viewModel = new BluetoothViewModel());
        private BluetoothViewModel _viewModel;
        public Bluetooth()
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

            Application.Current.MainPage.DisplayAlert("Atencion", "Para hacer uso de esta funcion debe activar el bluetooth del dispositivo", "Ok");

            var header = new Label
            {
                FontSize = 16,
                Text = "Seleccciona un  Dispositivo"
            };
            var deviceSelect = new Picker();
            foreach (var device in ViewModel.ListOfDevices)
            {
                deviceSelect.Items.Add(device);
                
            }
            var connectButton = new Button
            {
                Text = "Conectar",
                TextColor = Color.Blue
                
            };
            

            connectButton.Clicked += ConnectButton_Clicked;

            var header2 = new Label
            {
                FontSize = 16,
                Text = "Manejar el auto"
            };
            var patternPicker = new Picker
            {
                Items = { "Color Wipe", "Theater Chase", "Rainbow Cycle", "Rainbow Chase", "Stop" }
            };
            patternPicker.SelectedIndexChanged += PatternPicker_SelectedIndexChanged;
            var sendButton = new Button
            {
                Text = "Enviar",
            };
            sendButton.Clicked += SendButton_Clicked;
            deviceSelect.SelectedIndexChanged += DeviceSelect_SelectedIndexChanged;
            var disconnectButton = new Button
            {
                Text = "Desconectar"
            };
            disconnectButton.Clicked += DisconnectButton_Clicked;
            
            var verificartrama = new Button
            {
                Text = "Verficar Trama"
            };
            verificartrama.Clicked += Verificartrama_Clicked;
            return new StackLayout
            {
                Padding = 20,
                Children =
                {
                    header,
                    deviceSelect,
                    connectButton,
                    header2,
                    patternPicker,
                    sendButton,
                    disconnectButton,
                    verificartrama
                }
            };
        }

        




        //Reference for libraries
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


        private void Verificartrama_Clicked(object sender, EventArgs e)
        {
            try
            {
                DependencyService.Get<IConvertionsIT>().ConReceived("7EAB04ACE9F01564F04584F05828F064837E0404");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Attention", ex.Message, "Ok");
            }
        }

    }
}
