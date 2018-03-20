using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web.Http;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string mruToken;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void BrowseFiles_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile file = await openPicker.PickSingleFileAsync();
            var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
            mruToken = mru.Add(file, "image");

            if (file != null)
            {
                FileName.Text = file.Name;
            }
        }

        private async void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            string codeType = null;
            if (AndroidRadio.IsChecked == true)
                codeType = "android";
            else if (WebRadio.IsChecked == true)
                codeType = "web";
            else if (IosRadio.IsChecked == true)
                codeType = "ios";
            else if (WindowsRadio.IsChecked == true)
                codeType = "windows";
            else
                codeType = "";
            if(codeType == "")
            {
                ErrorBlock.Text = "Select the format in which you want to generate the code.";
            }
            else
            {
                var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
                try
                {
                    StorageFile retrievedFile = await mru.GetFileAsync(mruToken);
                    IInputStream inputStream = await retrievedFile.OpenAsync(FileAccessMode.Read);

                    HttpMultipartFormDataContent multipartContent = new HttpMultipartFormDataContent();

                    multipartContent.Add(
                        new HttpStreamContent(inputStream),
                        "myFile",
                        retrievedFile.Name);

                    multipartContent.Add(
                        new HttpStringContent(codeType),
                        "type");

                    HttpClient client = new HttpClient();
                    Uri uri = new Uri("http://4403b864.ngrok.io/pix2code/");
                    HttpResponseMessage response = await client.PostAsync(
                        uri,
                        multipartContent);
                    string res = await response.Content.ReadAsStringAsync();
                    if (res != "ERROR")
                    {
                        CodeParams a = new CodeParams();
                        a.File = retrievedFile;
                        a.Code = res;
                        Frame.Navigate(typeof(SecondPage), a);
                    }
                    else
                    {
                        ErrorBlock.Text = "An error occurred. Please Try again";
                    }
                }
                catch(Exception exception)
                {
                    Debug.WriteLine(exception.Message);
                    ErrorBlock.Text = "You need to select an image to generate code";
                }
            }
        }
    }
}
