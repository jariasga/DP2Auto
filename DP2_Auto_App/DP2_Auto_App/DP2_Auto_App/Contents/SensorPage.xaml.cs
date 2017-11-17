using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DP2_Auto_App.Models.RestServices;
using System.Threading;

namespace DP2_Auto_App.Contents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SensorPage : ContentPage
    {
        
        public SensorPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Task.Run(async () => updateSensorValues());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            //Create random data

        }

        private Task updateSensorValues()
        {
            while (true)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    label_speed.Text = webService.rest.getReadingInfo(Readings.SPEED) + " km/h";
                    label_temperature.Text = webService.rest.getReadingInfo(Readings.TEMPERATURE) + " C";
                    label_weight.Text = webService.rest.getReadingInfo(Readings.WEIGHT) + " Kg";
                    label_pulse.Text = webService.rest.getReadingInfo(Readings.PULSE) + " p/m";
                    label_proximity.Text = webService.rest.getReadingInfo(Readings.PROXIMITY) + " m";
                    label_battery.Text = webService.rest.getReadingInfo(Readings.BATTERY) + " %";
                    DisplayAlert("Correcto", "Actualizado", "Ok");
                    Task.Delay(2000);
                });
            }
        }
    }
}