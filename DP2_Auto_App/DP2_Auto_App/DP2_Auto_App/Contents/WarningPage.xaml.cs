using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DP2_Auto_App.Contents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WarningPage : ContentPage
    {
        public WarningPage()
        {
            InitializeComponent();
        }
        void SwitchBelt(object sender, ToggledEventArgs e)
        {
            DisplayAlert("Alerta Cinturón", "Activado", "Ok");
        }
        void SwitchDoor(object sender, ToggledEventArgs e)
        {
            DisplayAlert("Alerta de Puertas", "Activado", "Ok");
        }
        void SwitchBattery(object sender, ToggledEventArgs e)
        {
            DisplayAlert("Alerta de Bateria", "Activado", "Ok");
        }
        void SwitchProximity(object sender, ToggledEventArgs e)
        {
            DisplayAlert("Alerta de Proximidad", "Activado", "Ok");
        }
        void SwitchHeart(object sender, ToggledEventArgs e)
        {
            DisplayAlert("Alerta de Ritmo Cardíaco", "Activado", "Ok");
        }
    }
}