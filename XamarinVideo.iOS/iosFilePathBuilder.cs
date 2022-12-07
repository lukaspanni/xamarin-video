using Foundation;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using System;

namespace XamarinVideo.iOS
{
    class iosFilePathBuilder : IPlatformFilePathBuilder
    {
        public string buildPlatformSpecificFilePath(string name)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "crossVideo", name);
        }
    }
}