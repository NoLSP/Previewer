using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using OpenCvSharp;
using System.Drawing.Imaging;

namespace Previewer
{
    public static class Manager
    {
        public static string? SourceFilesDirectoryPath { get; set; }
        public static string[]? SourceFilesPaths { get; set; }
        public static string? TargetFilesDirectoryPath { get; set; }
        private static List<VideoInfo> Videos = new List<VideoInfo>();

        private static object LockObject = new object();
        public static int LoadFilesMax { get; private set; } = 1;
        public static int LoadFilesCurrent { get; private set; } = 0;
        public static int LoadFramesMax { get; private set; } = 1;
        public static int LoadFramesCurrent { get; private set; } = 0;

        private static int p_CurrentVideoIndex = 0;
        private static int p_CurrentFrameIndex = 0;

        //public static void LoadFiles()
        //{
        //    if (SourceFilesDirectoryPath == null || !Path.Exists(SourceFilesDirectoryPath))
        //        throw new Exception("Source directory path incorrect");

        //    var files = new DirectoryInfo(SourceFilesDirectoryPath).GetFiles();
        //    lock (LockObject)
        //    {
        //        LoadFilesMax = files.Count();
        //        LoadFilesCurrent = 0;
        //    }

        //    foreach (var file in files)
        //    {
        //        var videoInfo = ObtainVideo(file);

        //        lock (LockObject)
        //        {
        //            if (videoInfo != null)
        //                Videos.Add(videoInfo);
        //            LoadFilesCurrent++;
        //        }
        //    }
        //}

        public static void LoadFiles()
        {
            if (SourceFilesPaths == null || SourceFilesPaths.Any(x => !Path.Exists(x)))
                throw new Exception("Source directory path incorrect");

            lock (LockObject)
            {
                LoadFilesMax = SourceFilesPaths.Count();
                LoadFilesCurrent = 0;
            }

            foreach (var filePath in SourceFilesPaths)
            {
                var videoInfo = ObtainVideo(new FileInfo(filePath));

                lock (LockObject)
                {
                    if (videoInfo != null)
                        Videos.Add(videoInfo);
                    LoadFilesCurrent++;
                }
            }
        }

        private static List<int> GetRandomIndexes(int framesCount, int indexesCount)
        {
            var framesCountInSegment = framesCount / indexesCount;
            var result = new List<int>();
            var random = new Random();

            for(var i = 0; i < indexesCount; i++)
            {
                var index = i * framesCountInSegment + random.Next(framesCountInSegment);
                result.Add(index);
            }

            return result;
        }

        private static VideoInfo? ObtainVideo(FileInfo file)
        {
            if (file.Extension != ".mp4" && file.Extension != ".wmv" && file.Extension != ".avi" && file.Extension != ".mkv")
                return null;

            try
            {
                var capture = new VideoCapture(file.FullName);
                var image = new Mat();
                var frames = new List<Bitmap>();
                var framesByteArrays = new List<byte[]>();
                lock (LockObject)
                {
                    LoadFramesMax = capture.FrameCount;
                    LoadFramesCurrent = 0;
                }

                var indexes = GetRandomIndexes(capture.FrameCount, 10);

                var i = 0;
                while (capture.IsOpened())
                {
                    capture.Read(image);
                    if (image.Empty())
                        break;

                    if (indexes.Contains(i))
                    {
                        var imageByteArray = image.ToBytes(".png");
                        framesByteArrays.Add(imageByteArray);
                        Bitmap? frame = null;
                        using (var ms = new MemoryStream(imageByteArray))
                        {
                            frame = new Bitmap(ms);
                        }
                        frames.Add(frame);
                    }

                    LoadFramesCurrent = i + 1;

                    i++;
                }

                return new VideoInfo(file.FullName, file.Name, frames, framesByteArrays);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public static (int loadedVideosCount, int videosCount, int loadedFramesCount, int framesCount) GetLoadFilesStatus()
        {
            lock(LockObject)
            {
                return (LoadFilesCurrent, LoadFilesMax, LoadFramesCurrent, LoadFramesMax);
            }
        }

        public static (string videoName, int videoNumber, int videosCount, int frameNumber, int framesCount, Bitmap frame) GetCurrentFrame()
        {
            lock (LockObject)
            {
                var videoInfo = Videos[p_CurrentVideoIndex];
                return (videoInfo.Name, p_CurrentVideoIndex + 1, Videos.Count(), p_CurrentFrameIndex + 1, videoInfo.Frames.Count(), videoInfo.Frames[p_CurrentFrameIndex]);
            }
        }

        public static (string videoName, int videoNumber, int videosCount, int frameNumber, int framesCount, Bitmap frame) GetNextFrame()
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

        public static (string videoName, int videoNumber, int videosCount, int frameNumber, int framesCount, Bitmap frame) GetPreviousFrame()
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

        public static (string videoName, int videoNumber, int videosCount, int frameNumber, int framesCount, Bitmap frame) GetNextVideo()
        {
            lock (LockObject)
            {
                var lastVideoIndex = Videos.Count() - 1;

                if (lastVideoIndex > p_CurrentVideoIndex)
                {
                    p_CurrentVideoIndex++;
                    p_CurrentFrameIndex = 0;
                }
            }

            return GetCurrentFrame();

        }

        public static (string videoName, int videoNumber, int videosCount, int frameNumber, int framesCount, Bitmap frame) GetPreviousVideo()
        {
            lock (LockObject)
            {
                if (p_CurrentVideoIndex - 1 >= 0)
                {
                    p_CurrentVideoIndex--;
                    p_CurrentFrameIndex = 0;
                }
            }

            return GetCurrentFrame();
        }


        public static void ReloadCurrentVideo()
        {
            lock (LockObject)
            {
                LoadFilesMax = 1;
                LoadFilesCurrent = 0;
            }

            var videoInfo = ObtainVideo(new FileInfo(Videos[p_CurrentVideoIndex].FilePath));

            lock (LockObject)
            {
                if(videoInfo != null)
                    Videos[p_CurrentVideoIndex] = videoInfo;
                LoadFilesCurrent = 1;
                p_CurrentFrameIndex = 0;
            }
        }

        public static bool SaveCurrentFrame(out string reason)
        {
            var currentVideoInfo = Videos[p_CurrentVideoIndex];

            try
            {
                if(String.IsNullOrWhiteSpace(TargetFilesDirectoryPath) || !Path.Exists(TargetFilesDirectoryPath))
                {
                    reason = "Incorrect target directory.";
                    return false;
                }

                var frameBytes = currentVideoInfo.FramesByteArrays[p_CurrentFrameIndex];
                var fileName = currentVideoInfo.Name.Substring(0 , currentVideoInfo.Name.LastIndexOf(".")) + ".png";
                var filePath = Path.Combine(TargetFilesDirectoryPath, fileName);
                if (Path.Exists(filePath))
                    File.Delete(filePath);
                File.WriteAllBytes(filePath, frameBytes);

                reason = "";
                return true;
            }
            catch(Exception e)
            {
                reason = e.Message;
                return false;
            }
        }
    }
}
