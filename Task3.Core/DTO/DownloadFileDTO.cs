using System;

namespace Task3.Core.DTO
{
    public class DownloadFileDTO
    {
        public byte[] mas { get; set; }
        public string fileType{ get; set; }
        public string filePath{ get; set; }
        public string fileName{ get; set; }
    }
}