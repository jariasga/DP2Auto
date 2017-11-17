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
            //InitializeComponent();
            initializeValues();
        }
        private void initializeValues()
        {
            /*label_desc1.Text
            entry_Name.Text = RestService.client.name;
            entry_LastName.Text = RestService.client.lastname;

            entry_Email.Text = RestService.client.email;
            entry_Phone.Text = RestService.client.phone;
            entry_Rating.Text = RestService.client.rating.ToString();
            entry_Created.Text = RestService.client.created_at;
            entry_Updated.Text = RestService.client.updated_at;*/
        }

        private void buttonClickedAchievedGoals(object sender, EventArgs e)
        {
            App.Current.MainPage = new Contents.GoalsAchieved();
        }

        private void buttonClickedNewGoals(object sender, EventArgs e)
        {
            App.Current.MainPage = new Contents.GoalsNew();
        }
    }
}