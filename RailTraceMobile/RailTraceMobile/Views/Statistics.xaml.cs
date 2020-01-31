
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;
namespace RailTraceMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Statistics : ContentPage
	{
        int connection_retries = 3;
        CookieContainer cc = new CookieContainer();
        string proxy = "";




        public string GET_action(string URL)
        {
            var loopTo = connection_retries;
            for (var i = 1; i <= loopTo; i++)
            {
                try
                {
                    System.Net.HttpWebRequest req = (HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                    req.CookieContainer = cc;
                    {
                        var withBlock = req;

                        withBlock.Timeout = 7000;
                        req.AutomaticDecompression = DecompressionMethods.GZip;
                        withBlock.Headers["X-API-Key"] = "1d384058bbfd4362b8a8de089b677b54";
                        withBlock.Accept = "application/json";
                    }
                    if (proxy.Length > 1)
                    {
                        System.Net.WebProxy myProxy = new System.Net.WebProxy(proxy);
                        req.Proxy = myProxy;
                    }
                    System.Net.HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    System.IO.StreamReader strm = new System.IO.StreamReader(res.GetResponseStream());
                    string html = strm.ReadToEnd();
                    strm.Dispose();
                    res.Dispose();
                    return html;
                }
                catch (System.Exception ex)
                {

                    Thread.Sleep(300);
                }
            }
            return "error";
        }


        void init_graph()
        {
            string s = GET_action("https://liveobjects.orange-business.com/api/v0/data/streams/metrou2");
            double[] rotatie_Y = new double[100];
            double[] rotatie_Z = new double[100];
            double[] timp_curent = new double[100];

            s = s.Replace("}, {", "~");
            string[] sp = s.Split('~');
            int i;
            for (i = 0; i < sp.Count(); i++)
            {
                Match m1 = Regex.Match(sp[i], "timp_curent.*");
                Match m2 = Regex.Match(sp[i], "rotatie_y.*");
                Match m3 = Regex.Match(sp[i], "rotatie_z.*");
                string[] sp2T = m1.Value.Replace(',', '\0').Split(':');
                string[] sp2Y = m2.Value.Replace(',', '\0').Split(':');
                string[] sp2Z = m3.Value.Replace(',', '\0').Split(':');
                rotatie_Y[i] = double.Parse(sp2Y[1]);
                rotatie_Z[i] = double.Parse(sp2Z[1]);
                timp_curent[i] = double.Parse(sp2T[1]);
            }

            // reprezentare grafice

            var entries1 = new Entry[rotatie_Y.Length];
            var entries2 = new Entry[rotatie_Z.Length];
            for (i = 0; i < rotatie_Y.Length; i++)
            {
                entries1[i] = new Entry((float)rotatie_Y[i])
                {
                    Label = timp_curent[i].ToString(),
                    ValueLabel = rotatie_Y[i].ToString(),
                    Color = SKColor.Parse("#b455b6")
                };

                entries2[i] = new Entry((float)rotatie_Y[i])
                {
                    Label = timp_curent[i].ToString(),
                    ValueLabel = rotatie_Y[i].ToString(),
                    Color = SKColor.Parse("#b455b6")
                };
            }

            var chart1 = new LineChart() { Entries = entries1 };
            var chart2 = new LineChart() { Entries = entries2 };
            //this.chartView1.Chart = chart1;
            //this.chartView2.Chart = chart2;
        }
    
    public Statistics ()
		{
            init_graph();

            InitializeComponent ();
		}
	}
}