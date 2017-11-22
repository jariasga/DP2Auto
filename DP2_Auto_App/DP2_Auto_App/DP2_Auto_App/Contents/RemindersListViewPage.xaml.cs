using DP2_Auto_App.Models.RestServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Click", "Recordatorio seleccionado.", "OK");

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