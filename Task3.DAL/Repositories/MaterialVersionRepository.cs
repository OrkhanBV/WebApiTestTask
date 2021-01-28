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

        private Task3DbContext Task3DbContext
        {
            get { return Context as Task3DbContext; }
        }

        public async Task<IEnumerable<MaterialVersion>> GetFilterVersionsByDate(Guid mId)
        {
            return await Task3DbContext.MaterialVersions.Where(m => m.Material.Id == mId)
                .OrderByDescending(m => m.FileDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<MaterialVersion>> GetFilterVersionsBySize(Guid mId)
        {
            return await Task3DbContext.MaterialVersions.Where(m => m.Material.Id == mId)
                .OrderByDescending(m => m.Size)
                .ToListAsync();
        }
    }
}