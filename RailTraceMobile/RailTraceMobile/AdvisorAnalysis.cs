using SimpleNLG;
using System;
using System.Collections.Generic;
using System.Text;
using SimpleNLG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
//using Xamarin.Essentials;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Messaging;

namespace RailTraceMobile
{
    class LinearRegression
    {
        double[] thetaVector = new double[] { 0.094579, 0.986822 };

        public Double apply(Double[] featureVector)
        {
            // for computational reasons the first element has to be 1.0
            featureVector[0] = 1.0;
            // simple, sequential implementation
            double prediction = 0;
            for (int j = 0; j < thetaVector.Length; j++)
            {
                prediction += thetaVector[j] * featureVector[j];
            }
            return prediction;
        }

        public Boolean checkIfNeededNotificationToAlert(double[] thetaVector, double[] featureVector, double currentAmount)
        {
            double sum = 0.0;

            for (int i = 0; i < featureVector.Length; i++)
            {
                sum = sum + featureVector[i];
            }

            sum = sum / featureVector.Length;
            double percent = 100 * currentAmount / sum;
            if (percent > thetaVector[1] && (1 - thetaVector[0]) < percent)
            {
                return true;
            }

            return false;
        }
    }

        class AdvisorAnalysis
    {
        private static double[] ef = new double[] { 160, 150, 170, 150, 130, 210, 190, 170, 220, 230, 180, 260, 160, 160, 160, 160 };
        private static double[] prog = new double[] {154.90, 181.77, 169.30, 181.60, 159.19, 186.77, 173.93, 186.53, 163.49, 191.78, 178.56, 191.46, 167.78, 196.78, 183.19, 196.39, 172.07, 201.78, 187.81, 201.33 };
        private static double[] tt = new double[] { 170.68, 171.87, 173.05, 174.23, 175.41, 176.60, 177.78, 178.96, 180.15, 181.33, 182.51, 183.69, 184.88, 186.06, 187.24, 188.42, 189.61, 190.79, 191.97, 193.15 };

        public static double checkEfficiency ()
        {
            LinearRegression targetFunction = new LinearRegression();
            double predictedEfficiency = targetFunction.apply(ef);
            

            return predictedEfficiency;
        }

        public static double checkPrognisis()
        {
            LinearRegression targetFunction = new LinearRegression();
            double predicted = targetFunction.apply(prog);
            return predicted;
        }

        public static double checkTT()
        {
            LinearRegression targetFunction = new LinearRegression();
            double predictedtt = targetFunction.apply(tt);
            return predictedtt;
        }

        public static Boolean getEf ()
        {
            double avgef = average(ef);
            double ef1 = checkEfficiency();
            if (ef1 > avgef)
            {
                return true;
            }

            return false;
        }

        public static Boolean getProg()
        {
            double avgef = average(prog);
            double ef1 = checkEfficiency();
            if (ef1 > avgef)
            {
                return true;
            }

            return false;
        }

        public static Boolean getTT()
        {
            double avgef = average(tt);
            double ef1 = checkEfficiency();
            if (ef1 > avgef)
            {
                return true;
            }

            return false;
        }

        public static double average(double[]v)
        {
            double avg = 0.0;
            for (int i = 0; i < v.Length; i++)
            {
                avg = avg + v[i];
            }

            return avg / v.Length;
        }

