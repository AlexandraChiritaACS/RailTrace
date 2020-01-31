using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.AppCenter;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using SQLite;
using System.IO;
using RailTraceMobile.Models;

namespace RailTraceMobile.Droid
{
    [Activity(Label = "RailTraceMobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CreateDB();
            

            LoadApplication(new App());
           
        }

        public string CreateDB()
        {
            var output = "";
            output += "Creating Databse if it doesnt exists";
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Employee.db3"); //Create New Database  
            var db = new SQLiteConnection(dpPath);
            db.CreateTable<RegEntity>();
            RegEntity r1 = new RegEntity
            {
                Username = "Alexandra",
                Password = "camin",
                PhoneNumber = "0771691920"
            };

            RegEntity r2 = new RegEntity
            {
                Username = "Florin",
                Password = "biscuiti",
                PhoneNumber = "0756821850"
            };

            db.Insert(r1);
            db.Insert(r2);

            output += "\n Database Created....";
            return output;
        }

        public string CreateDB1()
        {
            var output = "";
            output += "Creating Databse if it doesnt exists";
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Transaction.db3"); //Create New Database  
            var db = new SQLiteConnection(dpPath);
            db.CreateTable<RegEntity1>();
          
            output += "\n Database Created....";
            return output;
        }
    }
}