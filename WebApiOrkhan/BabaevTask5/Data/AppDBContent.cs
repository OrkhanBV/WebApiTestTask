
using BabaevTask5.Data.Models;
using Microsoft.EntityFrameworkCore;

/*
        Использовал подход "Связь один ко многим"
        Смотреть ссылку https://metanit.com/sharp/entityframework/3.7.php
 */

namespace BabaevTask5.Data
{
    public class AppDbContent : DbContext {
        public AppDbContent(DbContextOptions<AppDbContent> options) : base(options) {
                
            }
            public DbSet<Material> Materials { get; set; }
            public DbSet<MaterialVersion> MaterialVersions { get; set; }
    }
}