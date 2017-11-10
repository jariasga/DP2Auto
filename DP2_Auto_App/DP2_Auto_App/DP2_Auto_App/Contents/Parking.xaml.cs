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
		public Parking ()
		{
			InitializeComponent ();
            /*
            btn_angulo.Clicked += (sender, e) =>
            {
                DisplayAlert("Mensaje", "Angulo", "OK");
            };
            */

            // Leer estado del estacionamiento
            // Test
            /*
            var label = new Label
            {
                Text = "Escribe el Angulo de Inclinación"
            };
            var txtNombre = new Entry
            {
                
                Placeholder = "Angulo"
            };
            var btnPrimerBoton = new Button
            {
                Text = "Mover Persiana!"
            };
            btnPrimerBoton.Clicked += (sender, e) =>
            {
                DisplayAlert("Mensaje", txtNombre.Text, "OK");
            };
            Content = new StackLayout
            {
                Padding = 30,
                Spacing = 10,
                Children = { label, txtNombre, btnPrimerBoton }
            };*/

        }
    }
}