using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DP2_Auto_App.Models.RestServices;

namespace DP2_Auto_App.Contents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SensorPage : ContentPage
    {
        public SensorPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            label_speed.Text = await webService.rest.getReadingInfo(Readings.SPEED);
            label_temperature.Text = await webService.rest.getReadingInfo(Readings.TEMPERATURE);
            label_weight.Text = await webService.rest.getReadingInfo(Readings.WEIGHT);
            label_pulse.Text = await webService.rest.getReadingInfo(Readings.PULSE);
            label_proximity.Text = await webService.rest.getReadingInfo(Readings.PROXIMITY);
            label_battery.Text = await webService.rest.getReadingInfo(Readings.BATTERY);
            await DisplayAlert("Correcto", "Actualizado", "Ok");
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}