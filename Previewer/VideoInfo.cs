using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Previewer
{
    public class VideoInfo
    {
        public string FilePath { get; set; }
        public List<Bitmap> Frames { get; set; }

        public VideoInfo(string filePath, List<Bitmap> frames) 
        { 
            FilePath = filePath;
            Frames = frames;
        }
    }
}
