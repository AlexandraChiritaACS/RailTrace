using RailTraceMobile.Views;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RailTraceMobile
{
    public partial class App : Application
    {
        public static string DefaultImageId = "default_image";
        public static string ImageIdToSave = null;
        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new LoginPage(this));
           
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
