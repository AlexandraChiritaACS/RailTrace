//using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.Android.Telephony;
using RailTraceMobile.Models;
using System;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace RailTraceMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Transaction : ContentPage
    {
        static string code;

        public Transaction()
        {
            InitializeComponent();
        }

        public static void SMS1(string con)
        {
            SmsManager sms = SmsManager.Default;
            sms.SendTextMessage("0771691920", null, con, null, null);
        }

        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static void SendCode(object sender, EventArgs e)
        {
            string s = RandomString(6, true);
            SMS1(s);
            code = s;
        }


        public async void FinishProcedure(object sender, EventArgs e)
        {
            string ss = RandomCode.Text;
            User user = new User(Entry_Username.Text, Entry_Password.Text);
            SqlHelper1 sq = new SqlHelper1();
            if (code == ss)
            {
                // Tranzactia se poate efectua cu success
                sq.AddItem(Entry_Username.Text, Entry_Password.Text);
                SMS1("Transaction succeded.");
            }

            else
            {
                // Nu putem efectua tranzactua
                DisplayAlert("Error", "Transacion invalid", "Ok");
            }
        }

        public static void SeeProcedure(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new ListViewTrans());
        }
    }
}