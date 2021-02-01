using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task3.Core.DTO;
using Task3.Core.Models;

namespace Task3.Core.Repositories
{
    public interface IMaterialRepository : IRepository<Material>
    {
        Task<IEnumerable<Material>> FilterMaterialsByDate();
        Task<IEnumerable<Material>> FilterMatreerialsByType(int catId);

        /*Task<DownloadFileDTO> GetDtoForDownloadMaterialAsync(Guid mId);*/
        Task<Material> GetMaterialById(Guid mId);
    }
}