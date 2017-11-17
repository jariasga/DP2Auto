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
        string bateria;
        string proximidad;
        public SensorPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            updateSensorValues();
            double valueBattery, valueProximity;
            bateria = label_battery.Text;
            proximidad = label_proximity.Text;
            try
            {
                valueBattery = Convert.ToDouble(label_battery.Text);
            }
            catch (Exception)
            {
                DisplayAlert("Alerta", "Error", "Ok");
            }
            valueBattery = Convert.ToDouble(label_battery.Text);
            if (valueBattery <= 15)
            {
                DisplayAlert("Alerta", "Batería baja!", "Ok");
            }
            else if (valueBattery <= 10)
            {
                DisplayAlert("Alerta", "Bateria muy baja!", "Ok");
            }

            /*try
            {
                valueProximity = Convert.ToDouble(label_proximity.Text);
            }
            catch (Exception)
            {
                DisplayAlert("Alerta", "Error", "Ok");
            }
            valueProximity = Convert.ToDouble(label_proximity.Text);
            if (valueProximity <= 1000)
            {
                DisplayAlert("Alerta", "Batería baja!", "Ok");
            }
            else if (valueProximity <= 10)
            {
                DisplayAlert("Alerta", "Bateria muy baja!", "Ok");
            }*/

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            //Create random data

        }

        private async void updateSensorValues()
        {
            while (true)
            {
                label_speed.Text = await webService.rest.getReadingInfo(Readings.SPEED) + " km/h";
                label_temperature.Text = await webService.rest.getReadingInfo(Readings.TEMPERATURE) + " C";
                label_weight.Text = await webService.rest.getReadingInfo(Readings.WEIGHT) + " Kg";
                label_pulse.Text = await webService.rest.getReadingInfo(Readings.PULSE) + " p/m";
                label_proximity.Text = await webService.rest.getReadingInfo(Readings.PROXIMITY) + " m";
                //label_proximity.Text = await webService.rest.getReadingInfo(Readings.PROXIMITY);
                label_battery.Text = await webService.rest.getReadingInfo(Readings.BATTERY) + " %";
                //label_battery.Text = await webService.rest.getReadingInfo(Readings.BATTERY);

                /*bateria = label_battery.Text;
                proximidad = await webService.rest.getReadingInfo(Readings.PROXIMITY);*/
                Debug.WriteLine("Datos del estado actualiado");
            }
                await Task.Delay(2000);
            
        }
    }
}