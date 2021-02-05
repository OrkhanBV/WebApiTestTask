using System;

namespace Task3.Core.DTO
{
    public class DownloadFileDTO
    {
        public byte[] Mas { get; set; }
        public string FileType{ get; set; }
        public string FilePath{ get; set; }
        public string FileName{ get; set; }
    }
}