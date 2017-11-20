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
        webService web;
        public Login()
        {
            InitializeComponent();
        }

        private void button_SignIn_Clicked(object sender, EventArgs e)
        {
            button_SignIn.IsEnabled = false;
            authenticateAsync();
        }

        private async void authenticateAsync()
        {
            web = new webService();
            Users user = new Users
            {
                email = label_Username.Text,
                password = label_Password.Text
            };

            string saber = await webService.rest.getLoginToken(user);

            if (saber.Equals("loginSuccess"))
            {
                await DisplayAlert("Login", "Correcto", "Ok");
                await webService.rest.getClientInfo();
                App.Current.MainPage = new Contents.MainMenu();
            }
            else if (saber.Equals("connectionProblem")) await DisplayAlert("Error", "Verifique su conexión !", "Ok");
            else if (saber.Equals("Unauthorized")) await DisplayAlert("Error", "Usuario Bloqueado !", "Ok");
            else await DisplayAlert("Error", "Usuario incorrecto", "Ok");
            button_SignIn.IsEnabled = true;
        }
    }
}