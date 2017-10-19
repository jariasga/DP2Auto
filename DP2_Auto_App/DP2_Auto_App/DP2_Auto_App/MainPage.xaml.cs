using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DP2_Auto_App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void toLogin(object sender, EventArgs e)
        {
            App.Current.MainPage = new Contents.Login();            
        }
    }
}
