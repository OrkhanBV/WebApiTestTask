using System;
using Microsoft.AspNetCore.Http;

namespace BabaevTask5.Controllers.Models
{
    public class FormForVersion
    {
        public string Name { get; set; }
        public Guid MaterialId { get; set; }
        public IFormFile File { get; set; }
    }
}