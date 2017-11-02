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
    public partial class Login : ContentPage
    {
        RestService rest;
        public Login()
        {
            InitializeComponent();
        }

        private void button_SignIn_Clicked(object sender, EventArgs e)
        {
            /*rest = new RestService();
            Users user = new Users();
            user.email = label_Username.Text;
            user.password = label_Password.Text;

            rest.createUserData(user, false);
            if (authenticate())
            {
                */DisplayAlert("Login", "Correcto", "Ok");
                App.Current.MainPage = new Contents.MainMenu();
            /*}
            else DisplayAlert("Error", "Usuario incorrecto", "Ok");*/
        }

        private bool authenticate()
        {
            // Deberá leer los datos de una BD y comparar password encriptados
            //if (user.Username.Equals(".") && user.Password.Equals(".")) return true;
            /*else */return false;
        }
    }
}