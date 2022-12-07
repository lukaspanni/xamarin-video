using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.CommunityToolkit;


namespace XamarinVideo
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        private string videoPath;

        public string VideoPath
        {
            get { return videoPath; }
            set
            {
                SetField(ref videoPath, value, "VideoPath");
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        async Task TakeVideoAsync()
        {
            try
            {
                var video = await MediaPicker.CaptureVideoAsync();
                await LoadVideoAsync(video);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine("Feature is not supported on the device");

            }
            catch (PermissionException pEx)
            {
                Console.WriteLine("Permissions not granted");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadVideoAsync(FileResult video)
        {
            // canceled
            if (video == null)
            {
                VideoPath = null;
                return;
            }

            requestPermissionsIfNeeded();
            string newFilePath = BuildFileName(video);
            Directory.CreateDirectory(Path.GetDirectoryName(newFilePath));

            using (var stream = await video.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFilePath))
                await stream.CopyToAsync(newStream);


            Console.WriteLine($"Stored Video {newFilePath}");

            VideoPath = newFilePath;
        }

        private static string BuildFileName(FileResult video)
        {
            return App.filePathBuilder.buildPlatformSpecificFilePath(video.FileName);
        }

        private async void requestPermissionsIfNeeded()
        {
            if (Device.RuntimePlatform != Device.Android)
                return;

            if((await Permissions.CheckStatusAsync<Permissions.StorageRead>()) != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.StorageRead>();
            }
            if ((await Permissions.CheckStatusAsync<Permissions.StorageWrite>()) != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.StorageWrite>();
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            OutputLabel.Text = "Test Camera";
            Console.WriteLine("Test Camera");
            await TakeVideoAsync();

        }
    }
}
