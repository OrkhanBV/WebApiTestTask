
using Microsoft.EntityFrameworkCore;
using WebApiOrkhan.Data.Models;

/*
        Использовал подход "Связь один ко многим"
        Смотреть ссылку https://metanit.com/sharp/entityframework/3.7.php
 */

namespace WebApiOrkhan.Data
{
    public class AppDBContent : DbContext {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options) {
                
            }
            public DbSet<Material> Materials { get; set; }
            public DbSet<File> Files { get; set; }
    }
}