using ParentalControl.App.Mobile.Models;
using ParentalControl.App.Mobile.Services;
using Plugin.DeviceInfo;
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
            GetDeviceInfoModel getDeviceInfoModel = new GetDeviceInfoModel();
            getDeviceInfoModel.DevicePhoneCode = CrossDeviceInfo.Current.Id;
            getDeviceInfoModel.ParentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));
            var response = await new DeviceService().GetDeviceInfo(getDeviceInfoModel);

            if(response != null)
            {
                if (!string.IsNullOrEmpty(response.MessageError))
                {
                    _ = DisplayAlert("Error", response.MessageError, "OK");
                }
                else
                {
                    // Nombre del Dispositivo
                    txtDeviceName.Text = response.DevicePhoneName;

                    foreach (var infant in response.infantAccountList)
                    {
                        cmbInfants.Items.Add(infant.InfantName);
                    }

                    // Combobox de las Cuentas Infantiles
                    if (response.InfantAccountId == null)
                    {
                        cmbInfants.SelectedIndex = 0;
                    }
                    else
                    {
                        int index = response.infantAccountList.FindIndex(x => x.InfantAccountId == response.InfantAccountId);
                        cmbInfants.SelectedIndex = index;
                    }

                    int rowCount = 0;

                    // Table DevicPhoneUse
                    foreach (var use in response.devicePhoneUseList)
                    {
                        Picker picker = new Picker{ Title = "Horario", FontSize = 18, VerticalOptions = LayoutOptions .Center};

                        foreach (var schedule in response.scheduleList)
                        {
                            picker.Items.Add(schedule.ScheduleTime);
                        }

                        if(use.ScheduleId == null)
                        {
                            picker.SelectedIndex = 0;
                        }
                        else
                        {
                            int index = response.scheduleList.FindIndex(x => x.ScheduleId == use.ScheduleId);
                            picker.SelectedIndex = index;
                        }

                        tblDeviceUse.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        tblDeviceUse.Children.Add(new Label { Text = $"{use.DevicePhoneUseDay}:", FontSize = 18,
                                                  VerticalOptions = LayoutOptions.Center, Padding = new Thickness(0,0,0,8)},
                                                  0, rowCount);

                        tblDeviceUse.Children.Add(picker, 1, rowCount);

                        rowCount++;
                    }
                    
                }
            }
            else
            {
                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                _ = Navigation.PushAsync(new HomePage());
            }
        }

        private void Infant_SelectedIndexChanged(object sender, EventArgs a)
        {
            int position = cmbInfants.SelectedIndex;

            if(position > -1)
            {
                cmbInfants.SelectedIndex = position;
            }
        }

        private async void DeleteDevice_Clicked(object sender, EventArgs a)
        {

        }

        private async void SaveDeviceChanges_Clicked(object sender, EventArgs a)
        {

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