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
    public partial class SensorPage : ContentPage
    {
        RestService rest;
        public SensorPage()
        {
            InitializeComponent();
        }
        private void button_Actualizar(object sender, EventArgs e)
        {
            refreshSensors();
        }

        private async void refreshSensors()
        {

        }
    }
}