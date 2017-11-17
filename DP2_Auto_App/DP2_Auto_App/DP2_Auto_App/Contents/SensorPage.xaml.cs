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
            updateSensorValues();
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
                label_battery.Text = await webService.rest.getReadingInfo(Readings.BATTERY) + " %";
                
                await Task.Delay(2000);
            }            
        }
    }
}