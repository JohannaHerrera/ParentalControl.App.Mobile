using ParentalControl.App.Mobile.Models;
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
    public partial class MyProfilePage : ContentPage
    {
        public MyProfilePage()
        {
            InitializeComponent();
            LoadInformation();
        }

        private async void LoadInformation()
        {
            int parentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));
            var response = await new MyProfileService().GetMyProfile(parentId);

            if (response != null)
            {
                if (!string.IsNullOrEmpty(response.MessageError))
                {
                    _ = DisplayAlert("Error", response.MessageError, "OK");
                    _ = Navigation.PushAsync(new HomePage());
                }
                else
                {
                    txtUsername.Text = response.ParentUsername;
                    txtUsername.IsEnabled = false;
                    txtEmail.Text = response.ParentEmail;
                    txtEmail.IsEnabled = false;
                    txtPassword.Text = response.ParentPassword;
                    txtPassword.IsEnabled = false;
                }
            }
            else
            {
                _ = DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
                _ = Navigation.PushAsync(new HomePage());
            }
        }

        private async void Edit_Clicked(object sender, EventArgs a)
        {
            if (bntEdit.Text == "Editar")
            {
                txtUsername.IsEnabled = true;
                txtEmail.IsEnabled = true;
                txtPassword.IsEnabled = true;

                bntEdit.Text = "Guardar";
            }
            else
            {
                MyProfileModel myProfileModel = new MyProfileModel();
                myProfileModel.ParentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));
                myProfileModel.ParentUsername = txtUsername.Text;
                myProfileModel.ParentEmail = txtEmail.Text;
                myProfileModel.ParentPassword = txtPassword.Text;
                var response = await new MyProfileService().UpdateMyProfile(myProfileModel);

                if (response)
                {
                    _ = DisplayAlert("Aviso", "La información se actualizó correctamente.", "OK");
                    _ = Navigation.PushAsync(new MyProfilePage());
                }
                else
                {
                    _ = DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
                    _ = Navigation.PushAsync(new HomePage());
                }
            }
            
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