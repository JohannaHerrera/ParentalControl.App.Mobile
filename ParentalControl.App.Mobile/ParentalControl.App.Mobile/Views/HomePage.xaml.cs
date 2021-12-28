using ParentalControl.App.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParentalControl.App.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            LoadInformation();
        }

        public async void LoadInformation()
        {
            var response = await new NewsService().GetNews();

            if (response != null)
            {
                if (!string.IsNullOrEmpty(response.MessageError))
                {
                    _ = DisplayAlert("Error", response.MessageError, "OK");
                    _ = Navigation.PushAsync(new HomePage());
                }
                else
                {
                    if (response.NewsModelList.Count() > 0)
                    {
                        int rowCount = 0;

                        foreach (var news in response.NewsModelList)
                        {
                            //Frame frame = new Frame
                            //{
                            //    BorderColor = Color.FromHex("#436D93")
                            //};

                            var formattedString = new FormattedString();
                            formattedString.Spans.Add(new Span { Text = $"{news.NewsTitle}: ", FontAttributes = FontAttributes.Bold, TextColor = Color.FromHex("#0d6efd") });
                            formattedString.Spans.Add(new Span { Text = $"{news.NewsDescription}", FontAttributes = FontAttributes.Bold });
                            var span = new Span { Text = " Leer más", TextColor = Color.FromHex("#0d6efd") };
                            span.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command((url) => OpenLink(news.NewsLink)) });
                            formattedString.Spans.Add(span);

                            tblNews.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                            tblNews.Children.Add(new Label { FormattedText = formattedString}, 0, rowCount);

                            rowCount++;

                            tblNews.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                            tblNews.Children.Add(new Label { HeightRequest = 1, BackgroundColor = Color.FromHex("#13B57E") }, 0, rowCount);

                            rowCount++;
                        }
                    }
                    else
                    {
                        _ = DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
                        _ = Navigation.PushAsync(new HomePage());
                    }
                }
            }
            else
            {
                _ = DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
                _ = Navigation.PushAsync(new HomePage());
            }
        }

        private void OpenLink(string url)
        {
            var uri = new Uri(url);
            Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
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