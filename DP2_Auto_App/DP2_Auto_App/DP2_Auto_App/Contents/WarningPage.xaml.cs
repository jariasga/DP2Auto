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
        int maxWeight;
        int maxBeat;
        public WarningPage()
        {
            InitializeComponent();
            maxWeight = 50;
            maxBeat = 100;
            weight_text.Text = "" + getMaxWeight();
            beat_text.Text = "" + getMaxBeat();
        }

        void setMaxWeight(int newValue)
        {
            //consumir servicio de cambio de valores
            this.maxWeight = newValue;
            weight_text.Text = "" + getMaxWeight();
        }

        void setMaxBeat(int newValue)
        {
            //consumir servicio de cambio de valores
            this.maxBeat = newValue;
            beat_text.Text = "" + getMaxBeat();
        }

        int getMaxWeight()
        {
            //consumir servicio de cambio de valores
            return this.maxWeight;
        }

        int getMaxBeat()
        {
            //consumir servicio de cambio de valores
            return this.maxBeat;
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

        void SwitchWeight(object sender, ToggledEventArgs e)
        {
            DisplayAlert("Alerta de Peso máximo", "Activado", "Ok");
        }

        async void ChangeHeartbeat(object sender, EventArgs e)
        {
            int nuevo=int.Parse(beat_text.Text);
            var rpta = await DisplayAlert("Aleta de Cambio", "¿Está seguro de realizar los cambios?", "Si", "No");
            string salida = "" + rpta;
            if (salida == "Si")
            {
                setMaxWeight(nuevo);
                //consumir servicio de cambio de valores (en el set)
            }
        }

        async void ChangeWeight(object sender, EventArgs e)
        {
            int nuevo = int.Parse(weight_text.Text);
            var rpta = await DisplayAlert("Aleta de Cambio","¿Está seguro de realizar los cambios?", "Si", "No");
            string salida = "" + rpta;
            if(salida == "Si")
            {
                setMaxBeat(nuevo);
                //consumir servicio de cambio de valores (en el set)
            }


        }
    }
}