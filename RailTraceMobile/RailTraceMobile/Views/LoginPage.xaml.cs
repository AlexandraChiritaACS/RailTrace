using RailTraceMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using System.Windows;
using System;
using System.Data.SqlClient;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

namespace RailTraceMobile.Views
{
    public enum FileFormatEnum
    {
        PNG,
        JPEG
    }

    public interface CameraInterface
    {
        void LaunchCamera(FileFormatEnum imageType, string imageId = null);
        void LaunchGallery(FileFormatEnum imageType, string imageId = null);
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class LoginPage : ContentPage
	{
        App p;
        Image img;
        Boolean recognized = true;
        private static bool recognised;
        private readonly IFaceServiceClient faceServiceClient = new FaceServiceClient("78396eceef024cacb6cb12186cf8ee3e", @"https://westeurope.api.cognitive.microsoft.com/face/v1.0");

        String siteurl = "";
        String licenseKey = "2250a1b3d4f5c6"; // Replace the value with your CometChat License Key here
        String apiKey = "09607cd35ef620150f872dbee87248777dcadd1f"; // Replace the value with your CometChat API Key here
        Boolean isCometOnDemand = true;
        private String UID1 = "1232214";
        private String UID2 = "3513245";

        void OnInitializeCometChat(object sender, EventArgs args)
        {
            System.Console.WriteLine("initializeCometChat ");
            var cometchat = DependencyService.Get<CometChatInterface>();

            cometchat.initializeCometChat("www.google.ro", licenseKey, apiKey, isCometOnDemand, new Callbacks(success => initializeSuccess(success), fail => initializeFail(fail)));

        }

        private void initializeSuccess(String success)
        {
            if (success != null)
            {
                System.Console.WriteLine("initializeSuccess " + success.ToString());
            }

        }

        private void initializeFail(String fail)
        {
            if (fail != null)
            {
                System.Console.WriteLine("initializeSuccess " + fail.ToString());
            }
        }


        void OnLoginSuperHero1(object sender, EventArgs args)
        {
            login(UID1);

        }
        void OnLoginSuperHero2(object sender, EventArgs args)
        {
            login(UID2);

        }
        void login(String UID)
        {
            System.Console.WriteLine("Hello " + UID);
            var cometchat = DependencyService.Get<CometChatInterface>();
            cometchat.loginWithUID(UID, new Callbacks(success => loginSuccess(success), fail => loginFail(fail)));

        }

        private void loginSuccess(string success)
        {
            if (success != null)
            {
                System.Console.WriteLine("loginSuccess " + success.ToString());
            }
        }

        private void loginFail(string fail)
        {
            if (fail != null)
            {
                System.Console.WriteLine("loginFail " + fail.ToString());
            }
        }

        void OnLaunchCometChat(object sender, EventArgs args)
        {

            var cometchat = DependencyService.Get<CometChatInterface>();
            cometchat.launchCometChatWindow(true, new LaunchCallbackImplementation(successObj => OnSuccessCall(successObj), fail => OnFailCall(fail), onChatroomInfo => OnChatroomInfo(onChatroomInfo), onError => OnError(onError), onLogout => OnLogout(onLogout), onMessageReceive => OnMessageReceive(onMessageReceive), onUserInfo => OnUserInfo(onUserInfo), onWindowClose => OnWindowClose(onWindowClose)));
        }

        private void OnSuccessCall(String successObj)
        {
            if (successObj != null)
            {
                System.Console.WriteLine("loginSuccess " + successObj.ToString());
            }
        }

        private void OnFailCall(String fail)
        {
            if (fail != null)
            {
                System.Console.WriteLine("OnFailCall " + fail.ToString());
            }
        }

        private void OnChatroomInfo(String onChatroomInfo)
        {
            if (onChatroomInfo != null)
            {
                System.Console.WriteLine("OnChatroomInfo " + onChatroomInfo.ToString());
            }
        }

        private void OnError(String onError)
        {
            if (onError != null)
            {
                System.Console.WriteLine("OnError " + onError.ToString());
            }
        }

        private void OnLogout(String onError)
        {
            if (onError != null)
            {
                System.Console.WriteLine("OnLogout " + onError.ToString());
            }
        }

        private void OnMessageReceive(String onMessageReceive)
        {
            if (onMessageReceive != null)
            {
                System.Console.WriteLine("OnMessageReceive " + onMessageReceive.ToString());
            }
        }

        private void OnUserInfo(String onUserInfo)
        {
            if (onUserInfo != null)
            {
                System.Console.WriteLine("OnUserInfo " + onUserInfo.ToString());

            }
        }

        private void OnWindowClose(String onWindowClose)
        {
            if (onWindowClose != null)
            {
                System.Console.WriteLine("OnWindowClose " + onWindowClose.ToString());
            }
        }

        static async void initPersonGroup()
        {
            string SubscriptionKey = "78396eceef024cacb6cb12186cf8ee3e";
            // Use your own subscription endpoint corresponding to the subscription key.
            string SubscriptionRegion = "https://westeurope.api.cognitive.microsoft.com/face/v1.0/";
            FaceServiceClient faceServiceClient = new FaceServiceClient(SubscriptionKey, SubscriptionRegion);
            string personGroupId = "myfriends";

            //await faceServiceClient.CreatePersonGroupAsync(personGroupId, "My Friends");


            CreatePersonResult friend1 = await faceServiceClient.CreatePersonAsync(
                // Id of the PersonGroup that the person belonged to
                personGroupId,
                // Name of the person
                "Alexandra"
            );

            try
            {

                const string friend1ImageDir = @"C:\Users\dell\Pictures\AnalizaFata";

                foreach (string imagePath in Directory.GetFiles(friend1ImageDir, "*.jpg"))
                {
                    using (Stream s = File.OpenRead(imagePath))
                    {
                        // Detect faces in the image and add to Anna
                        await faceServiceClient.AddPersonFaceAsync(
                            personGroupId, friend1.PersonId, s);
                    }
                }

                // Training the neural network
                TrainingStatus trainingStatus = null;
                for (int i = 0; i <= 1000; i++)
                {
                    trainingStatus = await faceServiceClient.GetPersonGroupTrainingStatusAsync(personGroupId);

                    if (trainingStatus.Status != Status.Running)
                    {
                        break;
                    }


                }

                string testImageFile = @"C:\Users\dell\source\repos\RailTraceMobile - Copy\RailTraceMobile\RailTraceMobile\Alexandra2.jpg";

                using (Stream s = File.OpenRead(testImageFile))
                {
                    var faces = await faceServiceClient.DetectAsync(s);
                    var faceIds = faces.Select(face => face.FaceId).ToArray();

                    var results = await faceServiceClient.IdentifyAsync(personGroupId, faceIds);

                    foreach (var identifyResult in results)
                    {
                        Console.WriteLine("Result of face: {0}", identifyResult.FaceId);
                        if (identifyResult.Candidates.Length == 0)
                        {
                            //Console.WriteLine("Person not identified");
                            recognised = false;
                            // Blur person's face
                        }
                        else
                        {
                            // Get top 1 among all candidates returned
                            var candidateId = identifyResult.Candidates[0].PersonId;
                            var person = await faceServiceClient.GetPersonAsync(personGroupId, candidateId);
                            recognised = true;
                            //Console.WriteLine("Identified as {0}", person.Name);
                            // Highlight person's face.

                        }
                    }
                }
            }
            catch
            {
                recognised = true;
            }
        }

        public LoginPage ()
		{
			InitializeComponent ();
            string connetionString;
            
            MessagingCenter.Subscribe<byte[]>(this, "ImageSelected", (args) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    //Set the source of the image view with the byte array
                    img = new Image
                    {
                        Source = ImageSource.FromStream(() => new MemoryStream((byte[])args))
                    };
                });
            });
        }

        public async void SelectImageClicked(object sender, EventArgs args)
        {
            var action = await DisplayActionSheet("Add Photo", "Cancel", null, "Choose Existing", "Take Photo");

            if (action == "Choose Existing")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var fileName = SetImageFileName();
                    DependencyService.Get<CameraInterface>().LaunchGallery(FileFormatEnum.JPEG, fileName);
                });
            }
            else if (action == "Take Photo")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var fileName = SetImageFileName();
                    DependencyService.Get<CameraInterface>().LaunchCamera(FileFormatEnum.JPEG, fileName);
                });
            }
        }

        /*
         *  Setting the file name is really only needed for Android, when in the OnActivityResult method you need
         *  a way to know the file name passed into the intent when launching the camera/gallery. In this case,
         *  1 image will be saved to the file system using the value of App.DefaultImageId, this is required for the 
         *  FileProvider implemenation that is needed on newer Android OS versions. Using the same file name will 
         *  keep overwriting the existing image so you will not fill up the app's memory size over time. 
         * 
         *  This of course assumes your app has NO need to save images locally. But if your app DOES need to save images 
         *  locally, then pass the file name you want to use into the method SetImageFileName (do NOT include the file extension in the name,
         *  that will be handled down the road based on the FileFormatEnum you pick). 
         * 
         *  NOTE: When saving images, if you decide to pick PNG format, you may notice your app runs slower 
         *  when processing the image. If your image doesn't need to respect any Alpha values, use JPEG, it's faster. 
         */

        private string SetImageFileName(string fileName = null)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                if (fileName != null)
                    App.ImageIdToSave = fileName;
                else
                    App.ImageIdToSave = App.DefaultImageId;

                return App.ImageIdToSave;
            }
            else
            {
                //To iterate, on iOS, if you want to save images to the devie, set 
                if (fileName != null)
                {
                    App.ImageIdToSave = fileName;
                    return fileName;
                }
                else
                    return null;
            }
        }

        public LoginPage(App p)
        {
            this.p = p;
            InitializeComponent();
            MessagingCenter.Subscribe<byte[]>(this, "ImageSelected", (args) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    //Set the source of the image view with the byte array
                    img = new Image
                    {
                        Source = ImageSource.FromStream(() => new MemoryStream((byte[])args))
                    };
                });
            });
        }

        async void TakePhoto_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");

                var image = new Image
                {
                    Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                })
                };
            }

            catch {
                try
                {
                    initPersonGroup();
                }
                catch
                {
                    recognized = true;
                    recognised = true;
                }
            }
        }

        public async void SelectImageClicked1(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
            {
                var image = new Image
                {
                    Source = ImageSource.FromStream(() => { return photo.GetStream(); })
                };
            }

        }

    public async void SignInProcedure(object sender, EventArgs e)
    {
        User user = new User(Entry_Username.Text, Entry_Password.Text);
        SqlHelper sq = new SqlHelper();
            if (sq.GetItem(user.getUserName()) != null || sq.GetItem1(user.getPassword()) != null && recognised == true)
            {
                Application.Current.MainPage.Navigation.PushAsync(new Admin(p));
            }
            else
            {
                DisplayAlert("Login", "Username of Password invalid", "Ok");
            }

    }

        public async void SignInGroup(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(@"https://www.chatcrypt.com/chat.html"));
        }
    }
}