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
    public partial class MainMenu : MasterDetailPage
    {
        public MainMenu()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainMenuMenuItem;
            if (item == null)
                return;
            // Not to use
            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;
            // --

            if (item.Title.Equals("Usuario")) Detail = new NavigationPage(page);
            else if (item.Title.Equals("Objetivos y logros")) Detail = new NavigationPage(page);
            else if (item.Title.Equals("Estacionamiento")) Detail = new NavigationPage(new Contents.Parking());
            else if (item.Title.Equals("Navegación y Viajes")) Detail = new NavigationPage(new Contents.MapTabbedPage());
            else if (item.Title.Equals("Recordatorios")) Detail = new NavigationPage(new Contents.ReminderPage());
            else if (item.Title.Equals("Estado del Auto")) Detail = new NavigationPage(new Contents.SensorPage());
            else if (item.Title.Equals("Seguridad")) Detail = new NavigationPage(new Contents.SecurityPage());
            else if (item.Title.Equals("Bluetooth")) Detail = new NavigationPage(new Contents.Bluetooth());
            else Detail = new NavigationPage(page);

            Detail.Title = item.Title;

            IsPresented = false;
            MasterPage.ListView.SelectedItem = null;
        }
    }
}