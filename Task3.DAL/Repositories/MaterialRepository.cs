using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task3.Core.DTO;
using Task3.Core.Models;
using Task3.Core.Repositories;

namespace Task3.DAL.Repositories
{
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        public MaterialRepository(DbContext context) : base(context)
        {
        }
        
        private Task3DbContext Task3DbContext
        {
            get { return Context as Task3DbContext; }
        }

        public async Task<IEnumerable<Material>> FilterMaterialsByDate()
        {
            return await Task3DbContext.Materials
                .OrderByDescending(m => m.MaterialDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Material>> FilterMatreerialsByType(int catId)
        {
            return await Task3DbContext.Materials
                .OrderByDescending(m => m.MatCategoryId == catId)
                .ToListAsync();
        }

        public async Task<Material> GetMaterialById(Guid mId)
        {
            return await Task3DbContext.Materials.FindAsync(mId);
        }
    }
}