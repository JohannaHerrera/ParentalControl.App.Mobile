using Android.App;
using Android.App.Usage;
using Android.Content;
using Android.Content.PM;
using Java.IO;
using Java.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Android.App.ActivityManager;

namespace ParentalControl.App.Mobile.Services
{
    public class ApplyInfantRulesService
    {
        private bool flag = true;

        public ApplyInfantRulesService()
        {

        }

        public async Task BlockApps()
        {
            await Task.Run(async () =>
            {
                try
                {
                    while (flag)
                    {
                        await Task.Delay(5000);

                        Debug.WriteLine("Prueba");

                        var apps = Android.App.Application.Context.PackageManager.GetInstalledApplications(PackageInfoFlags.MatchAll);
                        ActivityManager amg = (ActivityManager)Android.App.Application.Context.GetSystemService(Context.ActivityService);
                        UsageStatsManager m = (UsageStatsManager)Android.App.Application.Context.GetSystemService(Context.UsageStatsService);

                        Android.App.Usage.UsageStatsInterval usageStatsInterval = Android.App.Usage.UsageStatsInterval.Daily;
                        var stats = m.QueryUsageStats(usageStatsInterval, 100, 100);

                        string nombre = string.Empty;

                        foreach (var app in apps)
                        {
                            string name = app.LoadLabel(Android.App.Application.Context.PackageManager);

                            if (name.ToLower().Contains("youtube"))
                            {
                                //Intent startMain = new Intent(Intent.ActionMain);
                                //startMain.AddCategory(Intent.CategoryHome);
                                //startMain.SetFlags(ActivityFlags.NewTask);
                                //Android.App.Application.Context.StartActivity(startMain);

                                nombre = app.ProcessName.ToLower();                                

                                amg.KillBackgroundProcesses(app.PackageName);
                                //amg.RestartPackage(app.PackageName);

                                //Java.Lang.Process process = Java.Lang.Runtime.GetRuntime()?.Exec($"adb shell -> ps -A | grep {app.PackageName}");
                                //var result = process?.WaitFor();

                                bool result = m.IsAppInactive(app.PackageName);

                                Android.OS.Process.KillProcess(app.Uid);
                            }
                        }

                        foreach (var app in amg.RunningAppProcesses)
                        {
                            if (app.ProcessName.ToLower().Contains("contacts"))
                            {
                                Android.OS.Process.KillProcess(app.Pid);
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
