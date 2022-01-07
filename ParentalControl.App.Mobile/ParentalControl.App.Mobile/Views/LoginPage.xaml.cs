using Android.Content.PM;
using Newtonsoft.Json;
using ParentalControl.App.Mobile.Interfaces;
using ParentalControl.App.Mobile.Models;
using ParentalControl.App.Mobile.Services;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParentalControl.App.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs a)
        {
            if (!(string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text)))
            {
                if (!(txtEmail.Text.Contains("@") && txtEmail.Text.Contains(".")))
                {
                    _ = DisplayAlert("Error", "Email no válido.", "OK");
                }
                else
                {
                    LoginModel loginModel = new LoginModel();
                    DevicePhoneModel devicePhoneModel = new DevicePhoneModel();
                    loginModel.ParentEmail = txtEmail.Text;
                    loginModel.ParentPassword = txtPassword.Text;
                    devicePhoneModel.DevicePhoneCode = CrossDeviceInfo.Current.Id;
                    devicePhoneModel.DevicePhoneName = CrossDeviceInfo.Current.DeviceName;

                    List<AppDeviceModel> appsInstalled = new List<AppDeviceModel>();
                    var apps = Android.App.Application.Context.PackageManager.GetInstalledApplications(PackageInfoFlags.MatchAll);

                    foreach (var app in apps)
                    {
                        AppDeviceModel appDeviceModel = new AppDeviceModel();
                        appDeviceModel.AppDeviceName = app.LoadLabel(Android.App.Application.Context.PackageManager);
                        appsInstalled.Add(appDeviceModel);
                    }

                    devicePhoneModel.AppsInstalled = appsInstalled;
                    loginModel.deviceModel = devicePhoneModel;

                    var response = await new LoginService().VerifyLogin(loginModel);

                    if (response != null)
                    {
                        if (!string.IsNullOrEmpty(response.MessageError))
                        {
                            _ = DisplayAlert("Error", response.MessageError, "OK");
                        }
                        else
                        {
                            if (response.ParentId == 0)
                            {
                                _ = DisplayAlert("Error", "Credenciales incorrectas.", "OK");
                            }
                            else
                            {
                                Preferences.Set("ParentId", response.ParentId.ToString());
                                Preferences.Set("ParentUsername", response.ParentUserName);
                                Preferences.Set("ParentEmail", response.ParentEmail);

                                _ = Navigation.PushAsync(new HomePage());

                                if (response.IsFirstTime)
                                {
                                    DependencyService.Get<IAndroidService>().StartService();
                                }
                            }
                        }
                    }
                    else
                    {
                        _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                    }                   
                }                
            }
            else
            {
                _ = DisplayAlert("¡Aviso!", "Complete todos los campos requeridos.", "OK");
            }
        }

        private void TapGestureLogin_Tapped(object sender, EventArgs a)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private void BackInfant_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new InfantPrincipalPage());
        }
}
}