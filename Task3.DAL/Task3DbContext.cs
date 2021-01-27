using Microsoft.EntityFrameworkCore;
using Task3.Core.Models;

namespace Task3.DAL
{
    public class Task3DbContext : DbContext
    {
        public Task3DbContext(DbContextOptions<Task3DbContext> options) : base(options)
        {
            
        }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialVersion> MaterialVersions { get; set; }
    }
}