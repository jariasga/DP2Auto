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
        private NavigationPage [] pages;
        public MainMenu()
        {
            InitializeComponent();
            initizalizePages();
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

            if (item.Title.Equals("Usuario")) Detail = pages[0];
            else if (item.Title.Equals("Objetivos y logros")) Detail = pages[1];
            else if (item.Title.Equals("Estacionamiento")) Detail = pages[2];
            else if (item.Title.Equals("Navegación y Viajes")) Detail = pages[3];
            else if (item.Title.Equals("Recordatorios")) Detail = pages[5];
            else if (item.Title.Equals("Estado del Auto")) Detail = pages[6];
            else if (item.Title.Equals("Seguridad")) Detail = pages[7];
            else if (item.Title.Equals("Bluetooth")) Detail = pages[8];
            else if (item.Title.Equals("Alertas")) Detail = pages[9];
            else if (item.Title.Equals("Cerrar Sesión"))
            {
                DP2_Auto_App.Models.RestServices.RestService.logout();
                App.Current.MainPage = new Contents.Login();
            }
            else Detail = new NavigationPage(page);

            Detail.Title = item.Title;

            IsPresented = false;
            MasterPage.ListView.SelectedItem = null;
        }

        private void initizalizePages()
        {
            pages = new NavigationPage[10];
            pages[0] = new NavigationPage(new Contents.UserPage());
            pages[1] = new NavigationPage(new Contents.GoalsPage());
            pages[2] = new NavigationPage(new Contents.Parking());
            pages[3] = new NavigationPage(new Contents.MapTabbedPage());
            pages[5] = new NavigationPage(new Contents.ReminderPage());
            pages[6] = new NavigationPage(new Contents.SensorPage());
            pages[7] = new NavigationPage(new Contents.SecurityPage());
            pages[8] = new NavigationPage(new Bluetooth());
            pages[9] = new NavigationPage(new Contents.WarningPage());
        }
    }
}