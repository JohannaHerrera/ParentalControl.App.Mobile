using ParentalControl.App.Mobile.Models;
using ParentalControl.App.Mobile.Services;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParentalControl.App.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfantAccountRulesPage : ContentPage
    {
        private int infantIdAux = 0;
        private int deviceId = 0;
        private List<ScheduleModel> schedules = new List<ScheduleModel>();

        public InfantAccountRulesPage(int infantId)
        {
            InitializeComponent();
            LoadInformation(infantId);
            infantIdAux = infantId;
        }

        public async void LoadInformation(int infantId)
        {
            GetInfantAccountInfoModel getInfantAccountInfoModel = new GetInfantAccountInfoModel();
            getInfantAccountInfoModel.ParentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));
            getInfantAccountInfoModel.InfantAccountId = infantId;
            getInfantAccountInfoModel.Action = 1; // Action = 1 (GetDeleteInfantAccount)

            GetDeviceInfoModel getDeviceInfoModel = new GetDeviceInfoModel();
            getDeviceInfoModel.DevicePhoneCode = CrossDeviceInfo.Current.Id;
            getDeviceInfoModel.ParentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));
            getDeviceInfoModel.Action = 1; // Action = 1 (GetDeviceInfo)
            var responseDeviceInfo = await new DeviceService().GetDeviceInfo(getDeviceInfoModel);
            deviceId = responseDeviceInfo.DevicePhoneId;

            ActivityRulesModel activityRulesModel = new ActivityRulesModel();
            AppRulesModel appRulesModel = new AppRulesModel();
            DeviceUseRulesModel deviceUseRulesModel = new DeviceUseRulesModel();
            WebConfigurationRulesModel webConfigurationRulesModel = new WebConfigurationRulesModel();

            GetAppConfigurationModel getAppConfigurationModel = new GetAppConfigurationModel();
            getAppConfigurationModel.InfantAccountId = infantId;
            getAppConfigurationModel.DevicePhoneCode = CrossDeviceInfo.Current.Id;

            var response = await new InfantAccountService().GetDeleteInfantAccount(getInfantAccountInfoModel);
            var responseActivity = await new RulesService().GetActivityRules(getInfantAccountInfoModel.InfantAccountId.ToString());
            var responseApp = await new RulesService().GetAppRules(getAppConfigurationModel);
            var responseDeviceUse = await new RulesService().GetDeviceUseRules(getInfantAccountInfoModel.InfantAccountId.ToString());
            var responseWebConfiguration = await new RulesService().GetWebConfigurationRules(getInfantAccountInfoModel.InfantAccountId.ToString());

            if (response != null && responseActivity != null && responseApp != null && responseDeviceUse != null && responseWebConfiguration != null)
            {
                if (!string.IsNullOrEmpty(response.MessageError))
                {
                    _ = DisplayAlert("Error", response.MessageError, "OK");
                }
                else
                {
                    schedules = responseDeviceInfo.scheduleList;
                    int rowCount = 0;

                    foreach (var use in response.InfantAccountModelList)
                    {
                        if (use.InfantAccountId == infantId)
                        {
                            labelInfantName.Text = use.InfantName;
                        }

                    }
                    if (responseActivity.IsSuccess && responseActivity.activityRulesModelList.Count > 0)
                    {
                        foreach (var use in responseActivity.activityRulesModelList)
                        {
                            tblHistorial.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                            tblHistorial.Children.Add(new Label
                            {
                                Text = use.ActivityDescription,
                                FontSize = 18,
                                TextColor = Color.Black,
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalOptions = LayoutOptions.StartAndExpand
                            }, 0, rowCount);
                            rowCount++;
                        }
                    }
                    else
                    {
                        tblHistorial.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                        tblHistorial.Children.Add(new Label
                        {
                            Text = "Aún no existen registros de actividad.",
                            FontSize = 18,
                            TextColor = Color.Black,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            FontAttributes = FontAttributes.Bold
                        }, 0, rowCount);
                    }
                    //Bloqueo de aplicaciones
                    rowCount = 0;
                    if (responseApp.IsSuccess && responseApp.appRulesModelList.Count > 0)
                    {
                        foreach (var use in responseApp.appRulesModelList)
                        {
                            tblBloqueoAplicaciones.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto});

                            tblBloqueoAplicaciones.Children.Add(new Label
                            {
                                Text = use.AppName,
                                FontSize = 18,
                                TextColor = Color.Black,
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.StartAndExpand,
                                FontAttributes = FontAttributes.Bold
                            }, 0, rowCount);

                            Picker picker = new Picker 
                            {   Title = "Desbloquear/Bloquear", FontSize = 18, 
                                VerticalOptions = LayoutOptions.Center,                                
                            };

                            List<string> blockList = new List<string>()
                            {
                                "Bloquear",
                                "Desbloquear"
                            };

                            foreach (var block in blockList)
                            {
                                picker.Items.Add(block);
                            }

                            if (use.AppAccessPermission == null)
                            {
                                picker.SelectedIndex = 0;
                            }
                            else
                            {
                                int index;
                                if (use.AppAccessPermission == true)
                                {
                                    index = 0;
                                }
                                else
                                {
                                    index = 1;
                                }

                                picker.SelectedIndex = index;
                            }
                            tblBloqueoAplicaciones.Children.Add(picker, 1, rowCount);

                            Picker pickerHorario = new Picker { Title = "Horario", FontSize = 18, VerticalOptions = LayoutOptions.Center };

                            foreach (var schedule in responseDeviceInfo.scheduleList)
                            {
                                pickerHorario.Items.Add(schedule.ScheduleTime);
                            }

                            if (use.ScheduleId == null)
                            {
                                pickerHorario.SelectedIndex = 0;
                            }
                            else
                            {
                                int index = responseDeviceInfo.scheduleList.FindIndex(x => x.ScheduleId == use.ScheduleId);
                                pickerHorario.SelectedIndex = index;
                            }

                            tblBloqueoAplicaciones.Children.Add(pickerHorario, 2, rowCount);
                            rowCount++;
                        }
                    }
                    else
                    {
                        tblBloqueoAplicaciones.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                        tblBloqueoAplicaciones.Children.Add(new Label
                        {
                            Text = "Aún no existen registros de aplicaciones.",
                            FontSize = 18,
                            TextColor = Color.Black,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            FontAttributes = FontAttributes.Bold
                        }, 0, rowCount);
                    }
                    //Uso del Dispositivo
                    rowCount = 0;
                    if (responseDeviceUse.IsSuccess && responseDeviceUse.deviceUseRulesModelList.Count > 0)
                    {
                        foreach (var use in responseDeviceUse.deviceUseRulesModelList)
                        {
                            tblUsoDispositivo.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                            tblUsoDispositivo.Children.Add(new Label
                            {
                                Text = use.DeviceUseDay,
                                FontSize = 18,
                                TextColor = Color.Black,
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.StartAndExpand,
                                FontAttributes = FontAttributes.Bold
                            }, 0, rowCount);

                            Picker pickerHorario = new Picker { Title = "Horario", FontSize = 18, VerticalOptions = LayoutOptions.Center };

                            foreach (var schedule in responseDeviceInfo.scheduleList)
                            {
                                pickerHorario.Items.Add(schedule.ScheduleTime);
                            }

                            if (use.ScheduleId == null)
                            {
                                pickerHorario.SelectedIndex = 0;
                            }
                            else
                            {
                                int index = responseDeviceInfo.scheduleList.FindIndex(x => x.ScheduleId == use.ScheduleId);
                                pickerHorario.SelectedIndex = index;
                            }

                            tblUsoDispositivo.Children.Add(pickerHorario, 1, rowCount);
                            rowCount++;
                        }
                    }
                    else
                    {
                        tblUsoDispositivo.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                        tblUsoDispositivo.Children.Add(new Label
                        {
                            Text = "Aún no existen registros de uso del dispositivo.",
                            FontSize = 18,
                            TextColor = Color.Black,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            FontAttributes = FontAttributes.Bold
                        }, 0, rowCount);
                    }
                    //Bloqueo Web
                    rowCount = 0;
                    if (responseWebConfiguration.IsSuccess && responseWebConfiguration.webConfigurationRulesModelList.Count > 0)
                    {
                        foreach (var use in responseWebConfiguration.webConfigurationRulesModelList)
                        {
                            tblBloqueoWeb.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                            if (use.CategoryId == 1)
                            {
                                tblBloqueoWeb.Children.Add(new Label
                                {
                                    Text = "Drogas",
                                    FontSize = 18,
                                    TextColor = Color.Black,
                                    VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.StartAndExpand,
                                    FontAttributes = FontAttributes.Bold
                                }, 0, rowCount);
                            } 
                            else if (use.CategoryId == 2)
                            {
                                tblBloqueoWeb.Children.Add(new Label
                                {
                                    Text = "Pornografía",
                                    FontSize = 18,
                                    TextColor = Color.Black,
                                    VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    FontAttributes = FontAttributes.Bold
                                }, 0, rowCount);
                            }
                            else if (use.CategoryId == 3)
                            {
                                tblBloqueoWeb.Children.Add(new Label
                                {
                                    Text = "Videojuegos",
                                    FontSize = 18,
                                    TextColor = Color.Black,
                                    VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    FontAttributes = FontAttributes.Bold
                                }, 0, rowCount);
                            }
                            else
                            {
                                tblBloqueoWeb.Children.Add(new Label
                                {
                                    Text = "Violencia",
                                    FontSize = 18,
                                    TextColor = Color.Black,
                                    VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    FontAttributes = FontAttributes.Bold
                                }, 0, rowCount);
                            }
                            
                            Picker pickerCategory = new Picker { Title = "Desbloquear/Bloquear", FontSize = 18, VerticalOptions = LayoutOptions.Center };
                            List<string> bloqueoList = new List<string>()
                            {
                                "Bloquear",
                                "Desbloquear"
                            };

                            foreach (var bloqueo in bloqueoList)
                            {
                                pickerCategory.Items.Add(bloqueo);
                            }

                            if (use.WebConfigurationAccess == null)
                            {
                                pickerCategory.SelectedIndex = 0;
                            }
                            else
                            {
                                int index;
                                if (use.WebConfigurationAccess == true)
                                {
                                    index = 0;
                                }
                                else
                                {
                                    index = 1;
                                }

                                pickerCategory.SelectedIndex = index;
                            }

                            tblBloqueoWeb.Children.Add(pickerCategory, 1, rowCount);
                            rowCount++;
                        }
                    }
                    else
                    {
                        tblBloqueoWeb.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                        tblBloqueoWeb.Children.Add(new Label
                        {
                            Text = "Aún no existen registros de categorías web.",
                            FontSize = 18,
                            TextColor = Color.Black,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            FontAttributes = FontAttributes.Bold
                        }, 0, rowCount);
                    }
                }
            }
            else
            {
                _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                _ = Navigation.PushAsync(new HomePage());
            }

        }

        private void BlockPicker(object sender, EventArgs e)
        {

        }

        private void Back_Clicked(object sender, EventArgs a)
        {
            Navigation.PushAsync(new InfantAccountPage());
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
            _ = DisplayAlert("Aviso", "Cerrando sesión...", "OK");
            _ = Navigation.PushAsync(new LoginPage());
        }

        private async void SaveRulesChanges_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(labelInfantName.Text))
            {
                _ = DisplayAlert("Error", "El nombre del infante no es válido.", "OK");
            }
            else
            {



                List<UpdateAppRulesModel> appRulesModelList = new List<UpdateAppRulesModel>();
                List<UpdateWebConfigurationRulesModel> webConfigurationRulesModelList = new List<UpdateWebConfigurationRulesModel>();
                List<UpdateDeviceUseRulesModel> deviceUseRulesModelList = new List<UpdateDeviceUseRulesModel>();

                int cont = 0;
                foreach (var item in tblBloqueoWeb.Children)
                {
                    UpdateWebConfigurationRulesModel updateWebConfigurationRulesModel = new UpdateWebConfigurationRulesModel();
                    try
                    {
                        var label = item as Label;
                        updateWebConfigurationRulesModel.CategoryName = label.Text;
                        updateWebConfigurationRulesModel.InfantAccountId = infantIdAux;
                        webConfigurationRulesModelList.Add(updateWebConfigurationRulesModel);
                        cont++;
                    }
                    catch (Exception ex)
                    {
                        var picker = item as Picker;
                        var selectedItem = picker.SelectedItem;
                        if (selectedItem.ToString().Equals("Bloquear"))
                        {
                            webConfigurationRulesModelList[cont - 1].WebConfigurationAccess = true;
                        }
                        else
                        {
                            webConfigurationRulesModelList[cont - 1].WebConfigurationAccess = false;
                        }
                    }

                }
                var responseWebConfiguration = await new RulesService().UpdateWebConfigurationRules(webConfigurationRulesModelList);

                cont = 0;
                foreach (var item in tblBloqueoAplicaciones.Children)
                {
                    UpdateAppRulesModel updateAppRulesModel = new UpdateAppRulesModel();
                    try
                    {
                        var label = item as Label;
                        updateAppRulesModel.AppName = label.Text;
                        updateAppRulesModel.DevicePhoneId = deviceId;
                        updateAppRulesModel.InfantAccountId = infantIdAux;
                        appRulesModelList.Add(updateAppRulesModel);
                        cont++;
                    }
                    catch (Exception ex)
                    {
                        var picker = item as Picker;
                        if (picker.Title == "Desbloquear/Bloquear")
                        {
                            var selectedItem = picker.SelectedItem;
                            if (selectedItem.ToString().Equals("Bloquear"))
                            {
                                appRulesModelList[cont - 1].AppAccessPermission = true;
                            }
                            else
                            {
                                appRulesModelList[cont - 1].AppAccessPermission = false;
                            }
                        }
                        else
                        {
                            var selectedItem = picker.SelectedItem;
                            int scheduleId = schedules.Where(x => x.ScheduleTime == selectedItem.ToString()).FirstOrDefault().ScheduleId;
                            appRulesModelList[cont - 1].ScheduleId = scheduleId;
                        }

                    }
                }
                var responseApp = await new RulesService().UpdateAppRules(appRulesModelList);

                cont = 0;
                foreach (var item in tblUsoDispositivo.Children)
                {
                    UpdateDeviceUseRulesModel updateDeviceUseRulesModel = new UpdateDeviceUseRulesModel();
                    try
                    {
                        var label = item as Label;
                        updateDeviceUseRulesModel.DeviceUseDay = label.Text;
                        updateDeviceUseRulesModel.InfantAccountId = infantIdAux;
                        deviceUseRulesModelList.Add(updateDeviceUseRulesModel);
                        cont++;
                    }
                    catch (Exception ex)
                    {
                        var picker = item as Picker;
                        var selectedItem = picker.SelectedItem;
                        int scheduleId = schedules.Where(x => x.ScheduleTime == selectedItem.ToString()).FirstOrDefault().ScheduleId;
                        deviceUseRulesModelList[cont - 1].ScheduleId = scheduleId;
                    }
                }
                var responseDeviceUse = await new RulesService().UpdateDeviceUseRules(deviceUseRulesModelList);

                if (responseDeviceUse && responseApp && responseWebConfiguration)
                {
                    _ = DisplayAlert("Aviso", "La información se actualizó correctamente.", "OK");
                    _ = Navigation.PushAsync(new InfantAccountPage());
                }
                else
                {
                    if(responseDeviceUse && responseWebConfiguration)
                    {
                        _ = DisplayAlert("Aviso", "La información se actualizó correctamente.", "OK");
                        _ = Navigation.PushAsync(new InfantAccountPage());
                    }
                    else
                    {
                        _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                        _ = Navigation.PushAsync(new InfantAccountPage());
                    }
                }

            }
        }
    }
}