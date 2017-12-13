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
        public static bool sensorLoop;
        public SensorPage()
        {
            InitializeComponent();
            sensorLoop = false;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sensorLoop)
            {
                button_Actualizar.Text = "Actualizar";
                sensorLoop = false;
            }
            else
            {
                sensorLoop = true;
                updateSensorValues();
            }
        }

        private async void updateSensorValues()
        {
            button_Actualizar.Text = "Detener";
            List<Readings> r;
            int counter = 0;
            string updatedAt = "";
            while (sensorLoop)
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

                    if (speed.value < 30) bt_speed.BackgroundColor = Color.Green;
                    else if (speed.value < 60) bt_speed.BackgroundColor = Color.Yellow;
                    else bt_speed.BackgroundColor = Color.Red;
                }
                else Debug.WriteLine(" ---> Velocidad NO actualizada");

                if (temperature != null)
                {
                    Debug.WriteLine("Temperatura: " + temperature.value);
                    label_temperature.Text = temperature.value + " C";
                    updatedAt = DateTime.Now.ToString("h:mm:ss tt");

                    if (temperature.value < 30) bt_temperature.BackgroundColor = Color.Green;
                    else if (temperature.value < 60) bt_temperature.BackgroundColor = Color.Yellow;
                    else bt_temperature.BackgroundColor = Color.Red;
                }
                else Debug.WriteLine(" ---> Temperatura NO actualizada");

                if (weight != null)
                {
                    Debug.WriteLine("Peso: " + weight.value);
                    label_weight.Text = weight.value + " Kg";
                    updatedAt = DateTime.Now.ToString("h:mm:ss tt");

                    if (weight.value < 50) bt_weight.BackgroundColor = Color.Green;
                    else if (weight.value < 90) bt_weight.BackgroundColor = Color.Yellow;
                    else bt_weight.BackgroundColor = Color.Red;
                }
                else Debug.WriteLine(" ---> Peso NO actualizado");

                if (pulse != null)
                {
                    Debug.WriteLine("Pulso: " + pulse.value);
                    label_pulse.Text = pulse.value + " p/m";
                    updatedAt = DateTime.Now.ToString("h:mm:ss tt");

                    if (pulse.value < 60) bt_pulse.BackgroundColor = Color.Red;
                    else if (pulse.value < 80) bt_pulse.BackgroundColor = Color.Yellow;
                    else if (pulse.value < 120) bt_pulse.BackgroundColor = Color.Green;
                    else if (pulse.value < 160) bt_pulse.BackgroundColor = Color.Yellow;
                    else bt_pulse.BackgroundColor = Color.Red;
                }
                else Debug.WriteLine(" ---> Pulso NO actualizado");

                if (proximity != null)
                {
                    Debug.WriteLine("Proximidad: " + proximity.value);
                    label_proximity.Text = proximity.value + " m";
                    updatedAt = DateTime.Now.ToString("h:mm:ss tt");

                    if (proximity.value < 5) bt_proximity.BackgroundColor = Color.Red;
                    else if (proximity.value < 15) bt_proximity.BackgroundColor = Color.Yellow;
                    else bt_proximity.BackgroundColor = Color.Green;
                }
                else Debug.WriteLine(" ---> Proximidad NO actualizada");

                if (battery != null)
                {
                    Debug.WriteLine("Bateria: " + battery.value);
                    label_battery.Text = battery.value + " %";
                    updatedAt = DateTime.Now.ToString("h:mm:ss tt");

                    if (battery.value < 30) bt_battery.BackgroundColor = Color.Red;
                    else if (battery.value < 60) bt_battery.BackgroundColor = Color.Yellow;
                    else bt_battery.BackgroundColor = Color.Green;
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