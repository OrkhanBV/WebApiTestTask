using Microsoft.AspNetCore.Http;

namespace WebApiOrkhan.Controllers.Models
{
    public class FormForMaterials
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
        
        /*public string MaterialType { get; set; }
        
        public string MaterialName { get; set; } 
        
        Возможно и другие данные*/
    }
}