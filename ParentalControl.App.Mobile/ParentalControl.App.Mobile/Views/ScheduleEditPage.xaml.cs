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
        public ScheduleEditPage()
        {
            InitializeComponent();
            LoadSchedule();
        }
        public async void LoadSchedule()
        {
            ScheduleEditFindModel getScheduleInfoModel = new ScheduleEditFindModel();
            getScheduleInfoModel.ScheduleId= Convert.ToInt32(Preferences.Get("ParentId", "0"));
            var response = await new ScheduleService().ScheduleFindEdit(getScheduleInfoModel);
            if (response!=null)
            {

                if (!string.IsNullOrEmpty(response.MessageError))
                {

                    _ = DisplayAlert("Error", response.MessageError, "OK");
                }
                else
                {

                    DateTime horaI = Convert.ToDateTime(response.ScheduleStartTime);
                    startPick.Time = horaI.TimeOfDay;
                    DateTime horaF = Convert.ToDateTime(response.ScheduleEndTime);
                    endPick.Time = horaF.TimeOfDay;
                    //schedules.Add(item);
                }


            }
            else
            {
                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                _ = Navigation.PushAsync(new HomePage());
            }
        }
        private async void Edit_Clicked(object sender, EventArgs a)
        {
            if (startPick != null && endPick != null)
            {

                if (startPick.Time >= endPick.Time)
                {
                    _ = DisplayAlert("Error", "La hora de inicio debe ser mayor a la hora final", "OK");
                }
                else
                {
                    ScheduleUpdateModel updateModel = new ScheduleUpdateModel();
                    updateModel.ParentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));
                    updateModel.ScheduleStartTime = Convert.ToDateTime(startPick.Time.ToString());
                    updateModel.ScheduleEndTime = Convert.ToDateTime(endPick.Time.ToString());
                    updateModel.ScheduleId = 1;

                    var response = await new ScheduleService().ScheduleUpdate(updateModel);
                    if (response )
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
            }
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