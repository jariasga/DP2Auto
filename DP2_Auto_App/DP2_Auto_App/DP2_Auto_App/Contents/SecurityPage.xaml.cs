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
    public partial class SecurityPage : ContentPage
    {
        public SecurityPage()
        {
            InitializeComponent();
        }

        private void buttonClickedChangePsw(object sender, EventArgs e)
        {
            //App.Current.MainPage = new Contents.GoalsAchieved();
        }
        private void buttonClickedFingerprint(object sender, EventArgs e)
        {
            //App.Current.MainPage = new Contents.GoalsAchieved();
        }
        private void buttonClickedPattern(object sender, EventArgs e)
        {
            //App.Current.MainPage = new Contents.GoalsAchieved();
        }
    }
}