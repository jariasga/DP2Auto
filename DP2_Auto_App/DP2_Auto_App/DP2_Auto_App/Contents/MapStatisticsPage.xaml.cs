using System;
using System.Collections.Generic;
using DP2_Auto_App.Models.RestServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DP2_Auto_App.Contents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapStatisticsPage : ContentPage
    {
        public MapStatisticsPage()
        {
            InitializeComponent();
            startPicker();
            
        }

        void startPicker()
        {
            Travel aux = new Travel();
            int cantidad = RestService.CountTravels();
            if(cantidad != 0)
            {
                for(int i = 0; i < cantidad; i++)
                {
                    aux = RestService.getTravelAt(i);
                    pickerRecorridos.Items.Add("" + aux.started.id);
                }
            }
                
        }

        void actualizaPicker()
        {
            Travel aux = new Travel();
            int cantidad = RestService.CountTravels();
            //pickerRecorridos.Items.Clear();
            //for(int i= 0;i<cantidad;i++)

             aux = RestService.getTravelAt(cantidad-1);
             pickerRecorridos.Items.Add( ""+aux.started.id);

            pickerRecorridos.SelectedIndex = 0;
        }

        void SeleccionaViaje()
        {
            int id_viaje = int.Parse(pickerRecorridos.SelectedItem.ToString());
            //buscar en la lista de viajes el id
            Travel aux = RestService.getNTravel(id_viaje);
            if(aux != null)
            {
                recorrido_viaje.Detail = "10 m.";
                DateTime inicio = DateTime.Parse(aux.ended.started_at);
                DateTime fin = DateTime.Parse(aux.ended.ended_at.date);
                TimeSpan difference = fin.TimeOfDay - inicio.TimeOfDay;
                //fin tiene las horas totales
                tiempo_viaje.Detail = "" + difference.ToString();
            }
        }
    }
}