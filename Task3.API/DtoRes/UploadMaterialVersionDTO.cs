using System;

namespace Task3.API.DtoRes
{
    public class UploadMaterialVersionDto
    {
        public string Name { get; set; }
        public Guid MaterialId { get; set; }
        //public IFormFile File { get; set; }
    }
}