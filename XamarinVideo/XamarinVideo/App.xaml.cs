using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.Xaml;
using System;

namespace XamarinVideo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            //cleanup cache files
            foreach (string f in Directory.GetFiles(FileSystem.CacheDirectory)){
                try
                {
                    File.Delete(f);
                }
                catch (Exception ex) { }
            }

        }

        protected override void OnResume()
        {
        }
    }
}
