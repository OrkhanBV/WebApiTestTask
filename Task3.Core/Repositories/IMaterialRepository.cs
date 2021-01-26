using System.Collections.Generic;
using System.Threading.Tasks;
using Task3.Core.Models;

namespace Task3.Core.Repositories
{
    public interface IMaterialRepository : IRepository<Material>
    {
        Task<IEnumerable<Material>> GetAllMaterialVersionsAsync();
        Task<Material> GetMateriaVersionsByIdAsync(int id);
    }
}