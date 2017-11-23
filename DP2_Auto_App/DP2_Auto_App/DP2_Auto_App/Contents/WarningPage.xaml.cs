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
    public partial class WarningPage : ContentPage
    {
        Readings speed, temperature, weight, pulse, proximity, battery;
        int min_battery = 90;
        int batery = 100;
        int flag_battery = 0;
        int flag_belt = 0;
        int flag_door = 0;
        int min_heart = 10;
        int flag_heart = 0;
        int min_near = 1;
        int flag_near = 0;
        public WarningPage()
        {
            InitializeComponent();
            ThreadWarnings();
            minLatidos.Text = ""+min_heart;
            minProximidad.Text = ""+min_near;
            minBatt.Text = ""+min_battery;
        }
        void MensajeActivado(string a)
        {
            DisplayAlert("Alerta " + a, "Activado", "Ok");
        }

        void MensajeDesactivado(string a)
        {
            DisplayAlert("Alerta " + a, "Desactivado", "Ok");
        }

        void SwitchBelt(object sender, ToggledEventArgs e)
        {
            if (flag_belt == 0)
            {
                MensajeActivado("Cinturon");
                flag_belt = 1;
            }
            else
            {
                MensajeDesactivado("Cinturon");
                flag_belt = 0;
            }
           
        }


        void SwitchDoor(object sender, ToggledEventArgs e)
        {
            if (flag_door == 0)
            {
                MensajeActivado("Puerta");
                flag_door = 1;
            }
            else
            {
                MensajeDesactivado("Puerta");
                flag_door = 0;
            }
        }
        void SwitchBattery(object sender, ToggledEventArgs e)
        {
            if (flag_battery == 0)
            {
                MensajeActivado("Bateria");
                flag_battery = 1;
                BatteryEmulator();
            }
            else
            {
                MensajeDesactivado("Bateria");
                flag_battery = 0;
            }

        }
        void SwitchProximity(object sender, ToggledEventArgs e)
        {
            if (flag_near == 0)
            {
                MensajeActivado("Proximidad");
                flag_near = 1;
            }
            else
            {
                MensajeDesactivado("Proximidad");
                flag_near = 0;
            }
        }
        void SwitchHeart(object sender, ToggledEventArgs e)
        {
            if (flag_heart == 0)
            {
                MensajeActivado("Ritmo Cardiaco");
                flag_heart = 1;
            }
            else
            {
                MensajeDesactivado("Ritmo Cardiaco");
                flag_heart = 0;
            }
        }


        private async void BatteryEmulator()
        {
            while (batery>0)
            {
                batery -= 1;
                await Task.Delay(1000);
            }
        }

        private async void ThreadWarnings()
        {
            List<Readings> r;
            while (true)
            {
                battery = pulse = proximity= null;
                if (flag_heart == 1)
                {
                    r = await webService.rest.getReadingList(Readings.PULSE);
                    if (r != null && r.Count > 0) pulse = r.First();
                }

                if (flag_near == 1)
                {
                    r = await webService.rest.getReadingList(Readings.PROXIMITY);
                    if (r != null && r.Count > 0) proximity = r.First();
                }

                if (flag_battery == 1)
                {
                    r = await webService.rest.getReadingList(Readings.BATTERY);
                    if (r != null && r.Count > 0) battery = r.First();
                }

                if (battery != null)
                {
                    if (battery.value < min_battery)
                        DisplayAlert("Alerta", "Bateria Baja", "Ok");
                }

                /*if (batery > 0 && flag_battery==1)
                {
                    if(batery<min_battery)
                        await DisplayAlert("Alerta", "Bateria Baja", "Ok");
                }*/

                if(pulse != null)
                {
                    if (pulse.value < min_heart)
                        DisplayAlert("Alerta", "Pulso Bajo", "Ok");
                }

                if(proximity!=null)
                {
                    if (proximity.value < min_near)
                        DisplayAlert("Alerta", "Cuidado, Objeto cerca al vehiculo", "Ok");
                }

                await Task.Delay(1000);
            }
        }

        //faltaria mandar los minimos al servicio
        private void ButtonLatidos()
        {
            int newvalue = int.Parse(minLatidos.Text);
            min_heart = newvalue;
            DisplayAlert("Mensaje", "Valores Cambiados", "Ok");
        }

        private void ButtonProx()
        {
            int newvalue = int.Parse(minProximidad.Text);
            min_near = newvalue;
            DisplayAlert("Mensaje", "Valores Cambiados", "Ok");
        }

        private void ButtonBatt()
        {
            int newvalue = int.Parse(minBatt.Text);
            min_battery = newvalue;
            DisplayAlert("Mensaje", "Valores Cambiados", "Ok");
        }
    }
}