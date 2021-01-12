using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BabaevTask5.Data.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}