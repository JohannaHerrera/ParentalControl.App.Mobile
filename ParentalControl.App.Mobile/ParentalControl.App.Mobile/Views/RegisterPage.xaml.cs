using ParentalControl.App.Mobile.Models;
using ParentalControl.App.Mobile.Services;
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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void Register_Clicked(object sender, EventArgs a)
        {
            if (!(string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text) 
                || string.IsNullOrEmpty(txtUsername.Text)))
            {
                if (!(txtEmail.Text.Contains("@") && txtEmail.Text.Contains(".")))
                {
                    _ = DisplayAlert("Error", "Email no válido.", "OK");
                }
                else
                {
                    RegisterModel registerModel = new RegisterModel();
                    registerModel.ParentUsername = txtUsername.Text;
                    registerModel.ParentEmail = txtEmail.Text;
                    registerModel.ParentPassword = txtPassword.Text;

                    var response = await new LoginService().Register(registerModel);

                    if (response != null)
                    {
                        if (!string.IsNullOrEmpty(response.MessageError))
                        {
                            _ = DisplayAlert("Error", response.MessageError, "OK");
                        }
                        else
                        {
                            if (!response.Registered)
                            {
                                _ = DisplayAlert("Error", "Ocurrió un problema en el registro. Inténtelo de nuevo.", "OK");
                            }
                            else
                            {
                                _ = DisplayAlert("¡Felicidades!", "Se registró correctamente.", "OK");
                                _ = Navigation.PushAsync(new LoginPage());
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

        private void TapGestureRegister_Tapped(object sender, EventArgs a)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}