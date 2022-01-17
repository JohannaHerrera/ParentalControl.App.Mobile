using Android.App;
using Android.App.Usage;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Widget;
using Java.IO;
using Java.Util;
using ParentalControl.App.Mobile.Views;
using Plugin.DeviceInfo;
using Plugin.LocalNotification;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

                        BlockApps();

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                return;
            });
        }

        public void BlockApps()
        {
            Task.Run(async () =>
            {
                try
                {                 
                    ActivityManager amg = (ActivityManager)Android.App.Application.Context.GetSystemService(Context.ActivityService);

                    #region Código de Prueba
                    /*var apps = Android.App.Application.Context.PackageManager.GetInstalledApplications(PackageInfoFlags.MatchAll);
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
                    }*/
                    #endregion Código de Prueba

                    string deviceCode = CrossDeviceInfo.Current.Id;
                    var response = await new GetDeviceConfigurationService().GetDeviceConfiguration(deviceCode);
                    
                    if(response != null)
                    {
                        if(response.AppsLocked != null)
                        {
                            if(response.AppsLocked.Count > 0)
                            {
                                var apps = Android.App.Application.Context.PackageManager.GetInstalledApplications(PackageInfoFlags.MatchAll);

                                foreach (var appLock in response.AppsLocked)
                                {
                                    string processName = string.Empty;

                                    foreach (var appItem in apps)
                                    {
                                        string name = appItem.LoadLabel(Android.App.Application.Context.PackageManager);

                                        if (name.ToLower().Contains(appLock.AppName.ToLower()))
                                        {
                                            processName = appItem.ProcessName;
                                            break;
                                        }
                                    }

                                    foreach (var app in amg.RunningAppProcesses)
                                    {
                                        if (app.ProcessName.ToLower().Contains(processName.ToLower()))
                                        {
                                            Intent startMain = new Intent(Intent.ActionMain);
                                            startMain.AddCategory(Intent.CategoryHome);
                                            startMain.SetFlags(ActivityFlags.NewTask);
                                            Android.App.Application.Context.StartActivity(startMain);

                                            Android.OS.Process.SendSignal(app.Pid, Android.OS.Signal.Kill);
                                            amg.KillBackgroundProcesses(app.PkgList[0]);
                                            Android.OS.Process.KillProcess(app.Pid);

                                            var notification = new NotificationRequest
                                            {
                                                NotificationId = 100,
                                                Title = "¡Atención!",
                                                Description = $"No puedes usar la aplicación {appLock.AppName}",
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
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                return;
            });
        }

    }
}
