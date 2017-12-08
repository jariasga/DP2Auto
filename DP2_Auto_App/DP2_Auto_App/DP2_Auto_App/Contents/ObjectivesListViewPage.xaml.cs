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
        public static bool  actualLoop;
        public static bool unico;

        public ObjectivesListViewPage()
        {
            InitializeComponent();
            actualLoop = true;
            unico = false;
            initializeValues();

        }

        private async void initializeValues()
        {
            while (actualLoop)
            {
                objectives = await webService.rest.listGoals();
                MyListView.ItemsSource = objectives;
                MyListView.IsPullToRefreshEnabled = true;

                await Task.Delay(8000);
                //comprobarLogro();
            }

        }

        public async void comprobarLogro()
        {
            objectives = await webService.rest.listAchievedGoals();
            int count = objectives.Count() - 1;
            for (int i = 0; i <= count; i++)
            {
                if (objectives[i].goal <= objectives[i].value && unico == false)
                {
                    await DisplayAlert("Felicitaciones!", string.Concat(objectives[i].description), " completado");
                    unico = true;
                }
            }
            //await Task.Delay(8000);



        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            Objective obj = MyListView.SelectedItem as Objective;
            //string objSelected = await webService.rest.getGoalInfo(goalID);
            await DisplayAlert("Avance", string.Concat(obj.value.ToString(),"/",obj.goal.ToString()), "OK");

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
