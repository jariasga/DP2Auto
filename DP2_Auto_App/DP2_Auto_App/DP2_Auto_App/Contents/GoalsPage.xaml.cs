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
    public partial class GoalsPage : ContentPage
    {
        public GoalsPage()
        {
            InitializeComponent();
        }

        private void buttonClickedAchievedGoals(object sender, EventArgs e)
        {
            DisplayAlert("Login", "Correcto", "Ok");
            App.Current.MainPage = new Contents.GoalsAchieved();
        }

        private void buttonClickedActualGoals(object sender, EventArgs e)
        {
            DisplayAlert("Login", "Correcto", "Ok");
            App.Current.MainPage = new Contents.GoalsActual();
        }

        private void buttonClickedNewGoals(object sender, EventArgs e)
        {
            DisplayAlert("Login", "Correcto", "Ok");
            App.Current.MainPage = new Contents.GoalsNew();
        }
    }
}