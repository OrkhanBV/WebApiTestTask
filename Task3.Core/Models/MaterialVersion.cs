using System;

namespace Task3.Core.Models
{
    public class MaterialVersion
    {
        public Guid Id { set; get; }
        public string FileName { set; get; }
        public long Size { set; get; }
        public string PathOfFile { set; get; }
        public DateTime FileDate { set; get; }
        public Material Material { set; get; }
    }
}