using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RailTraceMobile.Models;
namespace RailTraceMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewTrans : ContentPage
    {
        public ListViewTrans()
        {
            InitializeComponent();
            SqlHelper1 sq = new SqlHelper1();
            IEnumerable<RegEntity1> l = sq.GetItems();
            List<string> list = new List<string>();
            foreach (var value in l)
            {
                string user = value.Username;
                string pass = value.Password;
                list.Add(user + "     " + pass);
            }

            if (list.Count == 0)
            {
                Device.OpenUri(new Uri("https://app.powerbi.com/favorites"));
            }

            var lst = new ListView
            {
                ItemsSource = list
            };
        }
    }
}