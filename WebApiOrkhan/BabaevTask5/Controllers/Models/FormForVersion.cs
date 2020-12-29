using Microsoft.AspNetCore.Http;

namespace BabaevTask5.Controllers.Models
{
    public class FormForVersion
    {
        public string Name { get; set; }
        public int MaterialId { get; set; }
        public IFormFile File { get; set; }
    }
}