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
            await webService.rest.getReadingInfo(Readings.SPEED);
            await webService.rest.getReadingInfo(Readings.TEMPERATURE);
            await webService.rest.getReadingInfo(Readings.WEIGHT);
            await webService.rest.getReadingInfo(Readings.PULSE);
            await webService.rest.getReadingInfo(Readings.PROXIMITY);
            await webService.rest.getReadingInfo(Readings.BATTERY);
            await DisplayAlert("Correcto", "Actualizado", "Ok");
        }
    }
}