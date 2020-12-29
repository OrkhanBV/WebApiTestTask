using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BabaevTask5.Data.Interfaces;
using BabaevTask5.Data.Models;
using BabaevTask5.Controllers.Models;
using Microsoft.AspNetCore.Hosting;

namespace BabaevTask5.Data.Repository
{
    public class MaterialRepository : IMaterial
    {
        private AppDbContent appDbContent;
        private IWebHostEnvironment _env;
        private string _dir;

        public MaterialRepository(AppDbContent appDbContent, IWebHostEnvironment env)
        {
            this.appDbContent = appDbContent;
            _env = env;
            _dir = env.ContentRootPath + "/MaterialStorage";
        }

        public IOrderedEnumerable<Material> FilterMaterialsByDate
        {
            get => appDbContent.Materials.ToList().OrderByDescending(m => m.MaterialDate);
            set => throw new NotImplementedException();
        }

        public List<Material> FilterMaterialByType(string type)
        {
            if(type == "Другое" || type == "Приложение" || type == "Презентация")
                return appDbContent.Materials.Where(m => m.CategoryType == type).ToList();
            else
            {
                return null;
            }
        }

        public IEnumerable<MaterialVersion> GetAllVersions(Guid id) => appDbContent.MaterialVersions
            .Where(m => m.Id == id).ToList()
            .OrderByDescending(v => v.Material.Id == id);
        
        public string GetInfoAboutMaterial(Guid id)
        {
             int countOfVersion = appDbContent.MaterialVersions.Where(v => v.Material.Id == id).Count();
             string lastUpdate = GetAllVersions(id).Select(v => v.FileDate).SingleOrDefault().ToString();
             return ($"Count ov versions of material = {countOfVersion} \n" +
                     $"Last update = {lastUpdate}");
        }
        
        public Guid UploadNewMaterial(FormForMaterials formMaterials)
        {
            if(formMaterials.CategoryName == "Приложение" ||
               formMaterials.CategoryName == "Презентация" ||
               formMaterials.CategoryName == "Другое")
            {
                //Создаем материал и сохраняем изменения в BD
                Material mt1;
                mt1 = new Material{MaterialDate = DateTime.Now, 
                    MaterialName = formMaterials.Name, 
                    CategoryType = formMaterials.CategoryName};
                //appDbContent.SaveChanges();
                //Создаем версию материала 
                MaterialVersion version = new MaterialVersion
                {
                    FileDate = DateTime.Now, 
                    Material = mt1, 
                    FileName = formMaterials.Name, 
                    Size = formMaterials.File.Length, 
                    PathOfFile = _dir
                };
                using (var fileStream = new FileStream(
                    Path.Combine(_dir,
                        $"{formMaterials.Name}{Path.GetExtension(formMaterials.File.FileName)}"),
                    FileMode.Create, 
                    FileAccess.Write))
                {
                    formMaterials.File.CopyTo(fileStream);
                }
                //После того как убедились, что у нас всё ок сохраняем в бд
                appDbContent.MaterialVersions.AddRange(new List<MaterialVersion>{version});
                appDbContent.SaveChanges();
                Guid GetUploadedMaterialId() => appDbContent.Materials.Where(m => m == mt1).SingleOrDefault().Id;
                return GetUploadedMaterialId();
            }
            else
            {
                return Guid.Empty;
            }
            
        }
        
    }
}