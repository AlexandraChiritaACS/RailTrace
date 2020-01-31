using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RailTraceMobile.Views
{
    interface IBaseUrl { string Get(); }
    public class BaseUrlWebView : WebView { }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LessonPage : ContentPage
    {
        public LessonPage()
        {
            InitializeComponent();
            BaseUrlWebView lessonWebview = new BaseUrlWebView();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html>
<head></head>
<body>
<p><a href=""C:\Users\dell\source\repos\RailTraceMobile - Copy\RailTraceMobile\RailTraceMobile\Views\BOT23.html"">next page</a></p>
</body>
</html>";
            htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
            lessonWebview.Source = @"C:\Users\dell\source\repos\RailTraceMobile - Copy\RailTraceMobile\RailTraceMobile\Views\BOT23.html";
            this.Content = lessonWebview;
        }
    }
}