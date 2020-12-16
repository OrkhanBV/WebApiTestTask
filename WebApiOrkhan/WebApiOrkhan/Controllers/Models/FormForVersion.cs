using Microsoft.AspNetCore.Http;

namespace WebApiOrkhan.Controllers.Models
{
    public class FormForVersion
    {
        public string Name { get; set; }
        public int materialId { get; set; }
        public IFormFile File { get; set; }
    }
}