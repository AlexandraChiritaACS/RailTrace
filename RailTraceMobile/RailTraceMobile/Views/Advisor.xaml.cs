using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RailTraceMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Advisor : ContentPage
    {

        public static void ReadExcelFile ()
        {
            string[] allLines = File.ReadAllLines(@"C:\Users\dell\source\repos\RailTraceMobile - Copy\RailTraceMobile\RailTraceMobile\Model.csv");

            var query = from line in allLines
                        let data = line.Split(',')
                        select new
                        {
                            Device = data[0],
                            SignalStrength = data[1],
                            Location = data[2],
                            Time = data[3],
                            Age = Convert.ToInt16(data[4])
                        };
        }

        public Advisor()
        {
            InitializeComponent();
            imgDisp.Source = @"C:\Users\dell\source\repos\RailTraceMobile - Copy\RailTraceMobile\RailTraceMobile\AdvisorImg.png";
            //string s = GenerateSentencesFromData();
            //var underlineLabel = new Label { Text = s};

        }
    }
}