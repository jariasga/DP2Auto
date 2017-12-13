using DP2_Auto_App.Models.RestServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DP2_Auto_App.Contents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParkingPage1 : ContentPage
    {

        private static readonly HttpClient client = new HttpClient(); // Creando el cliente
        public int vsw = 0; //estado en el que esta el switch de modo automatico
        public int ang = 45; // inicio del angulo de persiana
        public string vmodo; //valor del modo
        public string vangulo; //valor del angulo
        public ParkingPage1()
        {
            InitializeComponent();
            ChangeData1();
        }
        private void ChangeData1()
        {
            btn_Restar.Clicked += Btn_Restar_Clicked;
            btn_Sumar.Clicked += Btn_Sumar_Clicked;
            sw.Toggled += Sw_Toggled;
            btn_angulo.Clicked += Btn_angulo_Clicked;
        }

        private async void SendData()
        {
            //var r = await webService.rest.getVehicleInfo(284);
            if (vsw == 1)
            {
                vmodo = "auto";
                vangulo = "0";
            }
            else if (vsw == 0)
            {
                vmodo = "manual";
                vangulo = ang.ToString();
            }
            await SendDataAsync(RestService.parking_ip);
        }

        public async Task SendDataAsync(string ip)
        {
            var values = new Dictionary<string, string>
            {
                { "modo", vmodo },
                { "angulo", vangulo }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://"+ip+"/prueba.php", content);

            var responseString = await response.Content.ReadAsStringAsync();
            if (responseString == "{\"estado\":\"exito\"}")
            {
                Mensaje(6);
            }
            else if (responseString == "{\"estado\":\"error\"}")
            {
                Mensaje(5);
            }
            Debug.WriteLine(responseString);
        }

        private void Btn_Sumar_Clicked(object sender, EventArgs e)
        {
            if (ang != 90)
            {
                ang += 15;
                label_Angulo.Text = ang.ToString() + "°";
            }
            else if (ang == 90)
            {
                Mensaje(1);
            }
        }

        private void Btn_Restar_Clicked(object sender, EventArgs e)
        {
            if (ang != 0)
            {
                ang -= 15;
                label_Angulo.Text = ang.ToString() + "°";
            }
            else if (ang == 0)
            {
                Mensaje(2);
            }
        }

        public void Sw_Toggled(object sender, ToggledEventArgs e)
        {
            var value = e.Value.ToString();
            if (value == "True")
            {
                vsw = 1;
                btn_Restar.IsEnabled = false;
                btn_Sumar.IsEnabled = false;
            }
            else if (value == "False")
            {
                vsw = 0;
                btn_Restar.IsEnabled = true;
                btn_Sumar.IsEnabled = true;
            }
        }

        private void Btn_angulo_Clicked(object sender, EventArgs e)
        {
            SendData();
        }

        public void Mensaje(int num)
        {
            switch (num)
            {
                case 1:
                    DisplayAlert("Atención", "El máximo angulo de la persiana es 90°", "OK");
                    break;
                case 2:
                    DisplayAlert("Atención", "El mínimo angulo de la persiana es 0°", "OK");
                    break;
                case 3:
                    DisplayAlert("Atención", "Desde ahora se actualizaran automaticamente los datos de los sensores", "OK");
                    break;
                case 4:
                    DisplayAlert("Atención", "La actualización automática se ha detenido", "OK");
                    break;
                case 5:
                    DisplayAlert("Atención", "Los datos no se enviaron correctamente, intente de nuevo", "OK");
                    break;
                case 6:
                    DisplayAlert("Atención", "Datos enviados correctamente", "OK");
                    break;
            }
        }
    }
}