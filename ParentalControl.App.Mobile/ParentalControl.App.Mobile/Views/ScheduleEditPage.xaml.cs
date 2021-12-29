using ParentalControl.App.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ParentalControl.App.Mobile.Services;


namespace ParentalControl.App.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleEditPage : ContentPage
    {
        public ScheduleEditPage(int id)
        {
            InitializeComponent();
            LoadSchedule(id);
        }
        public async void LoadSchedule(int id)
        {
            ScheduleEditFindModel getScheduleInfoModel = new ScheduleEditFindModel();
            getScheduleInfoModel.ScheduleId= id;
            var response = await new ScheduleService().ScheduleFindEdit(getScheduleInfoModel);

            if (response!=null)
            {

                int i = 1;
                if (!string.IsNullOrEmpty(response.MessageError))
                {

                    _ = DisplayAlert("Error", response.MessageError, "OK");
                }
                else
                {
                    tblSchedule.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    tblSchedule.Children.Add(new Label
                    {
                        Text = "Desde: ",
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        FontSize = 20,
                        TextColor = Color.Indigo,
                        VerticalOptions = LayoutOptions.Center,
                        Padding = new Thickness(0, 0, 0, 8)
                    }, 0, 0);
                    tblSchedule.Children.Add(new Label
                    {
                        Text = response.ScheduleStartTime,
                        HorizontalOptions= LayoutOptions.CenterAndExpand,   
                        FontSize = 20,
                        TextColor = Color.Indigo,
                        VerticalOptions = LayoutOptions.Center,
                        Padding = new Thickness(0, 0, 0, 8)
                    }, 1, 0) ;
                    tblSchedule.Children.Add(new Label
                    {
                        Text = "Hasta: ",
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        FontSize = 20,
                        TextColor = Color.Indigo,
                        VerticalOptions = LayoutOptions.Center,
                        Padding = new Thickness(0, 0, 0, 8)
                    }, 2, 0);
                    tblSchedule.Children.Add(new Label
                    {
                        Text = response.ScheduleEndTime,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        FontSize = 20,
                        TextColor = Color.Indigo,
                        VerticalOptions = LayoutOptions.Center,  
                        Padding = new Thickness(0, 0, 0, 8)
                    }, 3, 0);


                    //string hi = response.ScheduleStartTime;
                    //DateTime horaI = DateTime("");
                    //DateTime horaI = Convert.ToDateTime(hi);
                    //startPick.Time = horaI.TimeOfDay;
                    //DateTime horaF = Convert.ToDateTime(response.ScheduleEndTime);
                    //endPick.Time = horaF.TimeOfDay;
                }
                btnGuardar.Command = new Command((infantAccountId) => Edit_Clicked(id));

            }
            else
            {
                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                _ = Navigation.PushAsync(new HomePage());
            }
        }
        private async void Edit_Clicked(int id)
        {
            if (startPick != null && endPick != null)
            {

                if (startPick.Time < endPick.Time)
                {
                    ScheduleUpdateModel updateModel = new ScheduleUpdateModel();
                    updateModel.ParentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));
                    updateModel.ScheduleStartTime = Convert.ToDateTime(startPick.Time.ToString());
                    updateModel.ScheduleEndTime = Convert.ToDateTime(endPick.Time.ToString());
                    updateModel.ScheduleId = id;

                    var response = await new ScheduleService().ScheduleUpdate(updateModel);
                    if (response)
                    {
                        _ = DisplayAlert("Aviso", "La información se actualizó correctamente.", "OK");
                        _ = Navigation.PushAsync(new SchedulePage());
                    }
                    else
                    {
                        _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                        _ = Navigation.PushAsync(new SchedulePage());
                    }
                    }
                else
                {
                    _ = DisplayAlert("Error", "La hora de inicio debe ser mayor a la hora final", "OK");
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

        }

        private void MyAccount_Clicked(object sender, EventArgs a)
        {

        }

        private void Logout_Clicked(object sender, EventArgs a)
        {

        }
    }
}