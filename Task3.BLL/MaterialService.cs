using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task3.Core;
using Task3.Core.DTO;
using Task3.Core.Models;
using Task3.Core.Services;
using Task3.DAL;

namespace Task3.BLL
{
    public class MaterialService : IMaterialServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _env;
        private string _dir;

        public MaterialService(IUnitOfWork unitOfWork, IHostingEnvironment env)
        {
            this._unitOfWork = unitOfWork;
            _env = env;
            _dir = _env.ContentRootPath + "/MaterialStorage";
        }

        public async Task<IEnumerable<Material>> GetFilterMaterialsByDate()
        {
            return await _unitOfWork.Materials.FilterMaterialsByDate();
        }

        public async Task<IEnumerable<Material>> GetFilterMatreerialsByType(int categoryId)
        {
            return await _unitOfWork.Materials.FilterMatreerialsByType(categoryId);
        }

        public async Task<DownloadFileDTO> GetDtoForDownloadMaterialAsync(Guid mId)
        {
            DownloadFileDTO file = new DownloadFileDTO();
            IEnumerable<MaterialVersion> ActualList() => _unitOfWork.MaterialVersions
                .Find(m => m.Material.Id == mId)
                .ToList()
                .OrderByDescending(m =>m.FileDate);
            MaterialVersion ActualVersion = ActualList().Select(m=> m).FirstOrDefault();
            file.FileName = ActualVersion.FileName;
            file.FilePath = ActualVersion.PathOfFile + "/" + file.FileName;
            file.FileType = "application/octet-stream";
            file.Mas = System.IO.File.ReadAllBytes(file.FilePath);
            
            return file;
        }

        public async Task<Material> UploadNewMaterial(UploadMaterialDTO materialForm)
        {
            using (var fileStream = new FileStream(
                Path.Combine(_dir,
                    $"{materialForm.Name}{Path.GetExtension(materialForm.File.FileName)}"),
                FileMode.Create,
                FileAccess.Write))
            {
                materialForm.File.CopyTo(fileStream);
            }
            
            Material uploadedMaterial = new Material
                {
                    MaterialDate = DateTime.Now,
                    MaterialName = $"{materialForm.Name}{Path.GetExtension(materialForm.File.FileName)}",
                    MatCategoryId = Convert.ToInt16(materialForm.CategoryNameId)
                };
            MaterialVersion version = new MaterialVersion
                {
                    FileDate = DateTime.Now,
                    Material = uploadedMaterial,
                    FileName = $"{materialForm.Name}{Path.GetExtension(materialForm.File.FileName)}",
                    Size = materialForm.File.Length,
                    PathOfFile = _dir
                };
            
            await _unitOfWork.MaterialVersions.AddRangeAsync(new List<MaterialVersion> {version});
            await _unitOfWork.CommitAsync();
            return uploadedMaterial;
        }
        
        //FOR VERSIONS
        public async Task<IEnumerable<MaterialVersion>> FilterVersionsByDate(Guid mId)
        {
            return await _unitOfWork.MaterialVersions.GetFilterVersionsByDate(mId);
        }

        public async Task<IEnumerable<MaterialVersion>> FilterVersionsBySize(Guid mId)
        {
            return await _unitOfWork.MaterialVersions.GetFilterVersionsBySize(mId);
        }

        public async Task<MaterialVersion> UploadNewMaterialVersion(UploadMaterialVersionDTO materialVersionform)
        {
            using (var fileStream = new FileStream(
                Path.Combine(_dir,
                    $"{materialVersionform.Name}{Path.GetExtension(materialVersionform.File.FileName)}"),
                FileMode.Create,
                FileAccess.Write))
            {
                materialVersionform.File.CopyTo(fileStream);
            }
            
            MaterialVersion uploadedVersion = new MaterialVersion
            {
                FileDate = DateTime.Now,
                FileName = $"{materialVersionform.Name}{Path.GetExtension(materialVersionform.File.FileName)}",
                PathOfFile = _dir,
                Size = materialVersionform.File.Length,
                Material = await _unitOfWork.Materials.GetMaterialById(materialVersionform.MaterialId)
            };

            await _unitOfWork.MaterialVersions.AddRangeAsync(new List<MaterialVersion> {uploadedVersion});
            await _unitOfWork.CommitAsync();
            return uploadedVersion;
        }

        public async Task<DownloadFileDTO> GetMaterialVersionFile(Guid vId)
        {
            DownloadFileDTO file = new DownloadFileDTO();
            MaterialVersion GetOfMaterialVersion() =>
                _unitOfWork.MaterialVersions.Find(m => m.Id == vId).SingleOrDefault();
            
            file.FileName = GetOfMaterialVersion().FileName;
            file.FilePath = (GetOfMaterialVersion().PathOfFile + "/" + file.FileName);
            file.FileType = "application/octet-stream";
            file.Mas = System.IO.File.ReadAllBytes(file.FilePath);
            return(file);
        }
    }
}