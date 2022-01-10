using ParentalControl.App.Mobile.Models;
using ParentalControl.App.Mobile.Services;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParentalControl.App.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestPage : ContentPage
    {
        private List<ScheduleModel> schedules = new List<ScheduleModel>();
        List<WebConfigurationRulesModel> WebsLocked = new List<WebConfigurationRulesModel>();
        List<AppRulesModel> AppsLocked = new List<AppRulesModel>();
        List<DevicePhoneUseModel> DeviceConfig = new List<DevicePhoneUseModel>();
        private int deviceId = 0;
        private int infantId = 0;
        private int parentId = 0;

        public RequestPage()
        {
            InitializeComponent();
            LoadInformation();
        }

        public async void LoadInformation()
        {
            stkObjects.IsVisible = false;
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
                        deviceId = response.DevicePhoneId;
                        parentId = response.ParentId;
                        schedules = response.scheduleModelList;
                        WebsLocked = response.WebsLocked;
                        AppsLocked = response.AppsLocked;
                        DeviceConfig = response.DeviceConfig;
                        if(response.InfantAccountId != null)
                        {
                            infantId = (int)response.InfantAccountId;
                        }
                        cmbRequestTypes.Items.Add("Desbloqueo Web");
                        cmbRequestTypes.Items.Add("Desbloqueo de Aplicaciones");
                        cmbRequestTypes.Items.Add("Ampliar uso del Dispositivo");
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
            Navigation.PushAsync(new InfantPrincipalPage());
        }

        private void ViewRequests_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new ViewRequestsPage());
        }

        private async void Request_SelectedIndexChanged(object sender, EventArgs a)
        {
            btnSend.IsVisible = false;
            int position = cmbRequestTypes.SelectedIndex;

            if (position > -1)
            {
                if(position == 0)
                {
                    if(WebsLocked.Count() > 0)
                    {
                        stkObjects.IsVisible = true;
                        stkDeviceUse.IsVisible = false;
                        btnSend.IsVisible = false;
                        cmbObjects.Items.Clear();
                        cmbHours.Items.Clear();
                        cmbMinutes.Items.Clear();
                        lblCategories.Text = "Selecciona la categoría:";

                        foreach(var web in WebsLocked)
                        {
                            cmbObjects.Items.Add(web.CategoryName);
                        }
                    }
                    else
                    {
                        _ = DisplayAlert("Notificación", "No tienes categorías web bloqueadas.", "OK");
                        _ = Navigation.PushAsync(new RequestPage());
                    }
                }
                else if (position == 1)
                {
                    if(AppsLocked.Count() > 0)
                    {
                        stkObjects.IsVisible = true;
                        stkDeviceUse.IsVisible = false;
                        btnSend.IsVisible = false;
                        cmbObjects.Items.Clear();
                        cmbHours.Items.Clear();
                        cmbMinutes.Items.Clear();
                        lblCategories.Text = "Selecciona la aplicación:";

                        foreach (var app in AppsLocked)
                        {
                            cmbObjects.Items.Add(app.AppName);
                        }
                    }
                    else
                    {
                        _ = DisplayAlert("Notificación", "No tienes aplicaciones bloqueadas.", "OK");
                        _ = Navigation.PushAsync(new RequestPage());
                    }
                }
                else if(position == 2)
                {
                    btnSend.IsVisible = false;
                    cmbObjects.Items.Clear();
                    cmbHours.Items.Clear();
                    cmbMinutes.Items.Clear();
                    DateTime currentDate = DateTime.Now;
                    string day = currentDate.ToString("dddd", CultureInfo.CreateSpecificCulture("es-ES"));

                    GetTimeByDayModel getTimeByDayModel = new GetTimeByDayModel();
                    getTimeByDayModel.DevicePhoneId = deviceId;
                    getTimeByDayModel.DevicePhoneUseDay = day;
                    var response = await new DeviceDayUseService().GetDeviceUseByDay(getTimeByDayModel);

                    if (response != null && (response.Hours != null || response.Minutes != null))
                    {
                        stkDeviceUse.IsVisible = true;
                        stkObjects.IsVisible = false;
                        
                        if(response.Hours.Count() > 0)
                        {
                            foreach(var hour in response.Hours)
                            {
                                cmbHours.Items.Add(hour);
                            }
                        }

                        if (response.Minutes.Count() > 0)
                        {
                            foreach (var minute in response.Minutes)
                            {
                                cmbMinutes.Items.Add(minute);
                            }
                        }
                    }
                    else
                    {
                        _ = DisplayAlert("Notificación", "No tienes restricción de uso para este día.", "OK");
                        _ = Navigation.PushAsync(new RequestPage());
                    }
                }
                else
                {
                    _ = DisplayAlert("Atención", "Seleccione una categoría para continuar.", "OK");
                }
            }
        }

        private void Object_SelectedIndexChanged(object sender, EventArgs a)
        {
            int position = cmbObjects.SelectedIndex;

            if (position > -1)
            {
                btnSend.IsVisible = true;
            }
        }

        private void Time_SelectedIndexChanged(object sender, EventArgs a)
        {
            int positionH = cmbHours.SelectedIndex;
            int positionM = cmbMinutes.SelectedIndex;

            if (positionH > -1 || positionM > -1)
            {
                btnSend.IsVisible = true;
            }
        }

        private async void SendRequest_Clicked(object sender, EventArgs a)
        {
            int position = cmbRequestTypes.SelectedIndex;

            if (position > -1)
            {
                if (position == 0)
                {
                    if(cmbObjects.SelectedIndex > -1)
                    {
                        SendRequestModel sendRequestModel = new SendRequestModel();
                        sendRequestModel.DevicePhoneId = deviceId;
                        sendRequestModel.InfantAccountId = infantId;
                        sendRequestModel.RequestType = 1;
                        sendRequestModel.ParentId = parentId;
                        sendRequestModel.Object = WebsLocked[cmbObjects.SelectedIndex].CategoryName;

                        var response = await new GetDeviceConfigurationService().SendRequest(sendRequestModel);

                        if (!string.IsNullOrEmpty(response.MessageError))
                        {
                            _ = DisplayAlert("Error", response.MessageError, "OK");
                            _ = Navigation.PushAsync(new RequestPage());
                        }
                        else
                        {
                            if (!response.IsSend)
                            {
                                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                                _ = Navigation.PushAsync(new RequestPage());
                            }
                            else
                            {
                                _ = DisplayAlert("Aviso", "La petición se envió correctamente.", "OK");
                                _ = Navigation.PushAsync(new RequestPage());
                            }
                        }
                    }
                }
                else if (position == 1)
                {
                    if (cmbObjects.SelectedIndex > -1)
                    {
                        SendRequestModel sendRequestModel = new SendRequestModel();
                        sendRequestModel.DevicePhoneId = deviceId;
                        sendRequestModel.InfantAccountId = infantId;
                        sendRequestModel.RequestType = 2;
                        sendRequestModel.ParentId = parentId;
                        sendRequestModel.Object = AppsLocked[cmbObjects.SelectedIndex].AppName;

                        var response = await new GetDeviceConfigurationService().SendRequest(sendRequestModel);

                        if (!string.IsNullOrEmpty(response.MessageError))
                        {
                            _ = DisplayAlert("Error", response.MessageError, "OK");
                            _ = Navigation.PushAsync(new RequestPage());
                        }
                        else
                        {
                            if (!response.IsSend)
                            {
                                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                                _ = Navigation.PushAsync(new RequestPage());
                            }
                            else
                            {
                                _ = DisplayAlert("Aviso", "La petición se envió correctamente.", "OK");
                                _ = Navigation.PushAsync(new RequestPage());
                            }
                        }
                    }
                }
                else if (position == 2)
                {
                    if (cmbHours.SelectedIndex > -1 || cmbMinutes.SelectedIndex > -1)
                    {
                        SendRequestModel sendRequestModel = new SendRequestModel();
                        sendRequestModel.DevicePhoneId = deviceId;
                        sendRequestModel.InfantAccountId = infantId;
                        sendRequestModel.RequestType = 3;
                        sendRequestModel.ParentId = parentId;

                        if(cmbHours.SelectedIndex > -1)
                        {
                            sendRequestModel.Hours = cmbHours.Items[cmbHours.SelectedIndex];
                        }

                        if(cmbMinutes.SelectedIndex > -1)
                        {
                            sendRequestModel.Minutes = cmbMinutes.Items[cmbMinutes.SelectedIndex];
                        }

                        var response = await new GetDeviceConfigurationService().SendRequest(sendRequestModel);

                        if (!string.IsNullOrEmpty(response.MessageError))
                        {
                            _ = DisplayAlert("Error", response.MessageError, "OK");
                            _ = Navigation.PushAsync(new RequestPage());
                        }
                        else
                        {
                            if (!response.IsSend)
                            {
                                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                                _ = Navigation.PushAsync(new RequestPage());
                            }
                            else
                            {
                                _ = DisplayAlert("Aviso", "La petición se envió correctamente.", "OK");
                                _ = Navigation.PushAsync(new RequestPage());
                            }
                        }
                    }
                }
                else
                {
                    _ = DisplayAlert("Atención", "Seleccione una categoría para continuar.", "OK");
                }
            }
        }
    }
}