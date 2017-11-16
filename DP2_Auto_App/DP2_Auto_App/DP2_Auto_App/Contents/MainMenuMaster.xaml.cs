using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DP2_Auto_App.Contents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuMaster : ContentPage
    {
        public ListView ListView;

        public MainMenuMaster()
        {
            InitializeComponent();

            BindingContext = new MainMenuMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MainMenuMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainMenuMenuItem> MenuItems { get; set; }
            
            public MainMenuMasterViewModel()
            {
                MenuItems = new ObservableCollection<MainMenuMenuItem>(new[]
                {
                    new MainMenuMenuItem { Id = 0, Title = "Usuario" },
                    new MainMenuMenuItem { Id = 1, Title = "Objetivos y logros" },
                    new MainMenuMenuItem { Id = 2, Title = "Estacionamiento" },
                    new MainMenuMenuItem { Id = 3, Title = "Navegación y Viajes" },
                    new MainMenuMenuItem { Id = 4, Title = "Recordatorios" },
                    new MainMenuMenuItem { Id = 5, Title = "Estado del Auto" },
                    new MainMenuMenuItem { Id = 6, Title = "Seguridad" },
                    new MainMenuMenuItem { Id = 8, Title = "Alertas"},
                    new MainMenuMenuItem { Id = 7, Title = "Bluetooth" },
                    new MainMenuMenuItem { Id = 7, Title = "Cerrar Sesión" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}