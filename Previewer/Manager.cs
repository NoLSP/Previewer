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
        private static List<VideoInfo> Videos = new List<VideoInfo>();

        private static object LockObject = new object();
        public static int LoadFilesMax { get; private set;}
        public static int LoadFilesCurrent { get; private set; } = 0;

        private static int p_CurrentVideoIndex = 0;
        private static int p_CurrentFrameIndex = 0;

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
                var frames = new List<Bitmap>();

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
                        frames.Add(frame);
                    }

                    i++;
                }

                lock(LockObject)
                {
                    Videos.Add(new VideoInfo(file.FullName, file.Name, frames));
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

        public static (string videoName, int frameNumber, int framesCount, Bitmap frame) GetCurrentFrame()
        {
            lock (LockObject)
            {
                var videoInfo = Videos[p_CurrentVideoIndex];
                return (videoInfo.Name, p_CurrentFrameIndex + 1, videoInfo.Frames.Count(), videoInfo.Frames[p_CurrentFrameIndex]);
            }
        }

        public static (string videoName, int frameNumber, int framesCount, Bitmap frame) GetNextFrame()
        {
            lock (LockObject)
            {
                var nextVideoIndex = p_CurrentVideoIndex;
                var nextFrameIndex = p_CurrentFrameIndex;

                var lastCurrentVideoFrameIndex = Videos[nextVideoIndex].Frames.Count() - 1;
                var lastVideoIndex = Videos.Count() - 1;

                if (lastCurrentVideoFrameIndex == nextFrameIndex && lastVideoIndex > nextVideoIndex)
                {
                    nextVideoIndex = p_CurrentVideoIndex + 1;
                    nextFrameIndex = 0;
                }
                else if(nextFrameIndex < lastCurrentVideoFrameIndex)
                {
                    nextFrameIndex++;
                }

                p_CurrentVideoIndex = nextVideoIndex;
                p_CurrentFrameIndex = nextFrameIndex;
            }

            return GetCurrentFrame();

        }

        public static (string videoName, int frameNumber, int framesCount, Bitmap frame) GetPreviousFrame()
        {
            lock (LockObject)
            {
                var previousVideoIndex = p_CurrentVideoIndex;
                var previousFrameIndex = p_CurrentFrameIndex;

                if (previousFrameIndex == 0 && previousVideoIndex - 1 >= 0)
                {
                    previousVideoIndex = p_CurrentVideoIndex - 1;
                    previousFrameIndex = Videos[previousVideoIndex].Frames.Count() - 1;
                }
                else if(previousFrameIndex > 0)
                {
                    previousFrameIndex--;
                }

                p_CurrentVideoIndex = previousVideoIndex;
                p_CurrentFrameIndex = previousFrameIndex;
            }

            return GetCurrentFrame();
        }
    }
}
