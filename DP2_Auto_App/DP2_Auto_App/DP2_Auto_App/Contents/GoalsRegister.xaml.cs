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
    public partial class GoalsRegister : ContentPage
    {
        Objective objetivo;
        public GoalsRegister()
        {
            InitializeComponent();
        }

        private async void Button_ClickedCreate(object sender, EventArgs e)
        {
            int sensor, value;
            string sensorText, startDate, auxIni;

            sensor = 0;
            value = Int32.Parse(entry_Value.Text);
            sensorText = pickerSensors.SelectedItem.ToString();
            if (sensorText == "Peso") sensor = 1;
            else if (sensorText == "Ritmo Cardiaco") sensor = 2;
            else if (sensorText == "Proximidad") sensor = 3;
            else if (sensorText == "Temperatura") sensor = 4;
            else if (sensorText == "Velocidad") sensor = 5;
            else if (sensorText == "Humedad") sensor = 7;

            auxIni = fechaInicio.Date.ToString("dd/MM/yyyy");
            startDate = auxIni.Substring(0, auxIni.IndexOf(' '));

            //objetivo = webService.rest.createGoal(sensor, value, entry_Start.Text, entry_End.Text, entry_Desc.Text);
            objetivo = await webService.rest.storeGoals(sensor, value, startDate, entry_End.Text, entry_Desc.Text);
            await DisplayAlert("Actualizacion", "Actualizado", "Ok");
        }
    }
}