using Microsoft.AspNetCore.Http;

namespace Task3.Core.DTO
{
    public class UploadMaterialDTO
    {
        public string Name { get; set; }
        /*public string CategoryName { get; set; }*/
        public IFormFile File { get; set; }
    }
}