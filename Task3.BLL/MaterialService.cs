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
                .Find(m => m.Material.Id == mId)/*Where(m => m.Material.Id == mId)*/
                .ToList()
                .OrderByDescending(m =>m.FileDate);
            MaterialVersion ActualVersion = ActualList().Select(m=> m).FirstOrDefault();
            file.fileName = ActualVersion.FileName;
            //file.filePath = ActualVersion.PathOfFile + "/" + file.fileName + ".jpeg";
            file.filePath = ActualVersion.PathOfFile + "/" + file.fileName;
            file.fileType = "application/octet-stream";// + Path.GetExtension(file.fileName);
            
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
                    //MaterialName = materialForm.Name/* + materialForm.Extensions*/,
                    MaterialName = $"{materialForm.Name}{Path.GetExtension(materialForm.File.FileName)}",
                    MatCategoryId = Convert.ToInt16(materialForm.CategoryNameId)
                };
            //Создаем версию материала 
                MaterialVersion version = new MaterialVersion
                {
                    FileDate = DateTime.Now,
                    Material = uploadedMaterial,
                    //FileName = materialForm.Name/* + materialForm.Extensions*/,
                    FileName = $"{materialForm.Name}{Path.GetExtension(materialForm.File.FileName)}",
                    Size = materialForm.File.Length,
                    PathOfFile = _dir
                };
                /*using (var fileStream = new FileStream(
                    Path.Combine(_dir,
                        $"{materialForm.Name}{Path.GetExtension(materialForm.File.FileName)}"),
                    FileMode.Create,
                    FileAccess.Write))
                {
                    materialForm.File.CopyTo(fileStream);
                }*/

                //После того как убедились, что у нас всё ок сохраняем в бд используя unitOfWork
                await _unitOfWork.MaterialVersions.AddRangeAsync(new List<MaterialVersion> {version});
                await _unitOfWork.CommitAsync();
                return uploadedMaterial;
            }

        /*public Task<DownloadFileDTO> GetDtoForDownloadMaterialAsync(Guid mId)
        {
            throw new NotImplementedException();
        }*/
    }

    
}