﻿using Android.App;
using Android.App.Usage;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Widget;
using Java.IO;
using Java.Util;
using ParentalControl.App.Mobile.Models;
using ParentalControl.App.Mobile.Views;
using Plugin.DeviceInfo;
using Plugin.LocalNotification;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Android.App.ActivityManager;

namespace ParentalControl.App.Mobile.Services
{
    public class ApplyInfantRulesService
    {
        private bool flag = true;

        public ApplyInfantRulesService()
        {

        }

        public async Task ApplyRules()
        {
            await Task.Run(async () =>
            {
                try
                {
                    while (flag)
                    {
                        await Task.Delay(5000);

                        //await BlockApps();
                        DeviceUse();

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                return;
            });
        }

        public async Task BlockApps()
        {
            await Task.Run(() =>
            {
                try
                {
                    var apps = Android.App.Application.Context.PackageManager.GetInstalledApplications(PackageInfoFlags.MatchAll);
                    ActivityManager amg = (ActivityManager)Android.App.Application.Context.GetSystemService(Context.ActivityService);
                    UsageStatsManager m = (UsageStatsManager)Android.App.Application.Context.GetSystemService(Context.UsageStatsService);

                    Android.App.Usage.UsageStatsInterval usageStatsInterval = Android.App.Usage.UsageStatsInterval.Daily;
                    var stats = m.QueryUsageStats(usageStatsInterval, 100, 100);

                    string nombre = string.Empty;
                    string pkgName = string.Empty;

                    foreach (var app in apps)
                    {
                        string name = app.LoadLabel(Android.App.Application.Context.PackageManager);

                        if (name.ToLower().Contains("youtube"))
                        {
                            Intent startMain = new Intent(Intent.ActionMain);
                            startMain.AddCategory(Intent.CategoryHome);
                            startMain.SetFlags(ActivityFlags.NewTask);
                            Android.App.Application.Context.StartActivity(startMain);

                            //Java.Lang.Process process = Java.Lang.Runtime.GetRuntime()?.Exec($"adb shell -> ps -A | grep {app.PackageName}");
                            //var result = process?.WaitFor();
                            //bool result = m.IsAppInactive(app.PackageName);
                            //amg.RestartPackage(app.PackageName);

                            nombre = app.ProcessName.ToLower();

                            
                            amg.KillBackgroundProcesses(app.PackageName);
                            Android.OS.Process.KillProcess(app.Uid);


                            var notification = new NotificationRequest
                            {
                                NotificationId = 100,
                                Title = "¡Atención!",
                                Description = $"No puedes usar la aplicación {name}",
                                ReturningData = "Dummy data", // Returning data when tapped on notification.
                            };

                            NotificationCenter.Current.Show(notification);

                        }                           
                    }

                    foreach (var app in amg.RunningAppProcesses)
                    {
                        if (app.ProcessName.ToLower().Contains("brow"))
                        {
                            Intent startMain = new Intent(Intent.ActionMain);
                            startMain.AddCategory(Intent.CategoryHome);
                            startMain.SetFlags(ActivityFlags.NewTask);
                            Android.App.Application.Context.StartActivity(startMain);

                            Android.OS.Process.SendSignal(app.Pid, Android.OS.Signal.Kill);
                            amg.KillBackgroundProcesses(app.ProcessName);
                            amg.KillBackgroundProcesses(app.PkgList[0]);
                            Android.OS.Process.KillProcess(app.Pid);

                            var notification = new NotificationRequest
                            {
                                NotificationId = 100,
                                Title = "¡Atención!",
                                Description = $"No puedes usar la aplicación {app.ProcessName}",
                                ReturningData = "Dummy data", // Returning data when tapped on notification.
                            };

                            NotificationCenter.Current.Show(notification);
                        }
                    }
                    
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                return;
            });
        }

        public void DeviceUse()
        {
            Task.Run(async () =>
            {
                try
                {
                    string deviceCode = CrossDeviceInfo.Current.Id;
                    var response = await new GetDeviceConfigurationService().GetDeviceConfiguration(deviceCode);
                    DateTime dateValue = DateTime.Now;
                    string dia = dateValue.ToString("dddd", new CultureInfo("es-ES"));

                    if (response != null)
                    {
                        if(response.DeviceConfig != null)
                        {
                            if (response.DeviceConfig.Count > 0 )
                            {
                                foreach (var use in response.DeviceConfig)
                                {

                                    if (use.DevicePhoneUseDay.ToLower().Equals(dia.ToLower()))
                                    {
                                        ScheduleEditFindModel getScheduleInfoModel = new ScheduleEditFindModel();
                                        getScheduleInfoModel.ScheduleId = use.ScheduleId;
                                        var responseSchedule = await new ScheduleService().ScheduleFindEdit(getScheduleInfoModel);
                                        var hola= responseSchedule.ScheduleStartTime;
                                        if (ShowMessageScheduleWithSystemTime(responseSchedule, 10))
                                        {
                                            var notificationPrecaucion = new NotificationRequest
                                            {
                                                NotificationId = 100,
                                                Title = "¡Atención!",
                                                Description = $"En aproximadamente 10 minutos ya no podrá utilizar su dispositivo",
                                                ReturningData = "Dummy data", // Returning data when tapped on notification.
                                            };

                                            await NotificationCenter.Current.Show(notificationPrecaucion);
                                        }
                                        else if (!CompareScheduleWithSystemTime(responseSchedule))
                                        {
                                            Intent startMain = new Intent(Intent.ActionMain);
                                            startMain.AddCategory(Intent.CategoryHome);
                                            startMain.SetFlags(ActivityFlags.NewTask);
                                            Android.App.Application.Context.StartActivity(startMain);

                                            var notification = new NotificationRequest
                                            {
                                                NotificationId = 100,
                                                Title = "¡Atención!",
                                                Description = $"No puedes utilizar el dispositivo en este momento",
                                                ReturningData = "Dummy data", // Returning data when tapped on notification.
                                            };

                                            await NotificationCenter.Current.Show(notification);
                                        }                                 
                                    }
                                }
                            }
                        }
                        
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                return;
            });
        }

        private bool ShowMessageScheduleWithSystemTime(ScheduleResponseModel responseSchedule, int horaMensaje)
        {
            string scheduleEndTime = responseSchedule.ScheduleEndTime;
            DateTime horaFin = DateTime.ParseExact(scheduleEndTime, "HH:mm", null);

            string horaActual = DateTime.Now.ToString("HH:mm");
            DateTime.ParseExact(horaActual, "HH:mm", null);

            DateTime ObtenerHora = horaFin.AddMinutes(-(horaMensaje));

            string HoraAUtilizar = ObtenerHora.ToString("HH:mm");
            string horaFinAux = horaFin.ToString("HH:mm");

            DateTime.ParseExact(horaActual, "HH:mm", null);
            DateTime.ParseExact(HoraAUtilizar, "HH:mm", null);
            DateTime.ParseExact(horaFinAux, "HH:mm", null);

            if (horaActual.CompareTo(HoraAUtilizar) >= 0 && horaActual.CompareTo(horaFinAux) <= 0)
            {
                return true;
            }
            return false;
        }

        private bool CompareScheduleWithSystemTime(ScheduleResponseModel responseSchedule)
        {
            string scheduleEndTime = responseSchedule.ScheduleEndTime;
            string scheduleStartTime = responseSchedule.ScheduleStartTime;
            DateTime horaFin = DateTime.ParseExact(scheduleEndTime, "HH:mm", null);
            DateTime horaInicio = DateTime.ParseExact(scheduleStartTime, "HH:mm", null);

            string horaFinAux = horaFin.ToString("HH:mm");
            string horaInicioAux = horaInicio.ToString("HH:mm");
            string horaActual = DateTime.Now.ToString("HH:mm");

            DateTime.ParseExact(horaActual, "HH:mm", null);
            DateTime.ParseExact(horaFinAux, "HH:mm", null);
            DateTime.ParseExact(horaInicioAux, "HH:mm", null);

            if (horaActual.CompareTo(horaInicioAux) >= 0 && horaActual.CompareTo(horaFinAux) <= 0)
            {
                return true;
            }
            return false;
        }

    }
}
