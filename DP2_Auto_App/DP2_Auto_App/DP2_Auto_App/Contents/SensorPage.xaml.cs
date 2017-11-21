using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DP2_Auto_App.Models.RestServices;
using System.Threading;
using System.Diagnostics;

namespace DP2_Auto_App.Contents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SensorPage : ContentPage
    {
        Readings speed, temperature, weight, pulse, proximity, battery;
        public SensorPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            updateSensorValues();
            button_Actualizar.IsEnabled = false;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            //Create random data

        }

        private async void updateSensorValues()
        {
            List<Readings> r;
            int counter = 0;
            string updatedAt = "";
            while (true)
            {
                speed = temperature = weight = pulse = proximity = battery = null;
                r = await webService.rest.getReadingList(Readings.SPEED);
                if (r != null && r.Count > 0) speed = r.First();

                r = await webService.rest.getReadingList(Readings.TEMPERATURE);
                if (r != null && r.Count > 0) temperature = r.First();

                r = await webService.rest.getReadingList(Readings.WEIGHT);
                if (r != null && r.Count > 0) weight = r.First();

                r = await webService.rest.getReadingList(Readings.PULSE);
                if (r != null && r.Count > 0) pulse = r.First();

                r = await webService.rest.getReadingList(Readings.PROXIMITY);
                if (r != null && r.Count > 0) proximity = r.First();

                r = await webService.rest.getReadingList(Readings.BATTERY);
                if (r != null && r.Count > 0) battery = r.First();
                
                if (speed != null)
                {
                    Debug.WriteLine("Velocidad: " + speed.value);
                    label_speed.Text = speed.value + " km/h";
                    updatedAt = DateTime.Now.ToString("h:mm:ss tt");
                }
                else Debug.WriteLine(" ---> Velocidad NO actualizada");

                if (temperature != null)
                {
                    Debug.WriteLine("Temperatura: " + temperature.value);
                    label_temperature.Text = temperature.value + " C";
                    updatedAt = DateTime.Now.ToString("h:mm:ss tt");
                }
                else Debug.WriteLine(" ---> Temperatura NO actualizada");

                if (weight != null)
                {
                    Debug.WriteLine("Peso: " + weight.value);
                    label_weight.Text = weight.value + " Kg";
                    updatedAt = DateTime.Now.ToString("h:mm:ss tt");
                }
                else Debug.WriteLine(" ---> Peso NO actualizado");

                if (pulse != null)
                {

                    Debug.WriteLine("Pulso: " + pulse.value);
                    label_pulse.Text = pulse.value + " p/m";
                    updatedAt = DateTime.Now.ToString("h:mm:ss tt");
                }
                else Debug.WriteLine(" ---> Pulso NO actualizado");

                if (proximity != null)
                {
                    Debug.WriteLine("Proximidad: " + proximity.value);
                    label_proximity.Text = proximity.value + " m";
                    updatedAt = DateTime.Now.ToString("h:mm:ss tt");
                }
                else Debug.WriteLine(" ---> Proximidad NO actualizada");

                if (battery != null)
                {
                    Debug.WriteLine("Bateria: " + battery.value);
                    label_battery.Text = battery.value + " %";
                    updatedAt = DateTime.Now.ToString("h:mm:ss tt");
                }
                else Debug.WriteLine(" ---> Bateria NO actualizada");

                
                counter++;

                label_actualizado.Text = "Actualizado: " + updatedAt;
                Debug.WriteLine("-------------- Datos del estado actualiado --------------");
                Debug.WriteLine("<<<<<<<<<<<<<< Bucle: " + counter + " finalizado");               
                await Task.Delay(1000);
            }
        }
    }
}