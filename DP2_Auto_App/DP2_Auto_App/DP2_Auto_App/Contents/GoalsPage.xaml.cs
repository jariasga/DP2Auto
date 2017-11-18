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

            //label_desc1.Text = RestService.
            /*entry_Name.Text = RestService.client.name;
            entry_LastName.Text = RestService.client.lastname;

            entry_Email.Text = RestService.client.email;
            entry_Phone.Text = RestService.client.phone;
            entry_Rating.Text = RestService.client.rating.ToString();
            entry_Created.Text = RestService.client.created_at;
            entry_Updated.Text = RestService.client.updated_at;*/
        }

        private void buttonClickedNewGoals(object sender, EventArgs e)
        {
            App.Current.MainPage = new Contents.GoalsNew();
        }

        private void Button_ClickedCreate(object sender, EventArgs e)
        {
            int sensor, value;
            sensor = Int32.Parse(entry_Sensor.Text);
            value = Int32.Parse(entry_Value.Text);
            webService.rest.createGoal(sensor, value, entry_Start.Text, entry_End.Text, entry_Desc.Text);
            objetivo = RestService.currentObjective;
            //webService.rest.storeGoals(sensor, value, entry_Start.Text, entry_End.Text, entry_Desc.Text);
            DisplayAlert("Actualizacion", "Actualizado", "Ok");
        }
    }
}