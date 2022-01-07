using ParentalControl.App.Mobile.Models;
using ParentalControl.App.Mobile.Services;
using Plugin.DeviceInfo;
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
    public partial class InfantPrincipalPage : ContentPage
    {
        private List<ScheduleModel> schedules = new List<ScheduleModel>();

        public InfantPrincipalPage()
        {
            InitializeComponent();
            LoadInformation();
        }

        public async void LoadInformation()
        {
            string devicePhoneCode = CrossDeviceInfo.Current.Id;

            var response = await new GetDeviceConfigurationService().GetDeviceConfiguration(devicePhoneCode);

            if (response != null)
            {
                if (!response.HaveRules)
                {
                    _ = DisplayAlert("Aviso", "El Dispositivo aún no tiene configurada ninguna regla.", "OK");
                    _ = Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    if (!string.IsNullOrEmpty(response.MessageError))
                    {
                        _ = DisplayAlert("Error", response.MessageError, "OK");
                        _ = Navigation.PushAsync(new LoginPage());
                    }
                    else
                    {
                        // Guardo la información de los horarios e infantes
                        schedules = response.scheduleModelList;

                        int rowCount = 0;

                        if (response.WebsLocked.Count() > 0)
                        {
                            foreach (var web in response.WebsLocked)
                            {
                                tblWebsLocked.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                                tblWebsLocked.Children.Add(new Label
                                {
                                    Text = web.CategoryName,
                                    FontSize = 18,
                                    VerticalOptions = LayoutOptions.Center,
                                    Padding = new Thickness(0, 0, 0, 8)
                                }, 0, rowCount);

                                rowCount++;
                            }
                        }
                        else
                        {
                            tblWebsLocked.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                            tblWebsLocked.Children.Add(new Label
                            {
                                Text = "No tiene categorías web bloqueadas.",
                                FontSize = 18,
                                VerticalOptions = LayoutOptions.Center,
                                Padding = new Thickness(0, 0, 0, 8)
                            }, 0, rowCount);
                        }
                        

                        rowCount = 0;
                        if (response.AppsLocked.Count() > 0)
                        {
                            foreach (var app in response.AppsLocked)
                            {
                                tblAppsLocked.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                                tblAppsLocked.Children.Add(new Label
                                {
                                    Text = app.AppName,
                                    FontSize = 18,
                                    VerticalOptions = LayoutOptions.Center,
                                    Padding = new Thickness(0, 0, 0, 8)
                                }, 0, rowCount);

                                rowCount++;
                            }
                        }
                        else
                        {
                            tblAppsLocked.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                            tblAppsLocked.Children.Add(new Label
                            {
                                Text = "No tiene aplicaciones bloqueadas.",
                                FontSize = 18,
                                VerticalOptions = LayoutOptions.Center,
                                Padding = new Thickness(0, 0, 0, 8)
                            }, 0, rowCount);
                        }
                        

                        rowCount = 0;
                        if(response.DeviceConfig.Count() > 0)
                        {
                            foreach (var use in response.DeviceConfig)
                            {
                                tblDeviceUse.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                                tblDeviceUse.Children.Add(new Label
                                {
                                    Text = use.DevicePhoneUseDay,
                                    FontSize = 18,
                                    VerticalOptions = LayoutOptions.Center,
                                    Padding = new Thickness(0, 0, 0, 8)
                                }, 0, rowCount);

                                tblDeviceUse.Children.Add(new Label
                                {
                                    Text = schedules.Where(x => x.ScheduleId == use.ScheduleId).FirstOrDefault().ScheduleTime,
                                    FontSize = 18,
                                    VerticalOptions = LayoutOptions.Center,
                                    Padding = new Thickness(0, 0, 0, 8)
                                }, 1, rowCount);

                                rowCount++;
                            }
                        }
                        else
                        {
                            tblDeviceUse.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                            tblDeviceUse.Children.Add(new Label
                            {
                                Text = "No tiene límites de uso",
                                FontSize = 18,
                                VerticalOptions = LayoutOptions.Center,
                                Padding = new Thickness(0, 0, 0, 8)
                            }, 0, rowCount);
                        }
                    }
                }                
            }
            else
            {
                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                _ = Navigation.PushAsync(new InfantPrincipalPage());
            }
        }

        private void Request_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new RequestPage());
        }

        private void Login_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}