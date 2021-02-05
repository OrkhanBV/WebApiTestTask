using System;

namespace Task3.API.DtoRes
{
    public class MaterialVersionResultDto
    {
        public Guid Id { get; set; }
        public string FileName { set; get; }
        public long Size { set; get; }
        public DateTime FileDate { set; get; }
    }
}