using DP2_Auto_App.Models.RestServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DP2_Auto_App.Contents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RemindersListViewPage : ContentPage
    {
        private List<Reminder> reminders { get; set; }
        public static bool actualLoop;
        bool[] remindersParsed;
        public RemindersListViewPage()
        {
            InitializeComponent();
            actualLoop = true;
            initializeValues();
        }

        private async void initializeValues()
        {
            remindersParsed = new bool[1000];
            for (int i = 0; i < remindersParsed.Count(); i++)
                remindersParsed[i] = false;
            while (RestService.client != null && actualLoop)
            {
                reminders = await webService.rest.listReminders();
                MyListView.ItemsSource = reminders;
                //MyListView.IsPullToRefreshEnabled = true;
                
                await Task.Delay(1000);
                comprobarRecordatorio();
            }
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            Reminder rec = MyListView.SelectedItem as Reminder;
            await DisplayAlert("Detalle", string.Concat(rec.end_date, " -> ", rec.end_time), "OK");
            //await DisplayAlert("Click", "Recordatorio seleccionado.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;

            //alertRemind();



        }

        private async void comprobarRecordatorio()
        {
            string dia, hora, identificador, aux;
            dia = DateTime.Now.ToString("dd/MM/yyyy");
            identificador = DateTime.Now.ToString("tt", CultureInfo.InvariantCulture);
            aux = identificador.ToLower();
            hora = string.Concat(DateTime.Now.ToString("hh:mm "), aux);
            int contador = reminders.Count()-1;
            
            for (int i = 0; i <= contador; i++)
            {
                if (reminders[i].end_date == dia && !remindersParsed[i])
                {
                    remindersParsed[i] = true;
                    await DisplayAlert("Atención!", string.Concat("Hoy: ", reminders[i].description), "OK");                    
                }
                else if (reminders[i].end_date == dia && reminders[i].end_time == hora && !remindersParsed[i])
                {
                    remindersParsed[i] = true;
                    await DisplayAlert("Atención!", string.Concat("Ahora: ", reminders[i].description), "OK");                    
                }
            }
        }

        private void button_Remember_Clicked(object sender, EventArgs e)
        {
            //alertRemind();
        }

        /*private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }*/
    }
}