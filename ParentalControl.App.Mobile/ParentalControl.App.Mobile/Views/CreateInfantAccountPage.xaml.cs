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
    public partial class CreateInfantAccountPage : ContentPage
    {        
        public CreateInfantAccountPage()
        {
            InitializeComponent();
            LoadInformation();
        }

        public void LoadInformation()
        {
            List<string> genderList = new List<string>()
            {
                "Masculino",
                "Femenino"
            };

            //Cargo los géneros en el picker 
            foreach (var gender in genderList)
            {
               infantGender.Items.Add(gender);
            }
        }

        private async void CreateInfantAccount_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(infantName.Text))
            {
                _ = DisplayAlert("Error", "El nombre del infante no es válido.", "OK");
            }
            else if (infantGender.SelectedIndex == -1)
            {
                _ = DisplayAlert("Error", "Por Favor seleccione el género del infante.", "OK");
            }
            else
            {
                CreateInfantAccountModel createInfantAccountModel = new CreateInfantAccountModel();
                createInfantAccountModel.ParentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));
                createInfantAccountModel.InfantName = infantName.Text;
                createInfantAccountModel.InfantGender = infantGender.SelectedItem.ToString();

                var response = await new InfantAccountService().CreateInfantAccount(createInfantAccountModel);

                if (response.IsSuccess)
                {
                    _ = DisplayAlert("Aviso", "La información se actualizó correctamente.", "OK");
                    _ = Navigation.PushAsync(new InfantAccountPage());
                }
                else
                {
                    _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
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

        private void InfantGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            int position = infantGender.SelectedIndex;

            if (position > -1)
            {
                infantGender.SelectedIndex = position;
            }
        }
    }
}