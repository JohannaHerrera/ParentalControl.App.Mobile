using ParentalControl.App.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParentalControl.App.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DevicePage : ContentPage
    {
        public DevicePage()
        {
            InitializeComponent();
            LoadInformation();
        }

        public async void LoadInformation()
        {
            var parentId = Preferences.Get("ParentId", "0");
            //var response = await new DeviceService().GetDeviceInfo(parentId);
        }

        private void Home_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new HomePage());
        }

        private void InfantAccounts_Clicked(object sender, EventArgs a)
        {

        }

        private void Device_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new DevicePage());
        }

        private void Schedules_Clicked(object sender, EventArgs a)
        {

        }

        private void Notifications_Clicked(object sender, EventArgs a)
        {

        }

        private void MyAccount_Clicked(object sender, EventArgs a)
        {

        }

        private void Logout_Clicked(object sender, EventArgs a)
        {

        }
    }
}