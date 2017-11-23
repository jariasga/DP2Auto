using DP2_Auto_App.Models.RestServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
        public bool estado = false; //activacion/desactivacion de automatica o no la persina
        
        public ParkingPage()
        {
            InitializeComponent();
            ChangeData();
        }
        
        private void ChangeData()
        {
            btn_actualizar.Clicked += Btn_Actualizar_Clicked;
        }

        private void Btn_Actualizar_Clicked(object sender, EventArgs e)
        {
            if (estado == false)
            {
                Mensaje(3);
                btn_actualizar.Text = "Detener";
                estado = true;
            }
            else
            {
                Mensaje(4);
                btn_actualizar.Text = "Actualizar";
                estado = false;
            }

            UpdateSensors();
        }

        private async void UpdateSensors()
        {
            List<Readings> r;
            int counter = 0;
            while (estado)
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