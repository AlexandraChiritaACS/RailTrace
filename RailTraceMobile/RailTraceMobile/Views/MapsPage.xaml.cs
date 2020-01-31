using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RailTraceMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapsPage : ContentPage
    {
        public MapsPage()
        {
            InitializeComponent();
            var l = new Label
            {
                Text = "These buttons leave the current app and open the built-in Maps app for the platform"
            };

            var openLocation = new Button
            {
                Text = "Open location using built-in Maps app"
            };
            openLocation.Clicked += (sender, e) => {

                if (Device.RuntimePlatform == Device.iOS)
                {
                    Device.OpenUri(new Uri("http://maps.apple.com/?q=394+Pacific+Ave+San+Francisco+CA"));
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    Device.OpenUri(new Uri(@"https://www.google.com/maps/place/RATB+Recharge+Centre/@44.4827375,25.9770182,12z/data=!4m8!1m2!2m1!1ssediu+ratb+!3m4!1s0x0:0xe56ffffb6e192a31!8m2!3d44.4822541!4d26.0480475"));

                }
                else if (Device.RuntimePlatform == Device.UWP)
                {
                    Device.OpenUri(new Uri("bingmaps:?where=394 Pacific Ave San Francisco CA"));
                }
            };

            var openDirections = new Button
            {
                Text = "Get directions using built-in Maps app"
            };
            openDirections.Clicked += (sender, e) => {

                if (Device.RuntimePlatform == Device.iOS)
                {

                    Device.OpenUri(new Uri("http://maps.apple.com/?daddr=San+Francisco,+CA&saddr=cupertino"));

                }
                else if (Device.RuntimePlatform == Device.Android)
                {

                    Device.OpenUri(new Uri("https://www.google.com/maps/place/RATB+Recharge+Centre/@44.4827375,25.9770182,12z/data=!4m8!1m2!2m1!1ssediu+ratb+!3m4!1s0x0:0xe56ffffb6e192a31!8m2!3d44.4822541!4d26.0480475"));

                }
                else if (Device.RuntimePlatform == Device.UWP)
                {
                    Device.OpenUri(new Uri("bingmaps:?rtp=adr.394 Pacific Ave San Francisco CA~adr.One Microsoft Way Redmond WA 98052"));
                }
            };
            Content = new StackLayout
            {
                Padding = new Thickness(5, 20, 5, 0),
                HorizontalOptions = LayoutOptions.Fill,
                Children = {
                    l,
                    openLocation,
                    openDirections
                }
            };
        }
    }
}