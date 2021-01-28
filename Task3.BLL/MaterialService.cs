using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
            _dir = _env.ContentRootPath + "/Task3.DAL/MaterialStorage";
        }

        public async Task<IEnumerable<Material>> GetFilterMaterialsByDate()
        {
            return await _unitOfWork.Materials.FilterMaterialsByDate();
        }

        public async Task<IEnumerable<Material>> GetFilterMatreerialsByType(int categoryId)
        {
            return await _unitOfWork.Materials.FilterMatreerialsByType(categoryId);
        }

        public async Task<Material> UploadNewMaterial(UploadMaterialDTO materialForm)
        {
            if (materialForm.CategoryName != Convert.ToInt16(MatCategory.Другое) ||
                materialForm.CategoryName != Convert.ToInt16(MatCategory.Презентация) ||
                materialForm.CategoryName != Convert.ToInt16(MatCategory.Приложение))
                return null;
            else
            {
                //Создаем материал и сохраняем изменения в BD
                Material mt1;
                mt1 = new Material
                {
                    MaterialDate = DateTime.Now,
                    MaterialName = materialForm.Name,
                    MatCategoryId = materialForm.CategoryName
                };
                //appDbContent.SaveChanges();
                //Создаем версию материала 
                MaterialVersion version = new MaterialVersion
                {
                    FileDate = DateTime.Now,
                    Material = mt1,
                    FileName = materialForm.Name,
                    Size = materialForm.File.Length,
                    PathOfFile = _dir
                };
                using (var fileStream = new FileStream(
                    Path.Combine(_dir,
                        $"{materialForm.Name}{Path.GetExtension(materialForm.File.FileName)}"),
                    FileMode.Create,
                    FileAccess.Write))
                {
                    materialForm.File.CopyTo(fileStream);
                }

                //После того как убедились, что у нас всё ок сохраняем в бд
                await _unitOfWork.MaterialVersions.AddRangeAsync(new List<MaterialVersion> {version});
                await _unitOfWork.CommitAsync();
                return mt1;
            }
        }

    }
}