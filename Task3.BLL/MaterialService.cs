using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Task3.Core;
using Task3.Core.DTO;
using Task3.Core.Models;
using Task3.Core.Services;

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

        public async Task<Material> UploadNewMaterial(/*IFormFile file,*/ UploadMaterialDTO materialForm)
        {
            /*if (materialForm.CategoryName != Convert.ToInt16(MatCategory.Другое) ||
                materialForm.CategoryName != Convert.ToInt16(MatCategory.Презентация) ||
                materialForm.CategoryName != Convert.ToInt16(MatCategory.Приложение))
                return null;
            else
            {*/
                //Создаем материал и сохраняем изменения в BD
                Material uploadedMaterial = new Material
                {
                    MaterialDate = DateTime.Now,
                    MaterialName = materialForm.Name,
                    MatCategoryId = 1/*materialForm.CategoryName*/
                };
                //appDbContent.SaveChanges();
                //Создаем версию материала 
                MaterialVersion version = new MaterialVersion
                {
                    FileDate = DateTime.Now,
                    Material = uploadedMaterial,
                    FileName = materialForm.Name,
                    Size = materialForm.File.Length,
                    /*Size = file.Length,*/
                    PathOfFile = _dir
                };
                using (var fileStream = new FileStream(
                    Path.Combine(_dir,
                        $"{materialForm.Name}{Path.GetExtension(materialForm.File.FileName)}"),
                        /*$"{materialForm.Name}{Path.GetExtension(file.FileName)}"),*/
                    FileMode.Create,
                    FileAccess.Write))
                {
                    materialForm.File.CopyTo(fileStream);
                    /*file.CopyTo(fileStream);*/
                }

                //После того как убедились, что у нас всё ок сохраняем в бд
                await _unitOfWork.MaterialVersions.AddRangeAsync(new List<MaterialVersion> {version});
                await _unitOfWork.CommitAsync();
                return uploadedMaterial;
            }
        }

    
}