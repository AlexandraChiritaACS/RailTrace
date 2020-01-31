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
    public partial class Webi : ContentPage
    {
        public Webi()
        {
            InitializeComponent();
            Device.OpenUri(new Uri("https://industrial.ubidots.com/app/dashboards/5cab23621d84722ff9910ddc"));
        }
    }
}