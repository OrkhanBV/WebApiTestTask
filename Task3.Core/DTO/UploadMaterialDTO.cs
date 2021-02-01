using System;
using Microsoft.AspNetCore.Http;

namespace Task3.Core.DTO
{
    public class UploadMaterialDTO
    {
        public string Name { get; set; }
        public Int32 CategoryNameId { get; set; }
        public IFormFile File { get; set; }
    }
}