using System;
using Task3.Core.Models;

namespace Task3.API.Resources
{
    public class MaterialVersionResultDto
    {
        public Guid Id { get; set; }
        public string FileName { set; get; }
        public long Size { set; get; }
        public DateTime FileDate { set; get; }
    }
}