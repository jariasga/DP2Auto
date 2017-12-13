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
        int flag_msg_heart = 0;
        int flag_msg_near = 0;
        int flag_msg_bat = 0;
        public WarningPage()
        {
            InitializeComponent();
            //ThreadWarnings();
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

        //por ahora las alertas de cinturon y puertas no van 
        /*
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

        */
        void SwitchBattery(object sender, ToggledEventArgs e)
        {
            if (flag_battery == 0)
            {
                MensajeActivado("Bateria");
                flag_battery = 1;
                
                //Emulador de disminucion de bateria
                //BatteryEmulator();
            }
            else
            {
                MensajeDesactivado("Bateria");
                flag_battery = 0;
            }
            flag_msg_bat = 1;
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
            flag_msg_near = 1;
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
            flag_msg_heart = 1;
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

                //descomentar esto
                
                if (battery != null)
                {
                    if (battery.value < min_battery && flag_msg_bat==1)
                    {
                        await DisplayAlert("Alerta", "Bateria Baja", "Ok");
                        flag_msg_bat = 0;
                    }
                }
                

                //comentar esto
                /*
                if (batery > 0 && flag_battery==1 && flag_msg_bat==1)
                {
                    if (batery < min_battery)
                    {
                        await DisplayAlert("Alerta", "Bateria Baja", "Ok");
                        flag_msg_bat = 0;
                    }
                }
                */
                if(pulse != null)
                {
                    if (pulse.value < min_heart && flag_msg_heart == 1)
                    {
                        await DisplayAlert("Alerta", "Pulso Bajo", "Ok");
                        flag_msg_heart = 0;
                    }
                }

                if(proximity!=null)
                {
                    if (proximity.value < min_near && flag_msg_near == 1)
                    {
                        await DisplayAlert("Alerta", "Cuidado, Objeto cerca al vehiculo", "Ok");
                        flag_msg_near = 0;
                    }
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