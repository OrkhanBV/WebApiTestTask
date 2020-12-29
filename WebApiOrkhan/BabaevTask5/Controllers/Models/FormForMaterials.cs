using Microsoft.AspNetCore.Http;

namespace BabaevTask5.Controllers.Models
{
    public class FormForMaterials
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public IFormFile File { get; set; }
        
    }
}