using DP2_Auto_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace DP2_Auto_App
{

    class BluetoothViewModel
    {
        public List<string> ListOfDevices { get; set; } = new List<string>();
        public string SelectedBthDevice { get; set; }
        public string Message { get; set; }

        public void GetPairedDevices()
        {
            try
            {
                ListOfDevices = DependencyService.Get<IBth>().PairedDevices();
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Attention", ex.Message, "Ok");
            }

        }

        public void Connect()
        {
            try
            {
                DependencyService.Get<IBth>().Connect(SelectedBthDevice);
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Attention", ex.Message, "Ok");
            }
        }

        public void Send()
        {
            try
            {
                DependencyService.Get<IBth>().Send(Message);
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Attention", ex.Message, "Ok");
            }
        }

        public void Disconnect()
        {
            try
            {
                DependencyService.Get<IBth>().Disconnect();
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Attention", ex.Message, "Ok");
            }
        }
    }
}
