using DP2_Auto_App.Models.RestServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DP2_Auto_App.Contents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoalsPage : ContentPage
    {
        Objective objetivo;
        public GoalsPage()
        {
            InitializeComponent();
            initializeValues();
        }
        private void initializeValues()
        {
            loadGoals();
        }

        private async void loadGoals()
        {
            string prueba;
            prueba = await webService.rest.listGoals();
        }

        private void buttonClickedNewGoals(object sender, EventArgs e)
        {
            App.Current.MainPage = new Contents.GoalsNew();
        }

        private async void Button_ClickedCreate(object sender, EventArgs e)
        {
            int sensor, value;
            sensor = Int32.Parse(entry_Sensor.Text);
            value = Int32.Parse(entry_Value.Text);
            //objetivo = webService.rest.createGoal(sensor, value, entry_Start.Text, entry_End.Text, entry_Desc.Text);
            objetivo = await webService.rest.storeGoals(sensor, value, entry_Start.Text, entry_End.Text, entry_Desc.Text);
            await DisplayAlert("Actualizacion", "Actualizado", "Ok");
        }
    }
}