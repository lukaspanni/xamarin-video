using System.IO;
using XamarinVideo;


namespace XamarinVideo.Droid
{
    class AndroidFilePathBuilder : IPlatformFilePathBuilder
    {
        public string buildPlatformSpecificFilePath(string name)
        {
            // targetFolder: /storage/emulated/0/Documents/crossVideo
            return Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath, "crossVideo", name);
        }
    }
}