        public static int NoTrimestersUntilFaliment()
        {
            double avg = average(tt);
            double progtt = checkTT();
            double parameter = (avg - progtt);
            return (int)(progtt/parameter);
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

        public async Task SendEmail1(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
                
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
        }

        public static void SendE(string note)
        {
            var emailTask = CrossMessaging.Current.EmailMessenger;
            if (emailTask.CanSendEmail)
            {
                // Send simple e-mail to single receiver without attachments, CC, or BCC.
                emailTask.SendEmail("plugins@xamarin.com", "Xamarin Messaging Plugin", "Hello from your friends at Xamarin!");

                // Send a more complex email with the EmailMessageBuilder fluent interface.
                var email = new EmailMessageBuilder()
                  .To("chirita.alexandra.acs@gmail.com")
                  .Cc("plugins.cc@xamarin.com")
                  .Bcc(new[] { "plugins.bcc@xamarin.com", "plugins.bcc2@xamarin.com" })
                  .Subject("Advisor")
                  .Body(note)
                  .Build();

                emailTask.SendEmail(email);
            }

        }

        public static void SMS(string content)
        {
            var smsMessenger = CrossMessaging.Current.SmsMessenger;
            if (smsMessenger.CanSendSms)
            {
                smsMessenger.SendSms("0771691920", content);
                smsMessenger.SendSms("0756821850", content);
            }
        }

        public static void SMS1(string con)
        {
            SmsManager sms = SmsManager.Default;
            sms.SendTextMessage("0771691920", null, con, null, null);
        }

        public async Task GenerateSentenceAsync()
        {
            // Instructions will be given to you by the director.
            // Verificam parametrul eficienta:
            Boolean effbool = getEf();
            //Boolean progbool = getProg();
            Boolean ttbool = getTT();
            double eff1 = checkEfficiency();
            double prog1 = checkPrognisis();
            double tt1 = checkTT();
            int tri = NoTrimestersUntilFaliment();
            string s1 = "";
            string s2 = "";
            string s3 = "";

            if (effbool = true)
            {
                // Introduction message
                var ss = new XMLLexicon();
                var Factory = new NLGFactory(ss);
                var Realiser = new Realiser(ss);

                var verbp = Factory.createVerbPhrase("prove");
                verbp.setFeature(Feature.TENSE.ToString(), Tense.FUTURE);
                var subj = Factory.createNounPhrase("The Business");
                var oobj = Factory.createNounPhrase("RailTrace");
                var ioobj = Factory.createNounPhrase("surprisingly productive");
                subj.setPlural(false);
                oobj.setPlural(true);

                var s = new List<INLGElement>() { verbp, subj, oobj, ioobj };

                var clause = Factory.createClause();

                clause.setVerb(verbp);
                clause.setSubject(subj);
                clause.setObject(oobj);
                clause.setIndirectObject(ioobj);

                var sentence = Factory.createSentence(clause);
                sentence.setFeature(Feature.TENSE.ToString(), Tense.FUTURE);

                s1 = Realiser.realise(sentence).ToString() + "Efficiency parameter: " + eff1 + "\n";
                
            }

            else if (effbool == false)
            {
                // Introduction message
                var ss = new XMLLexicon();
                var Factory = new NLGFactory(ss);
                var Realiser = new Realiser(ss);

                var verbp = Factory.createVerbPhrase("prove");
                verbp.setFeature(Feature.TENSE.ToString(), Tense.FUTURE);
                var subj = Factory.createNounPhrase("The Business");
                var oobj = Factory.createNounPhrase("RailTrace");
                var ioobj = Factory.createNounPhrase("surprisingly unproductive");
                subj.setPlural(false);
                oobj.setPlural(true);

                var s = new List<INLGElement>() { verbp, subj, oobj, ioobj };

                var clause = Factory.createClause();

                clause.setVerb(verbp);
                clause.setSubject(subj);
                clause.setObject(oobj);
                clause.setIndirectObject(ioobj);

                var sentence = Factory.createSentence(clause);
                sentence.setFeature(Feature.TENSE.ToString(), Tense.FUTURE);

                s1 = Realiser.realise(sentence).ToString() + "Efficiency parameter: " + eff1 + "\n";

            }

            // Add prognosis

            s2 = "Prognosis for next trimester: " + prog1 + "\n";

            if (ttbool == true)
            {
                // Introduction message
                var ss = new XMLLexicon();
                var Factory = new NLGFactory(ss);
                var Realiser = new Realiser(ss);

                var verbp = Factory.createVerbPhrase("is");
                verbp.setFeature(Feature.TENSE.ToString(), Tense.FUTURE);
                var subj = Factory.createNounPhrase("The Business");
                var oobj = Factory.createNounPhrase("RailTrace");
                var ioobj = Factory.createNounPhrase("in evolution when it comes to profit");
                subj.setPlural(false);
                oobj.setPlural(true);

                var s = new List<INLGElement>() { verbp, subj, oobj, ioobj };

                var clause = Factory.createClause();

                clause.setVerb(verbp);
                clause.setSubject(subj);
                clause.setObject(oobj);
                clause.setIndirectObject(ioobj);

                var sentence = Factory.createSentence(clause);
                sentence.setFeature(Feature.TENSE.ToString(), Tense.FUTURE);

                s3 = Realiser.realise(sentence).ToString() + "Train parameter: " + tt1 + "\n";
            }

            else if (ttbool == true)
            {
                // Introduction message
                var ss = new XMLLexicon();
                var Factory = new NLGFactory(ss);
                var Realiser = new Realiser(ss);

                var verbp = Factory.createVerbPhrase("is");
                verbp.setFeature(Feature.TENSE.ToString(), Tense.FUTURE);
                var subj = Factory.createNounPhrase("The Business");
                var oobj = Factory.createNounPhrase("RailTrace");
                var ioobj = Factory.createNounPhrase("decreasing when it comes to profit");
                subj.setPlural(false);
                oobj.setPlural(true);

                var s = new List<INLGElement>() { verbp, subj, oobj, ioobj };

                var clause = Factory.createClause();

                clause.setVerb(verbp);
                clause.setSubject(subj);
                clause.setObject(oobj);
                clause.setIndirectObject(ioobj);

                var sentence = Factory.createSentence(clause);
                sentence.setFeature(Feature.TENSE.ToString(), Tense.FUTURE);

                s3 = Realiser.realise(sentence).ToString() + "Train parameter: " + tt1 + "\n" + "The business is predicted to bankruptcy in " + tri + "trimesters\n";
            }

            SendEmail(s1 + s2 + s3);
            List<string> l = new List<string>();
            l.Add("chirita.alexandra.acs@gmail.com");
            //SendEmail1("RailTrace Advisor", s1 + s2 + s3, l);
            SMS1(s1 + s2 + s3);
            //SendE(s1 + s2 + s3);


}

    }
}
