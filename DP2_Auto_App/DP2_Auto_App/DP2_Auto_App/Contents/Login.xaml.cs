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
    public partial class Login : ContentPage
    {
        RestService rest;
        public Login()
        {
            InitializeComponent();
        }

        private void button_SignIn_Clicked(object sender, EventArgs e)
        {
            authenticateAsync();
        }

        private async void authenticateAsync()
        {
            rest = new RestService();
            Users user = new Users();
            user.email = label_Username.Text;
            user.password = label_Password.Text;

            string saber = await rest.createUserData(user);

            Debug.WriteLine(saber);

            if (saber.Contains("token"))
            {
                await DisplayAlert("Login", "Correcto", "Ok");
                App.Current.MainPage = new Contents.MainMenu();
            }
            else await DisplayAlert("Error", "Usuario incorrecto", "Ok");
        }
    }
}