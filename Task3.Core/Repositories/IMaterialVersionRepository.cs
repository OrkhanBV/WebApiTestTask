using System.Collections.Generic;
using System.Threading.Tasks;
using Task3.Core.Models;

namespace Task3.Core.Repositories
{
    public interface IMaterialVersionRepository : IRepository<MaterialVersion>
    {
        Task<IEnumerable<MaterialVersion>> GetAllWithMaterialAsync();
        Task<MaterialVersion> GetWithMaterialByIdAsync(int id);
        Task<IEnumerable<MaterialVersion>> GetAllWithMaterialIdAsync(int materialId);
    }
}