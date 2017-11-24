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
        public RemindersListViewPage()
        {
            InitializeComponent();
            initializeValues();
        }

        private async void initializeValues()
        {
            reminders = await webService.rest.listReminders();
            MyListView.ItemsSource = reminders;
            MyListView.IsPullToRefreshEnabled = true;

            /*string dia, hora, identificador, aux;
            dia = DateTime.Now.ToString("dd/MM/yyyy");
            identificador = DateTime.Now.ToString("tt", CultureInfo.InvariantCulture);
            aux = identificador.ToLower();
            hora = string.Concat(DateTime.Now.ToString("hh:mm "), aux);
            int contador = reminders.Count();
            for (int i = 1; i <= contador; i++)
            {
                if (reminders[i].end_date == dia)
                {
                    await DisplayAlert("Click", string.Concat("Hoy: ", reminders[i].description), "OK");
                    break;
                }
            }*/
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

        }

        /*private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }*/
    }
}