using System;
using Microsoft.AspNetCore.Http;

namespace Task3.Core.DTO
{
    public class UploadMaterialVersionDTO
    {
        public string Name { get; set; }
        public Guid MaterialId { get; set; }
        public IFormFile File { get; set; }
    }
}