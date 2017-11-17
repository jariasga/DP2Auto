﻿using System;
using System.Collections.Generic;
using DP2_Auto_App.Models.RestServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace DP2_Auto_App.Contents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        startTravel inicio;
        endTravel fin;
        webService web;
        public MapPage()
        {
            InitializeComponent();
            initializeMap();
        }

        async void mensaje()
        {
            await DisplayAlert("funciona","a", "ok");
        }

        private void map_slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var zoomLevel = e.NewValue; // between 1 and 18
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            map.MoveToRegion(new MapSpan(map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
        }

        private void initializeMap()
        {
            var position = new Position(-12.0689857, -77.078947); // Latitude, Longitude
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "custom pin",
                Address = "custom detail info"
            };
            map.Pins.Add(pin);

            map.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                new Position(-12.0689857, -77.078947), Distance.FromMiles(0.5)));


            //map.MapType == MapType.Street;
            /*
            var stack = new StackLayout { Spacing = 3  };
            stack.Children.Add(map);
            Content = stack;*/
        }

        void Nuevo_destino(object sender, ToggledEventArgs e)
        {
            var pos = new Position(double.Parse(latitude.Text), double.Parse(longitude.Text));
            string add = "" + latitude.Text + " " + longitude.Text;
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = pos,
                Label = "destination pin",
                Address = add,
            };
            map.Pins.Add(pin);
        }

        private async void StartTravel(object sender, EventArgs e)
        {
            web = new webService();
            await webService.rest.startTravel("17:19:43:F0:20:B8");
            inicio = RestService.currentTravel;
            DateTime horaIni = DateTime.Parse(inicio.started_at.date);
            //await DisplayAlert("Viaje", "el viaje comenzó : " + horaIni.ToString("HH::mm::ss"), "Ok");
        }
        private async void EndTravel(object sender, EventArgs e)
        {
            web = new webService();
            inicio = RestService.currentTravel;
            await webService.rest.endTravel(inicio);
            //Travel auxTravel = RestService.getLastTrip();
            //DateTime horaIni = auxTravel.started;
            fin = RestService.end;
            DateTime horaIni = DateTime.Parse( fin.ended_at.date);
            DateTime horaFin = DateTime.Parse(fin.started_at);
            await DisplayAlert("Resumen del viaje", "Inicio: " + horaIni.ToString("HH::mm::ss") + "/\n Fin: " + horaFin.ToString("HH::mm::ss"), "Ok");
        }


    }
}