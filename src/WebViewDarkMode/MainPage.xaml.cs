using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WebViewDarkMode
{
    public partial class MainPage : ContentPage
    {
        private string _html;

        public MainPage()
        {
            InitializeComponent();

            Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
        }

        private async void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            await SetThemeAndLoadSource();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await SetThemeAndLoadSource();
        }


        private async Task SetThemeAndLoadSource()
        {
            _html = await LoadHtmlFromFileAsync();

            _html = Application.Current.RequestedTheme == OSAppTheme.Dark ?
                    _html.Replace("light", "dark") :
                    _html.Replace("dark", "light");

            this.TestWebView.Source = new HtmlWebViewSource() { Html = _html };
        }

        public async Task<string> LoadHtmlFromFileAsync()
        {
            var assembly = GetType().Assembly;
            using var stream = assembly.GetManifestResourceStream("WebViewDarkMode.LoremIpsum.html");
            using var reader = new StreamReader(stream);

            return await reader.ReadToEndAsync();
        }


    }
}
