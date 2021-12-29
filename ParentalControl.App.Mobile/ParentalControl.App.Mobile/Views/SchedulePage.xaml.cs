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
        public static int idSchedule;
        Button buttonEdit = new Button
        {
            Text = "Click to Rotate Text!",
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.Center
        };
        Button buttonDelete = new Button
        {
            Text = "Click to Rotate Text!",
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.Center
        };
        private List<ScheduleResponseModel> schedules = new List<ScheduleResponseModel>();
        public SchedulePage()
        {
            InitializeComponent();
            LoadSchedule();

        }
        public async void LoadSchedule()
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

                        idSchedule = item.ScheduleId;
                        ListSchedule.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        ListSchedule.Children.Add(new Label {Text = item.ScheduleStartTime, 
                                                FontSize = 18,VerticalOptions = LayoutOptions.Center,
                                                Padding = new Thickness(0, 0, 0, 8) },0, rowCount);
                        ListSchedule.Children.Add(new Label{Text = item.ScheduleEndTime,FontSize = 18,
                                                    VerticalOptions = LayoutOptions.Center, 
                                                    Padding = new Thickness(0, 0, 0, 8)}, 1, rowCount);
                        ListSchedule.Children.Add(new Button { ImageSource = "edit.png",  BackgroundColor = Color.Transparent},2,rowCount);
                        ListSchedule.Children.Add(new Button { ImageSource = "borrar.png", BackgroundColor = Color.Transparent }, 3, rowCount);
                        rowCount++;
                        this.buttonDelete.Clicked += EditSchedule_Clicked;
                        this.buttonDelete.Clicked += DeleteSchedule_Clicked;
                        //schedules.Add(item);
                    }
                }

            }
            else
            {
                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                _ = Navigation.PushAsync(new HomePage());
            }
        }
        private void EditSchedule_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new ScheduleEditPage());
        }
        private void DeleteSchedule_Clicked(object sender, EventArgs a)
        {

        }
        private void CreateSchedule_Clicked(object sender, EventArgs a)
        {
            //Navigation.PushAsync(new ScheduleCreatePage());
            Navigation.PushAsync(new ScheduleEditPage());
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