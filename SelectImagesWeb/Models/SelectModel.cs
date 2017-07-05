using System.Collections.Generic;

namespace SelectImagesWeb.Models
{
    public class SelectModel
    {
        public string CurrentImage { get; set; }
        public string MoveToFolder { get; set; }
        public List<string> Folders { get; set; }
        public int ImageCount { get; set; }
    }
}