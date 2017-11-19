using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DP2_Auto_App.Models.RestServices;
using System.Diagnostics;

namespace DP2_Auto_App.Contents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Parking : ContentPage
    {
        public Parking()
        {
            InitializeComponent();
            ChangeData();
        }
        
        private void ChangeData()
        {
            btn_actualizar.Clicked += Btn_Actualizar_Clicked;
            btn_Restar.Clicked += Btn_Restar_Clicked;
            btn_Sumar.Clicked += Btn_Sumar_Clicked;
            sw.Toggled += Sw_Toggled;
            btn_angulo.Clicked += Btn_angulo_Clicked;
        }

        private void Btn_Actualizar_Clicked(object sender, EventArgs e)
        {
            UpdateSensors();
        }

        private void Btn_Sumar_Clicked(object sender, EventArgs e)
        {
            if (label_Angulo.Text != "90")
            {
                int angulo;
                var an = label_Angulo.Text;
                Int32.TryParse(an, out angulo);
                angulo += 5;
                label_Angulo.Text = angulo.ToString();
            }
        }

        private void Btn_Restar_Clicked(object sender, EventArgs e)
        {
            if (label_Angulo.Text != "0")
            {
                int angulo;
                var an = label_Angulo.Text;
                Int32.TryParse(an, out angulo);
                angulo -= 5;
                label_Angulo.Text = angulo.ToString(); 
            }
        }
        
        private void Sw_Toggled(object sender, ToggledEventArgs e)
        {
            var value = e.Value.ToString();
            if (value == "True")
            {
                DisplayAlert("Atencion", "Para poder manipular el angulo debe desactivar el modo automático", "OK");
            }
        }
        
        private void Btn_angulo_Clicked(object sender, EventArgs e)
        {
            //
        }

        private async void UpdateSensors()
        {
            while (true)
            {
                label_Temperatura.Text = await webService.rest.getReadingInfo(Readings.TEMPERATURE) + " °C" ;
                label_Humedad.Text = await webService.rest.getReadingInfo(Readings.HUMIDITY) + " % ";
                label_Iluminacion.Text = await webService.rest.getReadingInfo(Readings.ILUMINITY) + " Lux";
                //label_Uv.Text = await webService.rest.getReadingInfo(Readings.UV) + " Uv";
                Debug.WriteLine("Datos Actualizados");
                await Task.Delay(20000);
            }
        }
    }
}