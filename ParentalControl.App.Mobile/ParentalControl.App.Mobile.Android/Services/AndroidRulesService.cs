using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ParentalControl.App.Mobile.Droid.Helpers;
using ParentalControl.App.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ParentalControl.App.Mobile.Droid.Services
{
    [Service]
    public class AndroidRulesService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public const int ServiceRunningNotifID = 9000;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Notification notif = DependencyService.Get<INotification>().ReturnNotif();
            StartForeground(ServiceRunningNotifID, notif);

            Task.Run(() =>
            {
                try
                {
                    var appRules = new ApplyInfantRulesService();
                    appRules.BlockApps().Wait();
                }
                catch (Exception ex)
                {

                }
            });

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override bool StopService(Intent name)
        {
            return base.StopService(name);
        }
    }
}