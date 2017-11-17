using System;
using Xamarin.Forms;
using DP2_Auto_App.Models;

namespace DP2_Auto_App
{

    public class Bluetooth : ContentPage
    {
        private BluetoothViewModel ViewModel => _viewModel ?? (_viewModel = new BluetoothViewModel());
        private BluetoothViewModel _viewModel;
        private bool stillConnected;
        public Bluetooth()
        {
            stillConnected = false;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.GetPairedDevices();
            ViewModel.GetMessagesData();
            Content = GetLayout();
        }
        public StackLayout GetLayout()
        {
            

            var header = new Label
            {
                FontSize = 18,
                Text = "Seleccciona un  Dispositivo",
                TextColor = Color.Brown
            };
            int v = 0;
            var deviceSelect = new Picker();
            foreach (var device in ViewModel.ListOfDevices)
            {
                deviceSelect.Items.Add(device);
                v++;
            }
            if (v == 0)
            {
                Application.Current.MainPage.DisplayAlert("Atención", "Para hacer uso de esta funcion debe activar el bluetooth del dispositivo", "Ok");
            }

            var connectButton = new Button
            {
                Text = "Conectar",
                TextColor = Color.Blue
            };
            connectButton.Clicked += ConnectButton_Clicked;

            var header2 = new Label
            {
                FontSize = 18,
                Text = "Manejar el auto",
                TextColor = Color.Brown
            };
            
            TableView tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection
                    {
                        new SwitchCell
                        {
                            Text = "Encendido:",
                            On = true
                        },
                        new SwitchCell
                        {
                            Text = "Habilitado:",
                            On = false
                        },
                        new SwitchCell
                        {
                            Text = "Modo Robo:",
                            On = false
                        },
                        new SwitchCell
                        {
                            Text = "Cinturon:",
                            On = true
                        }
                    }
                }
            };
            
            var patternPicker = new Picker
            {
                Items = { "0", "1", "2", "3", "4" }
            };
            
            patternPicker.SelectedIndexChanged += PatternPicker_SelectedIndexChanged;
            var sendButton = new Button
            {
                Text = "Enviar",
                TextColor = Color.Blue
            };
            sendButton.Clicked += SendButton_Clicked;
            deviceSelect.SelectedIndexChanged += DeviceSelect_SelectedIndexChanged;

            var disconnectButton = new Button
            {
                Text = "Desconectar",
                TextColor = Color.Blue
            };
            disconnectButton.Clicked += DisconnectButton_Clicked;
            
            var verificartrama = new Button
            {
                Text = "Verficar Trama",
                TextColor = Color.Blue
            };
            verificartrama.Clicked += Verificartrama_Clicked;

            var deviceMessages = new Picker();
            var datamsg = new Label();
            foreach (var data in ViewModel.ListMessages)
            {
                datamsg.Text = data;
            }


            

            return new StackLayout
            {
                Padding = 20,
                Children =
                {
                    header,
                    deviceSelect,
                    connectButton,
                    header2,
                    tableView,
                    patternPicker,
                    sendButton,
                    disconnectButton,
                    verificartrama,
                    datamsg
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
            stillConnected = true;
        }
        private void DisconnectButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.Disconnect();
            stillConnected = false;
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

        private async void keepListening()
        {
            while (stillConnected)
            {
                try
                {
                    DependencyService.Get<IConvertionsIT>().ConReceived("7EAB04ACE9F01564F04584F05828F064837E0404");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Attention", ex.Message, "Ok");
                }
            }
        }

    }
}
