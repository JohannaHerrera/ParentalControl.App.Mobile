
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Android.Webkit;
using Xamarin.Forms;
//using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ParentalControl.App.Mobile.Models;
using ParentalControl.App.Mobile.Services;
using Xamarin.Essentials;
using Plugin.DeviceInfo;
namespace ParentalControl.App.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlBrowsePage : ContentPage
    {
        public ControlBrowsePage()
        {
            InitializeComponent();
            LoadPage();
        }
        public void LoadPage()
        {

            WebView webView = new WebView();
            pageView.Source = "https://www.google.com/";
    }
        private async void pageView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            //Aqui valido si la url es buena
            string url = e.Url;
            string phoneCode = CrossDeviceInfo.Current.Id;
            string information = url + ";" + phoneCode;
            var response = await new WebConfigurationService().getWebConfiguration(information);
            if (response!=null)
            {
                if (response.IsSuccess==false)
                {                 
                    e.Cancel = true;
                    _ = DisplayAlert("Error", response.MessageError, "OK");
                    _ = Navigation.PushAsync(new InfantPrincipalPage());
                    //DisplayAlert("Alert", response.MessageError, "Ok");
                }
                else
                {
                    if (response.MessageError!=null)
                    {
                        _ = DisplayAlert("Advertencia", response.MessageError, "OK");
                    }                   
                }
            }
            else
            {
                _ = DisplayAlert("Error", "Conexion con el servicio Fallida", "OK");
            }           
        }

        private void pageView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            if (pageView.CanGoBack)
            {
                pageView.GoBack();
            }
            else
            {
                await Navigation.PopAsync();
            }
        }
    }

}