using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task3.Core.Models;
using Task3.Core.Repositories;

namespace Task3.DAL.Repositories
{
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        public MaterialRepository(DbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Material>> GetAllMaterialVersionsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Material> GetMateriaVersionsByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}