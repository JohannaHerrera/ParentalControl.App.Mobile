using ParentalControl.App.Mobile.Models;
using ParentalControl.App.Mobile.Services;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParentalControl.App.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditInfantAccountPage : ContentPage
    {
        public EditInfantAccountPage(int infantId)
        {
            InitializeComponent();
            LoadInformation(infantId);
        }

        public async void LoadInformation(int infantId)
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

            GetInfantAccountInfoModel getInfantAccountInfoModel = new GetInfantAccountInfoModel();
            getInfantAccountInfoModel.ParentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));
            getInfantAccountInfoModel.InfantAccountId = infantId;
            getInfantAccountInfoModel.Action = 1; // Action = 1 (GetDeleteInfantAccount)

            var response = await new InfantAccountService().GetDeleteInfantAccount(getInfantAccountInfoModel);

            if (response != null)
            {
                if (!string.IsNullOrEmpty(response.MessageError))
                {
                    _ = DisplayAlert("Error", response.MessageError, "OK");
                }
                else
                {
                    foreach (var use in response.InfantAccountModelList)
                    {                        
                        if (use.InfantAccountId == infantId)
                        {
                            labelInfantName.Text = use.InfantName;
                            infantName.Text = use.InfantName;
                            if (use.InfantGender == "Masculino")
                            {
                                infantImage.Source = "../ParentalControl.App.Mobile.Android/Resources/drawable/hijo_64.png";
                                infantGender.SelectedIndex = 0;
                            }
                            else
                            {
                                infantImage.Source = "../ParentalControl.App.Mobile.Android/Resources/drawable/nina_64.png";
                                infantGender.SelectedIndex = 1;
                            }
                        }
                        
                    }

                }
                btnGuardar.Command = new Command((infantAccountId) => EditInfantAccount_Clicked(infantId));
            }
            else
            {
                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                _ = Navigation.PushAsync(new HomePage());
            }
        }

        private async void EditInfantAccount_Clicked(int infantId)
        {
            UpdateInfantAccountModel updateInfantAccountModel = new UpdateInfantAccountModel();
            updateInfantAccountModel.ParentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));
            updateInfantAccountModel.InfantAccountId = infantId;
            updateInfantAccountModel.InfantName = infantName.Text;
            updateInfantAccountModel.InfantGender = infantGender.SelectedItem.ToString();

            var response = await new InfantAccountService().UpdateInfantAccount(updateInfantAccountModel);

            if (response)
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

        private void Home_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        private void InfantAccounts_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InfantAccountPage());
        }

        private void Device_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DevicePage());
        }

        private void Schedules_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SchedulePage());
        }

        private void Notifications_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotificationsPage());
        }

        private void MyAccount_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyProfilePage());
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Xamarin.Essentials.Preferences.Clear();
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