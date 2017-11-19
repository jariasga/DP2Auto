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
    public partial class Parking : ContentPage
    {
        public Parking()
        {
            var flag = true;
            InitializeComponent();
            ChangeData();
        }
        
        private void ChangeData()
        {
            
            btn_Restar.Clicked += Btn_Restar_Cliked;
            btn_Sumar.Clicked += Btn_Sumar_Cliked;
            sw.Toggled += Sw_Toggled;
        }

        private void Btn_Sumar_Cliked(object sender, EventArgs e)
        {
            if (label_Angulo.Text != "90")
            {
                int angulo;
                var an = label_Angulo.Text;
                Int32.TryParse(an, out angulo);
                angulo += 5;
                label_Angulo.Text = angulo.ToString();
            }
        }

        private void Btn_Restar_Cliked(object sender, EventArgs e)
        {
            if (label_Angulo.Text != "0")
            {
                int angulo;
                var an = label_Angulo.Text;
                Int32.TryParse(an, out angulo);
                angulo -= 5;
                label_Angulo.Text = angulo.ToString(); 
            }
        }
        
        private void Sw_Toggled(object sender, ToggledEventArgs e)
        {
            var value = e.Value.ToString();
            if (value == "True")
            {
                DisplayAlert("Atencion", "Para poder manipular el angulo debe desactivar el modo automático", "OK");
            }
        }
    }
}