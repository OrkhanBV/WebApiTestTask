using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Task3.Core.Models;

namespace Task3.Core.Services
{
    public interface IMaterialServices
    {
        Task<IEnumerable<Material>> GetFilterMaterialsByDate();
        Task<IEnumerable<Material>> GetFilterMatreerialsByType(int categoryId);
        
        //Task<Material> UploadNewMaterial(UploadMaterialDTO materialVersion);
        //Task<DownloadFileDTO> GetDtoForDownloadMaterialAsync(Guid mId);
        Task<Material> UploadNewMaterial(string fileName, Int32 categoryNameId, long length);
        Task<(byte[] mas, string fileType, string fileName)> GetDataForDownloadMaterialAsync(Guid mId);
        
        Task<IEnumerable<MaterialVersion>> FilterVersionsByDate(Guid mId);
        Task<IEnumerable<MaterialVersion>> FilterVersionsBySize(Guid mId);
        
        //Task<MaterialVersion> UploadNewMaterialVersion(UploadMaterialVersionDTO materialVersion);
        //Task<DownloadFileDTO> GetMaterialVersionFile(Guid vId);
        
        Task<(byte[] mas, string fileType, string fileName)> GetMaterialVersionFile(Guid vId);
        Task<MaterialVersion> UploadNewMaterialVersion(string fileName, Guid mId, long length);
        
    }
}