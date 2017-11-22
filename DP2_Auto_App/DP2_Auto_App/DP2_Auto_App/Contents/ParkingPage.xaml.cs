using DP2_Auto_App.Models.RestServices;
using Newtonsoft.Json;
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
    public partial class ParkingPage : ContentPage
    {
        Readings temperature, iluminity, humidity, uv;
        HttpClient webClient;
        Uri baseAddress, uri;
        public static Client client { get; private set; }
        public int vsw = 0;
        public int ang= 45; 
        public ParkingPage()
        {

            InitializeComponent();
            ChangeData();

            baseAddress = new Uri("http://dp2.iamallama.com/api/");
            webClient = new HttpClient();
            webClient.MaxResponseContentBufferSize = 256000;
            webClient.BaseAddress = baseAddress;
            
        }

        private void ChangeData()
        {
            btn_actualizar.Clicked += Btn_Actualizar_Clicked;
            btn_Restar.Clicked += Btn_Restar_Clicked;
            btn_Sumar.Clicked += Btn_Sumar_Clicked;
            sw.Toggled += Sw_Toggled;
            btn_angulo.Clicked += Btn_angulo_Clicked;
        }

        private void Btn_Actualizar_Clicked(object sender, EventArgs e)
        {
            UpdateSensors();
        }

        private void Btn_Sumar_Clicked(object sender, EventArgs e)
        {   
            if (ang != 90)
            {
                ang += 5;
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
                ang -= 5;
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
            }
        }

        private void Btn_angulo_Clicked(object sender, EventArgs e)
        {
            SendData();
        }

        private async void UpdateSensors()
        {
            List<Readings> r;
            int counter = 0;
            while (true)
            {
                temperature = iluminity = humidity = uv = null;
                r = await webService.rest.getReadingList(Readings.HUMIDITY);
                if (r != null && r.Count > 0) humidity = r.First();

                r = await webService.rest.getReadingList(Readings.TEMPERATURE);
                if (r != null && r.Count > 0) temperature = r.First();

                r = await webService.rest.getReadingList(Readings.ILUMINITY);
                if (r != null && r.Count > 0) iluminity = r.First();

                r = await webService.rest.getReadingList(Readings.UV);
                if (r != null && r.Count > 0) uv = r.First();
                
                if (temperature != null)
                {
                    label_Temperatura.Text = temperature.value + "  °C";
                }
                if (iluminity != null)
                {
                    label_Iluminacion.Text = iluminity.value + "  Lux";
                }
                if (humidity != null)
                {
                    label_Humedad.Text = humidity.value + "  %";
                }
                if (uv != null)
                {
                    label_Uv.Text = uv.value + "  UV";
                }

                counter++;

                Debug.WriteLine("Datos Actualizados");
                await Task.Delay(20000);
            }
        }
        
        private async void SendData()
        {
            client = new Client();
            Debug.WriteLine(vsw);
            Users user = new Users
            {
                email = "prueba20@gmail.com",//label_Username.Text,
                password = "12345678"//label_Password.Text
            };
            /*
            if (vsw == 1)
            {
                Parking parking = new Parking
                {
                    modo = "auto",
                    angulo = "0"
                };
            }
            else
            {
                Parking parking = new Parking
                {
                    modo = "manual",
                    angulo = label_Angulo.Text
                };
            }*/
            await SendDataAsync(user);
        }


        public async Task<string> SendDataAsync(Users user)
        {
            uri = new Uri(baseAddress, "login");
            client = new Client();
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await webClient.PostAsync(uri, content);

                var rString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(rString);
                if (response.IsSuccessStatusCode)
                {
                    //client = JsonConvert.DeserializeObject<Client>(rString);
                    return "datasend";
                }
            }
            catch (Exception ex)
            {
                return "connectionProblem: " + ex.Message;
            }
            return "notsend";
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
                    break;
            }
        }

    }
}