using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using OpenCvSharp;

namespace Previewer
{
    public static class Manager
    {
        public static string? SourceFilesDirectoryPath { get; set; }
        public static string? TargetFilesDirectoryPath { get; set; }
        private static List<Dictionary<int, Bitmap>> Videos = new List<Dictionary<int, Bitmap>>();

        private static object LockObject = new object();
        public static int LoadFilesMax { get; private set;}
        public static int LoadFilesCurrent { get; private set; } = 0;

        public static void LoadFiles()
        {
            if (SourceFilesDirectoryPath == null || !Path.Exists(SourceFilesDirectoryPath))
                throw new Exception("Source directory path incorrect");

            var files = new DirectoryInfo(SourceFilesDirectoryPath).GetFiles();
            lock(LockObject)
            {
                LoadFilesMax = files.Count();
                LoadFilesCurrent = 0;
            }

            foreach (var file in files)
            {
                var videoFile = file.FullName;
                var capture = new VideoCapture(videoFile);
                var image = new Mat();
                var frames = new Dictionary<int, Bitmap>();


                var indexes = new List<int>();
                var random = new Random();
                for(var j = 0; j < 10; j++)
                {
                    var index = 0;
                    var tryCount = 30;
                    while ((index == 0 || indexes.Any(x => Math.Abs(index - x) < 150)) && tryCount > 0)
                    {
                        index = random.Next(capture.FrameCount);
                        tryCount--;
                    }

                    if(index > 0)
                        indexes.Add(index);
                }

                var i = 0;

                while (capture.IsOpened())
                {
                    capture.Read(image);
                    if (image.Empty())
                        break;

                    if (indexes.Contains(i))
                    {
                        var imageByteArray = image.ToBytes(".jpg");
                        Bitmap? frame = null;
                        using (var ms = new MemoryStream(imageByteArray))
                        {
                            frame = new Bitmap(ms);
                        }
                        frames[i] = frame;
                    }

                    i++;
                }

                Videos.Add(frames);
                lock(LockObject)
                {
                    LoadFilesCurrent++;
                }
            }
        }

        public static (int, int) GetLoadFilesStatus()
        {
            lock(LockObject)
            {
                return (LoadFilesCurrent, LoadFilesMax);
            }
        }
    }
}
