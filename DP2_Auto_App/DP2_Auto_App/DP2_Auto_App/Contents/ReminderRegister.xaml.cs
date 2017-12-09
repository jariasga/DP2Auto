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
            hour = hora.Time.ToString();

            recordatorio = await webService.rest.storeReminder(entry_Desc.Text, date, hour);
            await DisplayAlert("Actualizacion", "Actualizado", "Ok");
        }
    }
}