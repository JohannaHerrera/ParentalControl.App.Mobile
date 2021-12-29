using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.DeviceInfo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ParentalControl.App.Mobile.Models;
using ParentalControl.App.Mobile.Services;
using Xamarin.Essentials;

namespace ParentalControl.App.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : ContentPage
    {


        public SchedulePage()
        {
            InitializeComponent();
            LoadInformation();

        }
        public async void LoadInformation()
        {
            GetScheduleInfoModel getScheduleInfoModel = new GetScheduleInfoModel();
            getScheduleInfoModel.ParentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));
            //getDeviceInfoModel.Action = 1; // Action = 1 (GetDeviceInfo)
            var response = await new ScheduleService().ScheduleIndex(getScheduleInfoModel);
            if (response.Count() > 0)
            {
                int rowCount = 0;
                foreach (var item in response)
                {
                    if (!string.IsNullOrEmpty(item.MessageError))
                    {

                        _ = DisplayAlert("Error", item.MessageError, "OK");
                    }
                    else
                    {
                        ListSchedule.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        ListSchedule.Children.Add(new Label {Text = item.ScheduleStartTime, 
                                                FontSize = 18,VerticalOptions = LayoutOptions.Center,
                                                Padding = new Thickness(0, 0, 0, 8) },0, rowCount);
                        ListSchedule.Children.Add(new Label{Text = item.ScheduleEndTime,FontSize = 18,
                                                    VerticalOptions = LayoutOptions.Center, 
                                                    Padding = new Thickness(0, 0, 0, 8)}, 1, rowCount);

                        ImageButton buttonEdit;                       
                        ListSchedule.Children.Add(buttonEdit = new ImageButton
                        {
                            Source = "edit.png",
                            BackgroundColor = Color.Transparent,
                            VerticalOptions = LayoutOptions.Center
                        }, 2, rowCount);
                        ImageButton buttonDelete;
                        ListSchedule.Children.Add(buttonDelete = new ImageButton { VerticalOptions = LayoutOptions.CenterAndExpand, Source = "borrar.png", BackgroundColor = Color.Transparent }, 3, rowCount);
                        rowCount++;
                        buttonEdit.Command = new Command((infantId) => EditSchedule_Clicked(item.ScheduleId));
                        buttonDelete.Command = new Command((infantId) => DeleteSchedule_Clicked(item.ScheduleId));


                        //this.buttonDelete.Clicked += DeleteSchedule_Clicked;

                    }
                }

            }
            else
            {
                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                _ = Navigation.PushAsync(new HomePage());
            }
        }
        private void EditSchedule_Clicked(int id)
        {
            Navigation.PushAsync(new ScheduleEditPage(id));
        }
        private async void DeleteSchedule_Clicked(int id)
        {
            GetScheduleInfoModel infoModel = new GetScheduleInfoModel();
            infoModel.ScheduleId = id;
            var response = await new ScheduleService().DeleteSchedule(infoModel);
            if (response!=null)
            {
                if (response.IsSuccess)
                {
                    _ = DisplayAlert("Aviso", "Se ha eliminado el horario con éxito.", "OK");
                    LoadInformation();
                    _ = Navigation.PushAsync(new SchedulePage());
                }
                else
                {
                    _ = DisplayAlert("Error", "No se pudo eliminar el horario. Inténtelo de nuevo.", "OK");
                }
            }
            else
            {
                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                _ = Navigation.PushAsync(new SchedulePage());
            }
        }
        private void CreateSchedule_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new ScheduleCreatePage());

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

        }

        private void MyAccount_Clicked(object sender, EventArgs a)
        {

        }

        private void Logout_Clicked(object sender, EventArgs a)
        {

        }

    }
}