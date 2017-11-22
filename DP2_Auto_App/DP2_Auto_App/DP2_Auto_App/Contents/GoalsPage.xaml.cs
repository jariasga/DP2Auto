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
        List<Objective> objectives;
        GoalsAchieved achieved;
        public GoalsPage()
        {
            InitializeComponent();
            initializeValues();            
        }
        private async void initializeValues()
        {
            objectives = await webService.rest.listGoals();
            achieved = new GoalsAchieved();
        }

        private async void buttonClickedAchievedGoals(object sender, EventArgs e)
        {               
            await Navigation.PushModalAsync(achieved);
        }
    }
}