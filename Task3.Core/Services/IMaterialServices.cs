using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Task3.Core.DTO;
using Task3.Core.Models;

namespace Task3.Core.Services
{
    public interface IMaterialServices
    {
        Task<IEnumerable<Material>> GetFilterMaterialsByDate();
        Task<IEnumerable<Material>> GetFilterMatreerialsByType(int categoryId);
        
        Task<Material> UploadNewMaterial(UploadMaterialDTO materialVersion);
        Task<DownloadFileDTO> GetDtoForDownloadMaterialAsync(Guid mId);

        /*public IOrderedEnumerable<Material> FilterMaterialsByDate { get; set; }
        public List<Material> FilterMaterialByType(string type);
        public string GetInfoAboutMaterial(Guid id);
        public Guid UploadNewMaterial(FormForMaterials formForMaterials);
        public Guid ChangeCetagoryOfMaterial(Guid mId, string type);
        public FileModel GetFileParametrsForDownloadActualVersion(Guid mId);*/
    }
}