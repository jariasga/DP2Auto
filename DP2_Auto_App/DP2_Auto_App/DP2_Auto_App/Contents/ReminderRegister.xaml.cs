using DP2_Auto_App.Models.RestServices;
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
    public partial class ReminderRegister : ContentPage
    {
        Reminder recordatorio;
        public ReminderRegister()
        {
            InitializeComponent();
        }
        private async void Button_ClickedCreate(object sender, EventArgs e)
        {
            string date, hour;
            date = fecha.Date.ToString("dd/MM/yyyy");
            hour = convertToFormat(hora.Time.ToString());
            
            recordatorio = await webService.rest.storeReminder(entry_Desc.Text, date, hour);
            await DisplayAlert("Actualizacion", "Actualizado", "Ok");
        }

        private string convertToFormat(string hora)
        {
            int h, m, s;
            string tiempo, cadena;
            string[] tokens = hora.Split(':');
            Int32.TryParse(tokens[0], out h);
            Int32.TryParse(tokens[1], out m);
            Int32.TryParse(tokens[2], out s);

            if (h > 12)
            {
                h = h - 12;
                tiempo = "pm";
            }
            else tiempo = "am";
            cadena = h.ToString("00") + ':' + tokens[1] + ' ' + tiempo;
            return cadena;
        }
    }
}