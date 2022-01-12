using ParentalControl.App.Mobile.Services;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParentalControl.App.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewRequestsPage : ContentPage
    {
        public ViewRequestsPage()
        {
            InitializeComponent();
            LoadInformation();
        }

        private async void LoadInformation()
        {
            string devicePhoneCode = CrossDeviceInfo.Current.Id;

            var response = await new GetInfantRequestService().GetInfantRequests(devicePhoneCode);

            if(response != null)
            {
                if (!string.IsNullOrEmpty(response.MessageError))
                {
                    _ = DisplayAlert("Aviso", response.MessageError, "OK");
                    _ = Navigation.PushAsync(new InfantPrincipalPage());
                }
                else
                {
                    if(response.requestModelList != null && response.requestModelList.Count() > 0)
                    {
                        int rowCount = 0;
                        foreach (var request in response.requestModelList)
                        {
                            tblNotifications.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                            tblNotifications.Children.Add(new Label
                            {
                                Text = request.RequestDescription,
                                FontSize = 18,
                                VerticalOptions = LayoutOptions.Center,
                                Padding = new Thickness(0, 0, 0, 8)
                            }, 0, rowCount);

                            var label = new Label
                            {
                                Text = request.StateRequest,
                                FontSize = 20,
                                VerticalOptions = LayoutOptions.Center,
                                FontAttributes = FontAttributes.Bold
                            };

                            if (request.StateRequest.ToLower().Equals("En espera".ToLower()))
                            {
                                label.TextColor = Color.Blue;
                            }
                            else if (request.StateRequest.ToLower().Equals("Aprobado".ToLower()))
                            {
                                label.TextColor = Color.Green;
                            }
                            else if (request.StateRequest.ToLower().Equals("Desaprobado".ToLower()))
                            {
                                label.TextColor = Color.Red;
                            }
                            else if (request.StateRequest.ToLower().Equals("Sin Respuesta".ToLower()))
                            {
                                label.TextColor = Color.Orange;
                            }

                            tblNotifications.Children.Add(label, 1, rowCount);

                            rowCount++;
                        }
                    }
                    else
                    {
                        _ = DisplayAlert("Aviso", "Aún no tienes notificaciones por revisar", "OK");
                        _ = Navigation.PushAsync(new InfantPrincipalPage());
                    }
                }
            }
            else
            {
                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                _ = Navigation.PushAsync(new InfantPrincipalPage());
            }
        }

        private void Back_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new RequestPage());
        }
    }
}