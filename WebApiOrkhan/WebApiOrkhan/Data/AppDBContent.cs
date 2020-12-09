using System;
using Microsoft.EntityFrameworkCore;
using WebApi5.Data.Models;
using WebApiOrkhan.Data.Models;

namespace WebApiOrkhan.Data
{
    //Данный класс является классом подключения к базе данных
    public class AppDBContent : DbContext {

            public AppDBContent(DbContextOptions<AppDBContent> options) : base(options) {
                
            }

            public DbSet<Category> Category { get; set; }
            public DbSet<Material> Material { get; set; }
            public DbSet<File> File { get; set; }
    }
}