using SimpleNLG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
//using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RailTraceMobile.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using System.Windows.Media.Capture;
//using System.Windows.Storage;
using System;
using System;
using System.Drawing;

using System.Threading;

using System.Collections.Generic;

using System.Threading.Tasks;

using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Intent;

using System.Windows.Input;
//using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
//using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;

using System.Net.Http;
using System.Threading;

using System.Net;
using System.Text.RegularExpressions;
using System;
using System.Windows;

using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Google.Cloud.Speech.V1;

namespace RailTraceMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Admin : ContentPage
	{
        App p;
        static string speech_recorded = "";
        public Admin ()
		{
			InitializeComponent ();
		}

        public Admin (App p)
        {
            this.p = p;
            InitializeComponent();
        }

        public void Tasks (object sender, EventArgs e)
        {
            // Deschidem o pagina pentru a incarca si a analiza o imagine de pe buletin
            //p.MainPage = new PhotoAnalysis(p);
            //p.MainPage = new Statistics();
            Application.Current.MainPage.Navigation.PushAsync(new ToDoList());
        }
        
        public void DisplayMap(object sender, EventArgs e)
        {
            // Metoda care sa deschida o pagina in care sa poti vedea harta unei locatii selectate

            Device.OpenUri(new Uri(@"https://app.powerbi.com/groups/me/reports/17d69a1e-a587-4a8c-9cf7-379daa28706f/ReportSection"));

        }

        public void ShowStatistics (object sender, EventArgs e)
        {
            // Afisam dashboard-ul din Power BI
            Device.OpenUri(new Uri("https://app.powerbi.com/groups/me/reports/72af55e1-be90-440d-84e7-1866ec4bb489/ReportSection45f4794186a4db99844f"));
        }

        public void DisplayBot(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(@"https://alexandrachiritaacs.github.io/"));

        }

        public void AzureProfileEdit (object sender, EventArgs e)
        {
            // Metoda care deschide pagina de profil a angajatului (isi poate seta situatia - busy, available, on holiday etc.)
            //Application.Current.MainPage.Navigation.PushAsync(new Webi());
            Device.OpenUri(new Uri(@"https://portal.azure.com/#@cti.pub.ro/dashboard/arm/subscriptions/3f5bc1d7-1959-4d7c-b439-5a1328078c32/resourcegroups/dashboards/providers/microsoft.portal/dashboards/0d546612-339a-4ac6-9549-46b0b5d425a1"));   
        }

        public void ScheduleFunctions(object sender, EventArgs e)
        {
            // Deschizi dashboard-ul de la accelo
            Device.OpenUri(new Uri(@"https://compilerspeed.accelo.com/?action=schedule_view&id=2"));
        }

        public static void RecSpeech()
        {
            try
            {
                var speech = SpeechClient.Create();
                var config = new RecognitionConfig
                {
                    Encoding = RecognitionConfig.Types.AudioEncoding.Flac,
                    SampleRateHertz = 16000,
                    LanguageCode = LanguageCodes.English.UnitedStates
                };

                var audio = RecognitionAudio.FromStorageUri("gs://cloud-samples-tests/speech/brooklyn.flac");
                var response = speech.Recognize(config, audio);

                string[] vect = new string[100];
                int i = 0;
                foreach (var result in response.Results)
                {
                    foreach (var alternative in result.Alternatives)
                    {
                        vect[i++] = alternative.Transcript;
                    }
                }

                for (int j = 0; j < i; j++)
                {
                    speech_recorded = speech_recorded + vect[j];
                }
            }
            catch
            {
                speech_recorded = "power";
            }
        }

        protected static void SendEmail(string note)
        {
            //gather email from form textbox
            string remail = "chirita.alexandra.acs@gmail.com";

            MailAddress from = new MailAddress("chirita.alexandra.acs@gmail.com");
            MailAddress to = new MailAddress(remail);
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Notification from Advisor";

            message.Body = note;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("chirita.alexandra.acs@gmail.com", "iesiafaradeaici");

            try
            {
                client.Send(message);
            }
            catch
            {
                //error message?
            }

            finally
            {

            }
        }

        static async Task RecognizeIntentAsync()
        {

            var config = SpeechConfig.FromSubscription("e074bf56d56e4347bb69997dd5493456", @"westus");

            // Creates a speech recognizer using microphone as audio input.
            using (var recognizer = new SpeechRecognizer(config))
            {
                // Starts recognizing

                // Performs recognition. RecognizeOnceAsync() returns when the first utterance has been recognized,
                // so it is suitable only for single shot recognition like command or query. For long-running
                // recognition, use StartContinuousRecognitionAsync() instead.
                var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

                // Checks result.
                if (result.Reason == ResultReason.RecognizedSpeech)
                {

                    speech_recorded = result.Text;
                }
                else if (result.Reason == ResultReason.NoMatch)
                {

                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(result);


                    if (cancellation.Reason == CancellationReason.Error)
                    {

                    }
                }
            }
        }

        public void AdvisorFunctions(object sender, EventArgs e)
        {
            //SendEmail("Hello");
            AdvisorAnalysis ad = new AdvisorAnalysis();
            ad.GenerateSentenceAsync();

        }

        public void CloudFunctions(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(@"https://cloud4rpi.io/control-panels/d3d9a630-484a-486e-aced-082850a6203d"));
        }

        

        public void VoiceFunctions(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(3000);
                speech_recorded = "power";
                if (speech_recorded.Contains("power") || speech_recorded.Contains("bi"))
                {
                    Device.OpenUri(new Uri("https://app.powerbi.com/groups/me/reports/72af55e1-be90-440d-84e7-1866ec4bb489/ReportSection45f4794186a4db99844f"));
                }

                else if (speech_recorded.Contains("chat"))
                {
                    // Deschizi chat_botul
                }

                else if (speech_recorded.Contains("transaction"))
                {
                    // Deschizi o tranzactie noua
                }

                else if (speech_recorded.Contains("show"))
                {
                    // Afisezi tranzactiile curente
                }

                else if (speech_recorded.Contains("cloud"))
                {
                    Device.OpenUri(new Uri(@"https://cloud4rpi.io/control-panels/d3d9a630-484a-486e-aced-082850a6203d"));
                }

                else if (speech_recorded.Contains("schedule"))
                {
                    Device.OpenUri(new Uri(@"https://compilerspeed.accelo.com/?action=schedule_view&id=2"));
                }

                else if (speech_recorded.Contains("azure") || speech_recorded.Contains("dashboard"))
                {
                    Device.OpenUri(new Uri(@"https://portal.azure.com/#@cti.pub.ro/dashboard/arm/subscriptions/3f5bc1d7-1959-4d7c-b439-5a1328078c32/resourcegroups/dashboards/providers/microsoft.portal/dashboards/0d546612-339a-4ac6-9549-46b0b5d425a1"));
                }

                else
                {
                    DisplayAlert("Error", "Command not recognised", "Ok");
                }
            }

            catch (Exception err)
            {
                DisplayAlert("Error", err.ToString(), "Ok");
            }
        }

        public void TransFunctions(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new Transaction());
        }
    }
}