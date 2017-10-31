using DP2_Auto_App.Models;
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
        User user;
        public Login()
        {
            InitializeComponent();
        }

        private void button_SignIn_Clicked(object sender, EventArgs e)
        {
            user = new User(label_Username.Text, label_Password.Text);
            if (authenticate())
            {
                DisplayAlert("Login", "Correcto", "Ok");
                App.Current.MainPage = new Contents.MainMenu();
            }
            else DisplayAlert("Error", "Usuario incorrecto", "Ok");
        }

        private bool authenticate()
        {
            // Deberá leer los datos de una BD y comparar password encriptados
            if (user.Username.Equals(".") && user.Password.Equals(".")) return true;
            else return false;
        }
    }
}