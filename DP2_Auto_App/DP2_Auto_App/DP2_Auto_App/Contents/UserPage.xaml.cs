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
    public partial class UserPage : ContentPage
    {
        public UserPage()
        {
            //InitializeComponent();
            initializeValues();
        }
        private void initializeValues()
        {
            entry_Name.Text = RestService.client.name;
            entry_LastName.Text = RestService.client.lastname;

            entry_Email.Text = RestService.client.email;
            entry_Phone.Text = RestService.client.phone;
            label_Rating.Text = RestService.client.rating.ToString();
            label_Created.Text = RestService.client.created_at;
            label_Updated.Text = RestService.client.updated_at;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DP2_Auto_App.Models.RestServices.webService.rest.updateClient(entry_Name.Text, entry_LastName.Text, entry_Phone.Text, entry_Email.Text);
            DisplayAlert("Actualizacion", "Actualizado", "Ok");
        }
    }
}