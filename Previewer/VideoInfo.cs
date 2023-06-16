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
        public string Name { get; set; }
        public List<Bitmap> Frames { get; set; }

        public VideoInfo(string filePath, string name, List<Bitmap> frames) 
        { 
            Name = name;
            FilePath = filePath;
            Frames = frames;
        }
    }
}
