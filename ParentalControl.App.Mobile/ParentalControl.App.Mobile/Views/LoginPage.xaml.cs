using Newtonsoft.Json;
using ParentalControl.App.Mobile.Models;
using ParentalControl.App.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
                    _ = DisplayAlert("Error", "Email no válido", "OK");
                }
                else
                {
                    LoginModel loginModel = new LoginModel();
                    loginModel.ParentEmail = txtEmail.Text;
                    loginModel.ParentPassword = txtPassword.Text;

                    var response = await new LoginService().VerifyLogin(loginModel);
                }                
            }
            else
            {
                _ = DisplayAlert("Error", "Complete todos los campos requeridos", "OK");
            }
        }

        private void TapGestureLogin_Tapped(object sender, EventArgs a)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}