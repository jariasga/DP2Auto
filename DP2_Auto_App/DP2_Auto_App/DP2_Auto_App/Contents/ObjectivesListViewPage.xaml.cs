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
    public partial class ObjectivesListViewPage : ContentPage
    {
        private List<Objective> objectives { get; set; }

        public ObjectivesListViewPage()
        {
            InitializeComponent();
            initializeValues();
        }

        private async void initializeValues()
        {
            objectives = await webService.rest.listGoals();
            MyListView.ItemsSource = objectives;
        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Click", "Objetivo seleccionado.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
