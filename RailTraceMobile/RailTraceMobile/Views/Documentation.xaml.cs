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
    public partial class Documentation : ContentPage
    {
        public Documentation()
        {
            InitializeComponent();
            Device.OpenUri(new Uri("https://docs.google.com/document/d/1HSQ3Fe77hnthw8hizqvXJU-qGEPHavMkctvCCadkVbY/edit?pli=1&fbclid=IwAR0QWKgQauiMqCTBvsCcwO3wrs5tX6s7Fp5_-Wta_AdyUlEVigOvcSyCB-4"));
        }
    }
}