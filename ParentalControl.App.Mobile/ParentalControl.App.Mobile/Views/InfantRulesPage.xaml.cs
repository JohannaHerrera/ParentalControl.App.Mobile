using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParentalControl.App.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfantRulesPage : ContentPage
    {
        public InfantRulesPage(int infantId)
        {
            InitializeComponent();
            LoadInformation(infantId);
        }

        public void LoadInformation(int infantId)
        {
            
        }

        private void Back_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new InfantAccountPage());
        }

        private void Home_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new HomePage());
        }

        private void InfantAccounts_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new InfantAccountPage());
        }

        private void Device_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new DevicePage());
        }

        private void Schedules_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new SchedulePage());
        }

        private void Notifications_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new NotificationsPage());
        }

        private void MyAccount_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new MyProfilePage());
        }

        private void Logout_Clicked(object sender, EventArgs a)
        {
            Xamarin.Essentials.Preferences.Clear();
            _ = DisplayAlert("Aviso", "Cerrando sesión...", "OK");
            _ = Navigation.PushAsync(new LoginPage());
        }
    }
}