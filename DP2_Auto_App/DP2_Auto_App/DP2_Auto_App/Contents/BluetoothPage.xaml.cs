using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DP2_Auto_App.Models;
using System.Diagnostics;
using DP2_Auto_App.Models.RestServices;

namespace DP2_Auto_App.Contents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BluetoothPage : ContentPage
    {
        private BluetoothViewModel ViewModel => _viewModel ?? (_viewModel = new BluetoothViewModel());
        private BluetoothViewModel _viewModel;
        public BluetoothPage()
        {
            InitializeComponent();
            ViewModel.GetPairedDevices();
            ListDevices();
        }
        private void ListDevices()
        {
            if (ViewModel.ListOfDevices.Count > 0)
                foreach (var device in ViewModel.ListOfDevices)
                {
                    picker1.Items.Add(device);
                }
            else
            {
                btn_Conectar.IsEnabled = false;
                btn_Desconectar.IsEnabled = false;
            }
            picker1.SelectedIndexChanged += Picker1_SelectedIndexChanged;
            btn_Conectar.Clicked += Btn_Conectar_Clicked;
            btn_Desconectar.Clicked += Btn_Desconectar_Clicked;
            picker2.SelectedIndexChanged += Picker2_SelectedIndexChanged;
            btn_Enviar.Clicked += Btn_Enviar_Clicked;
            btn_Generar.Clicked += Btn_Generar_Clicked;
            Messages();
        }

        private void Btn_Generar_Clicked(object sender, EventArgs e)
        {
            double[] sValues = new double[6];
            BTMessages.isSimulation = true;
            for (int i = 0; i < 6; i++)
            {
                sValues[i] = Randomizer.NextNumber(0,100);
            }
            try
            {
                DependencyService.Get<IConvertionsIT>().ConSend(sValues);
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Attention", ex.Message, "Ok");
            }
        }

        private void Picker1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker1 = (Picker)sender;
            ViewModel.SelectedBthDevice = ViewModel.ListOfDevices[picker1.SelectedIndex];
            btn_Conectar.IsEnabled = true;
        }
        
        private void Btn_Conectar_Clicked(object sender, EventArgs e)
        {
            ViewModel.Connect();
        }

        private void Btn_Desconectar_Clicked(object sender, EventArgs e)
        {
            ViewModel.Disconnect();
        }

        private void Picker2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker2 = (Picker)sender;
            ViewModel.Message = picker2.SelectedIndex.ToString();
        }

        private void Btn_Enviar_Clicked(object sender, EventArgs e)
        {
            ViewModel.Send();
        }
        
        private async void Messages()
        {
            await MessagesAsync();
        }

        private async Task MessagesAsync()
        {
            while (RestService.client != null)
            {
                string message = BTMessages.Messages.returnMessage(BTMessages.statusBT);
                //Debug.WriteLine(message);
                label2.Text = message;
                //Debug.WriteLine(BTMessages.statusBT);
                if(BTMessages.statusBT == 0)
                {
                    btn_Desconectar.IsEnabled = false;
                    btn_Generar.IsEnabled = true;
                }
                else if (BTMessages.statusBT == 1 || BTMessages.statusBT == 4)
                {
                    picker1.IsEnabled = false;
                    picker2.IsEnabled = true;
                    btn_Enviar.IsEnabled = true;
                    btn_Generar.IsEnabled = false;
                }
                else if (BTMessages.statusBT == 2)
                {
                    picker1.IsEnabled = true;
                    picker2.IsEnabled = false;
                    btn_Conectar.IsEnabled = true;
                    btn_Desconectar.IsEnabled = false;
                    btn_Enviar.IsEnabled = false;
                    btn_Generar.IsEnabled = true;
                }
                else if (BTMessages.statusBT == 3)
                {
                    btn_Conectar.IsEnabled = false;
                    btn_Desconectar.IsEnabled = true;
                    btn_Generar.IsEnabled = false;
                }
                await Task.Delay(500);
            }            
        }

    }
}