using BabaevTask5.DAL.Models;
using BabaevTask5.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

/*
        Использовал подход "Связь один ко многим"
        Смотреть ссылку https://metanit.com/sharp/entityframework/3.7.php
 */

namespace BabaevTask5.DAL
{
    public class AppDbContent : IdentityDbContext<User>/*DbContext*/ {
        public AppDbContent(DbContextOptions<AppDbContent> options) : base(options) {
                
            }
            public DbSet<Material> Materials { get; set; }
            public DbSet<MaterialVersion> MaterialVersions { get; set; }
    }
}