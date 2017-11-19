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
	public partial class GoalsNew : ContentPage
	{
		public GoalsNew ()
		{
			InitializeComponent ();
		}


        private void Button_ClickedCreate(object sender, EventArgs e)
        {
            int sensor, value;
            sensor = Int32.Parse(entry_Sensor.Text);
            value = Int32.Parse(entry_Value.Text);
            //webService.rest.createGoal(sensor, value, entry_Start.Text, entry_End.Text, entry_Desc.Text);
            //webService.rest.storeGoals(sensor, value, entry_Start.Text, entry_End.Text, entry_Desc.Text);
            DisplayAlert("Actualizacion", "Actualizado", "Ok");
        }
    }
}