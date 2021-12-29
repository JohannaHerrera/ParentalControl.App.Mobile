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
    public partial class NotificationsPage : ContentPage
    {
        private List<RequestModel> stateNotifications = new List<RequestModel>();

        public NotificationsPage()
        {
            InitializeComponent();
            LoadInformation();
        }

        public async void LoadInformation()
        {
            int parentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));

            var response = await new NotificationsService().GetRequests(parentId);

            if (response != null)
            {
                if (!string.IsNullOrEmpty(response.MessageError))
                {
                    _ = DisplayAlert("Error", response.MessageError, "OK");
                }
                else
                {
                    // Guardo la información de las notificaciones
                    stateNotifications = response.requestModelList;

                    int rowCount = 0;

                    // Table Notifications
                    foreach (var request in response.requestModelList)
                    {
                        tblNotifications.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                        Grid gridInfant = new Grid
                        {
                            RowDefinitions =
                            {
                                new RowDefinition { Height = new GridLength(50) },
                                new RowDefinition { Height = new GridLength(25) },
                            },
                            ColumnDefinitions =
                            {
                                new ColumnDefinition(),
                            },
                            VerticalOptions = LayoutOptions.Center,
                        };

                        // Infant Image
                        if (request.InfantGender == "Femenino")
                        {
                            gridInfant.Children.Add(new Image
                            {
                                VerticalOptions = LayoutOptions.EndAndExpand,
                                HorizontalOptions = LayoutOptions.Center,
                                Source = "../ParentalControl.App.Mobile.Android/Resources/drawable/nina_64.png"
                            }, 0, 0);
                        }
                        else
                        {
                            gridInfant.Children.Add(new Image
                            {
                                VerticalOptions = LayoutOptions.EndAndExpand,
                                HorizontalOptions = LayoutOptions.Center,
                                Source = "../ParentalControl.App.Mobile.Android/Resources/drawable/hijo_64.png"
                            }, 0, 0);
                        }

                        // InfantName
                        gridInfant.Children.Add(new Label
                        {
                            Text = request.InfantName,
                            FontSize = 18,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Center,
                            FontAttributes = FontAttributes.Bold,
                        }, 0, 1);

                        tblNotifications.Children.Add(gridInfant, 0, rowCount);

                        // Request Description
                        tblNotifications.Children.Add(new Label
                        {
                            Text = request.RequestDescription,
                            FontSize = 16,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                        }, 1, rowCount);

                        // Request Buttons
                        if(request.RequestState == 0)
                        {
                            Grid grid = new Grid
                            {
                                RowDefinitions =
                                {
                                    new RowDefinition { Height = new GridLength(50) },
                                    new RowDefinition { Height = new GridLength(50) },
                                },
                                ColumnDefinitions =
                                {
                                    new ColumnDefinition(),
                                },
                                VerticalOptions = LayoutOptions.Center,
                            };

                            grid.Children.Add(new Button
                            {
                                Text = "Aprobar",
                                TextColor = Color.White,
                                BackgroundColor = Color.Green,
                                CornerRadius = 20,
                                HeightRequest = 50,
                                VerticalOptions = LayoutOptions.Center,
                                Command = new Command((requestId) => Approve(request.RequestId)),
                            }, 0, 0);

                            grid.Children.Add(new Button
                            {
                                Text = "Desaprobar",
                                TextColor = Color.White,
                                BackgroundColor = Color.Red,
                                CornerRadius = 20,
                                HeightRequest = 50,
                                VerticalOptions = LayoutOptions.Center,
                                Command = new Command((requestId) => Disapprove(request.RequestId)),
                            }, 0, 1);

                            tblNotifications.Children.Add(grid, 2, rowCount);
                        }
                        else if(request.RequestState == 1)
                        {
                            Grid grid = new Grid
                            {
                                RowDefinitions =
                                {
                                    new RowDefinition { Height = new GridLength(50) },
                                },
                                ColumnDefinitions =
                                {
                                    new ColumnDefinition(),
                                },
                                VerticalOptions = LayoutOptions.Center,
                            };

                            grid.Children.Add(new Button
                            {
                                Text = "Aprobado",
                                TextColor = Color.White,
                                BackgroundColor = Color.Gray,
                                CornerRadius = 20,
                                HeightRequest = 50,
                                VerticalOptions = LayoutOptions.Center,
                            }, 0, 0);

                            tblNotifications.Children.Add(grid, 2, rowCount);
                        }
                        else if (request.RequestState == 2)
                        {
                            Grid grid = new Grid
                            {
                                RowDefinitions =
                                {
                                    new RowDefinition { Height = new GridLength(50) },
                                },
                                ColumnDefinitions =
                                {
                                    new ColumnDefinition(),
                                },
                                VerticalOptions = LayoutOptions.Center,
                            };

                            grid.Children.Add(new Button
                            {
                                Text = "Desaprobado",
                                TextColor = Color.White,
                                BackgroundColor = Color.Gray,
                                CornerRadius = 20,
                                HeightRequest = 50,
                                VerticalOptions = LayoutOptions.Center,
                            }, 0, 0);

                            tblNotifications.Children.Add(grid, 2, rowCount);
                        }
                        else if (request.RequestState == 3)
                        {
                            Grid grid = new Grid
                            {
                                RowDefinitions =
                                {
                                    new RowDefinition { Height = new GridLength(60) },
                                },
                                ColumnDefinitions =
                                {
                                    new ColumnDefinition(),
                                },
                                VerticalOptions = LayoutOptions.Center,
                            };

                            grid.Children.Add(new Button
                            {
                                Text = "Sin Respuesta",
                                TextColor = Color.White,
                                BackgroundColor = Color.Orange,
                                CornerRadius = 20,
                                HeightRequest = 60,
                                VerticalOptions = LayoutOptions.Center,
                            }, 0, 0);

                            tblNotifications.Children.Add(grid, 2, rowCount);
                        }

                        rowCount++;

                        tblNotifications.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        tblNotifications.Children.Add(new Label { HeightRequest = 1, BackgroundColor = Color.FromHex("#2D60B3") }, 0, rowCount);
                        tblNotifications.Children.Add(new Label { HeightRequest = 1, BackgroundColor = Color.FromHex("#2D60B3") }, 1, rowCount);
                        tblNotifications.Children.Add(new Label { HeightRequest = 1, BackgroundColor = Color.FromHex("#2D60B3") }, 2, rowCount);

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

        private async void Approve(int requestId)
        {
            UpdateRequestModel updateRequestModel = new UpdateRequestModel();
            updateRequestModel.RequestId = requestId;
            updateRequestModel.RequestAction = 1;

            var response = await new NotificationsService().UpdateRequest(updateRequestModel);

            if (response)
            {
                _ = DisplayAlert("Aviso", "Petición aprobada correctamente.", "OK");
                _ = Navigation.PushAsync(new NotificationsPage());
            }
            else
            {
                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                _ = Navigation.PushAsync(new NotificationsPage());
            }
        }

        private async void Disapprove(int requestId)
        {
            UpdateRequestModel updateRequestModel = new UpdateRequestModel();
            updateRequestModel.RequestId = requestId;
            updateRequestModel.RequestAction = 2;

            var response = await new NotificationsService().UpdateRequest(updateRequestModel);

            if (response)
            {
                _ = DisplayAlert("Aviso", "Petición desaprobada correctamente.", "OK");
                _ = Navigation.PushAsync(new NotificationsPage());
            }
            else
            {
                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                _ = Navigation.PushAsync(new NotificationsPage());
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