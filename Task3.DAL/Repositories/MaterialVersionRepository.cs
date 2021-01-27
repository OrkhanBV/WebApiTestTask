using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task3.Core.Models;
using Task3.Core.Repositories;

namespace Task3.DAL.Repositories
{
    public class MaterialVersionRepository : Repository<MaterialVersion>, IMaterialVersionRepository
    {
        public MaterialVersionRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MaterialVersion>> GetAllWithMaterialAsync()
        {
            return await Task3DbContext.MaterialVersions.Include(m => m.Material)
                .ToListAsync();
        }

        public async Task<MaterialVersion> GetWithMaterialByIdAsync(Guid id)
        {
            return await Task3DbContext.MaterialVersions.Include(m => m.Material)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MaterialVersion>> GetAllWithMaterialIdAsync(Guid materialId)
        {
            return await Task3DbContext.MaterialVersions.Where(m => m.Material.Id == materialId)
                .ToListAsync();
        }


        private Task3DbContext Task3DbContext
        {
            get { return Context as Task3DbContext; }
        }
        
    }
}