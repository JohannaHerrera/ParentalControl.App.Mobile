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
    public partial class ScheduleCreatePage : ContentPage
    {
        public ScheduleCreatePage()
        {
            InitializeComponent();
            startPick.Time = DateTime.Now.TimeOfDay;
            endPick.Time=DateTime.Now.TimeOfDay;
        }

        private async void Create_Clicked(object sender, EventArgs a)
        {
            if(startPick!=null && endPick != null)
            {
                
                if (startPick.Time >= endPick.Time)
                {
                    _ = DisplayAlert("Error", "La hora de inicio debe ser mayor a la hora final", "OK");
                }
                else
                {
                    ScheduleRegisterModel registerModel = new ScheduleRegisterModel();
                    registerModel.ParentId= Convert.ToInt32(Preferences.Get("ParentId", "0"));
                    registerModel.ScheduleStartTime = Convert.ToDateTime(startPick.Time.ToString());
                    registerModel.ScheduleEndTime = Convert.ToDateTime(endPick.Time.ToString());
                    
                    var response = await new ScheduleService().ScheduleCreate(registerModel);
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
                                _ = Navigation.PushAsync(new SchedulePage());
                            }
                        }
                    }
                    else
                    {
                        _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                    }
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