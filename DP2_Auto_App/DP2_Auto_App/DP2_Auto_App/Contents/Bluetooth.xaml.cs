using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE;
using System.Collections.ObjectModel;
using Plugin.BLE.Abstractions.Exceptions;
using System.Diagnostics;
using DP2_Auto_App.Models;

namespace DP2_Auto_App.Contents
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Bluetooth : ContentPage
	{
        public List<string> ListOfDevices { get; set; } = new List<string>();
        public string SelectedBTDevice { get; set; }
        public string Message { get; set; }

        public void GetPairedDevices()
        {
            lv.ItemsSource = ListOfDevices;
            try
            {
                ListOfDevices = DependencyService.Get<IBT>().PairedDevices();
            }
            catch (Exception e)
            {
                DisplayAlert("Atención", e.Message, "OK");
            }
        }

        public void Connect()
        {
            try
            {
                DependencyService.Get<IBT>().Connect(SelectedBTDevice);
            }
            catch (Exception e)
            {
                DisplayAlert("Atención", e.Message, "OK");
            }
        }

        public void Send()
        {
            try
            {
                DependencyService.Get<IBT>().Send(Message);
            }
            catch (Exception e)
            {
                DisplayAlert("Atención", e.Message, "OK");
            }
        }
        public void Disconnect()
        {
            try
            {
                DependencyService.Get<IBT>().Disconnect();
            }
            catch (Exception e)
            {
                DisplayAlert("Atención", e.Message, "OK");
            }
        }

        private void btnConnect_Clicked(object sender, EventArgs e)
        {
            Connect();
        }

        private void btnListDevices_Clicked(object sender, EventArgs e)
        {
            GetPairedDevices();
        }

        private void btnDisconnect_Clicked(object sender, EventArgs e)
        {
            Disconnect();
        }
    }
}