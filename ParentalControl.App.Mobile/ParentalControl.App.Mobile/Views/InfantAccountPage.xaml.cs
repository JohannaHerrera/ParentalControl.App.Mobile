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
    public partial class InfantAccountPage : ContentPage
    {
        private List<ListInfantAccountModel> infants = new List<ListInfantAccountModel>();
        public InfantAccountPage()
        {
            InitializeComponent();
            LoadInformation();
        }
        public async void LoadInformation()
        {
            ListInfantAccountModel listInfantAccountModel = new ListInfantAccountModel();
            listInfantAccountModel.ParentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));
            string parentId = listInfantAccountModel.ParentId.ToString();
            var response = await new InfantAccountService().GetInfantAccount(parentId);

            if (response != null)
            {
                if (!string.IsNullOrEmpty(response.MessageError))
                {
                    _ = DisplayAlert("Error", response.MessageError, "OK");
                }
                else
                {
                    int rowCount = 0;

                    // Table InfantAccount
                    foreach (var use in response.InfantAccountModelList)
                    {
                        tblInfantAccount.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        if (use.InfantGender == "Masculino")
                        {
                            tblInfantAccount.Children.Add(new Image
                            {
                                VerticalOptions = LayoutOptions.Center,
                                Source = "../ParentalControl.App.Mobile.Android/Resources/drawable/hijo_64.png"
                            }, 0, rowCount);
                        }
                        else
                        {
                            tblInfantAccount.Children.Add(new Image
                            {
                                VerticalOptions = LayoutOptions.Center,
                                Source = "../ParentalControl.App.Mobile.Android/Resources/drawable/nina_64.png"
                            }, 0, rowCount);
                        }

                        tblInfantAccount.Children.Add( new Label
                        {
                            Text = use.InfantName,
                            FontSize = 18,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            FontAttributes = FontAttributes.Bold
                        }, 1, rowCount);

                        ImageButton editButton;
                        tblInfantAccount.Children.Add(editButton = new ImageButton
                        {
                            VerticalOptions = LayoutOptions.Center,
                            BackgroundColor = Color.Transparent,
                            Source = "../ParentalControl.App.Mobile.Android/Resources/drawable/edit.png"
                        }, 2, rowCount);
                        editButton.Command = new Command((infantId) => OnEditImageButtonClicked(use.InfantAccountId));

                        ImageButton deleteButton;
                        tblInfantAccount.Children.Add(deleteButton = new ImageButton
                        {
                            VerticalOptions = LayoutOptions.Center,
                            BackgroundColor = Color.Transparent,
                            Source = "../ParentalControl.App.Mobile.Android/Resources/drawable/eliminar_32.png"
                        }, 3, rowCount);
                        deleteButton.Command = new Command((infantId) => OnDeleteImageButtonClicked(use.InfantAccountId));


                        ImageButton ib;
                        tblInfantAccount.Children.Add(ib = new ImageButton
                        {
                            VerticalOptions = LayoutOptions.Center,
                            BackgroundColor = Color.Transparent,
                            Source = "../ParentalControl.App.Mobile.Android/Resources/drawable/flecha_correcta.png"
                        }, 4, rowCount);
                        //ib.Clicked += OnImageButtonClicked;
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

        private void OnEditImageButtonClicked(int infantId)
        {
            Navigation.PushAsync(new EditInfantAccountPage(infantId));
        }

        private async void OnDeleteImageButtonClicked(int infantId)
        {
            GetInfantAccountInfoModel getInfantAccountInfoModel = new GetInfantAccountInfoModel();
            getInfantAccountInfoModel.ParentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));
            getInfantAccountInfoModel.InfantAccountId = infantId;
            getInfantAccountInfoModel.Action = 2; // Action = 2 (GetDeleteInfantAccount)

            var response = await new InfantAccountService().GetDeleteInfantAccount(getInfantAccountInfoModel);

            if (response != null)
            {
                if (!string.IsNullOrEmpty(response.MessageError))
                {
                    _ = DisplayAlert("Error", response.MessageError, "OK");
                }
                else
                {
                    if (response.IsSuccess)
                    {
                        _ = DisplayAlert("Aviso", "Se ha eliminado la cuenta de infante con éxito.", "OK");
                        LoadInformation();
                        _ = Navigation.PushAsync(new InfantAccountPage());
                    }
                    else
                    {
                        _ = DisplayAlert("Error", "No se pudo eliminar el infante. Inténtelo de nuevo.", "OK");
                    }
                }
            }
            else
            {
                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                _ = Navigation.PushAsync(new HomePage());
            }
        }

        private void OnRulesImageButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
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

        private void CreateInfantAccount_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateInfantAccountPage());
        }
    }
}