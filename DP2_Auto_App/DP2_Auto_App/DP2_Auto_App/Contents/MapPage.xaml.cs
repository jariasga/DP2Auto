using System;
using System.Collections.Generic;
using DP2_Auto_App.Models.RestServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using DP2_Auto_App.Models;



namespace DP2_Auto_App.Contents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {

        startTravel inicio;
        endTravel fin;
        double longInicial, longFinal, latInicial, latFinal, recorrido;
        double actualLong, actualLat;

        public MapPage()
        {
            InitializeComponent();
            initializeMap();
            longInicial = longFinal = latInicial = latFinal = recorrido = 0.0;
            actualLong = actualLat = 0.0;
            sendPosition();
        }


        public async void sendPosition()
        {
                while (true)
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;

                    if (locator.IsGeolocationAvailable && locator.IsGeolocationEnabled)
                    {

                        var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(100));
                        if (position == null)
                            return;


                        actualLat = position.Latitude;
                        actualLong = position.Longitude;
                        //await webService.rest.storePosition(BTMessages.macBT, actualLat, actualLong); // si hay modulo bt
                        await webService.rest.storePosition("00:21:13:01:D6:BB", actualLat, actualLong);
                    }
                    else
                    {
                        await DisplayAlert("Error", "Home", "OK");
                    }
                    await Task.Delay(1000);
                }
            
        }



        public async void getlocacion(object sender, EventArgs e)
        {
            await RetreiveLoc();
        }

        public async Task RetreiveLoc()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            if (locator.IsGeolocationAvailable && locator.IsGeolocationEnabled)
            {

                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(100));
                if (position == null)
                    return;


                latitude.Text = "" + position.Latitude;
                longitude.Text = "" + position.Longitude;
            }
            else
            {
                await DisplayAlert("Error", "Home", "OK");
            }

        }


        async void mensaje()
        {
            await DisplayAlert("funciona", "a", "ok");
        }

        private void map_slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var zoomLevel = e.NewValue; // between 1 and 18
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            map.MoveToRegion(new MapSpan(map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
        }

        private void initializeMap()
        {
            var position = new Xamarin.Forms.Maps.Position(-12.0689857, -77.078947); // Latitude, Longitude
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
                new Xamarin.Forms.Maps.Position(-12.0689857, -77.078947), Distance.FromMiles(0.5)));

        }

        void Nuevo_destino(object sender, ToggledEventArgs e)
        {
            var pos = new Xamarin.Forms.Maps.Position(double.Parse(latitude.Text), double.Parse(longitude.Text));
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
            if (BTMessages.macBT != "")
            {
                await webService.rest.startTravel("00:21:13:01:D6:BB");   // En caso no tener BT real
                //await webService.rest.startTravel(BTMessages.macBT);
                inicio = RestService.currentTravel;
                DateTime horaIni = DateTime.Parse(inicio.started_at.date);
                await DisplayAlert("Viaje", "el viaje comenzó a las " + horaIni.ToString("HH:mm:ss"), "Ok");
                await RetreiveLoc();
                longInicial = double.Parse(longitude.Text);
                latInicial = double.Parse(latitude.Text);
                button_start.IsEnabled = false;
                button_end.IsEnabled = true;
            }
            else await DisplayAlert("Atención", "Para iniciar un viaje debe conectarse a un vehiculo", "Ok");
        }
        private async void EndTravel(object sender, EventArgs e)
        {
            inicio = RestService.currentTravel;

            //recorrido
            longFinal = double.Parse(longitude.Text);
            latFinal = double.Parse(latitude.Text);

            recorrido = hallaRecorrido();
            await DisplayAlert("Recorrido", "" + recorrido + " Km", "Ok");

            await webService.rest.endTravel(inicio);
            //Travel auxTravel = RestService.getLastTrip();
            //DateTime horaIni = auxTravel.started;
            fin = RestService.end;
            DateTime horaFin = DateTime.Parse(fin.ended_at.date);
            DateTime horaIni = DateTime.Parse(fin.started_at);
            await DisplayAlert("Resumen del viaje", "Inicio: " + horaIni.ToString("HH:mm:ss") + "\n Fin: " + horaFin.ToString("HH::mm::ss"), "Ok");

            button_start.IsEnabled = true;
            button_end.IsEnabled = false;

            //descomentar esto si se quiere posiciones reales dl gps
            //await RetreiveLoc();

            
        }

        private double hallaRecorrido()
        {
            var R = 6371e3; // metres

            var φ1 = latInicial * Math.PI / 180;
            var φ2 = latFinal * Math.PI / 180;
            var Δφ = (latFinal - latInicial) * Math.PI / 180;
            var Δλ = (longFinal - longInicial) * Math.PI / 180;

            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                    Math.Cos(φ1) * Math.Cos(φ2) *
                    Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var d = R * c;

            return d;

        }
    }
}