using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BabaevTask5.Data.Interfaces;
using BabaevTask5.Data.Models;
using BabaevTask5.Controllers.Models;
using Microsoft.AspNetCore.Hosting;
//using static Category;

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
            /*if(type == "Другое" || type == "Приложение" || type == "Презентация")
                return appDbContent.Materials.Where(m => m.Category == type).ToList();*/
            return appDbContent.Materials.Where(m => m.CategoryId == Convert.ToInt16(type)).ToList();
        }

        public IEnumerable<MaterialVersion> GetAllVersions(Guid id) => appDbContent.MaterialVersions
            .Where(m => m.Id == id).ToList()
            .OrderByDescending(v => v.Material.Id == id);
        
        public string GetInfoAboutMaterial(Guid id)
        {
             int countOfVersion = appDbContent.MaterialVersions.Where(v => v.Material.Id == id).Count();
             //string lastUpdate = GetAllVersions(id).Select(v => v.FileDate).SingleOrDefault().ToString();
             var mat = appDbContent.Materials.Where(m => m.Id == id).SingleOrDefault();
             string lastUpdate = mat.MaterialVersions.Select(m => m.FileDate).SingleOrDefault().ToString();
             return ($"Count ov versions of material = {countOfVersion} \n" +
                     $"Last update = {lastUpdate}");
        }

        public Guid ChangeCetagoryOfMaterial(Guid mId, string type)
        {
            try
            {
                var mat = appDbContent.Materials.Where(m => m.Id == mId).FirstOrDefault();
                if (type == "Другое")
                    mat.CategoryId = Convert.ToInt16(MatCategory.Другое);
                else if (type == "Приложение")
                    mat.CategoryId = Convert.ToInt16(MatCategory.Приложение);
                else if (type == "Презентация")
                    mat.CategoryId = Convert.ToInt16(MatCategory.Презентация);
                appDbContent.SaveChanges();
                return mId;
            }
            catch 
            {
                Console.WriteLine("Error");
                return Guid.Empty;
            }
            /*var mat = appDbContent.Materials.Where(m => m.Id == mId).FirstOrDefault();
            mat.CategoryType = type;
            appDbContent.SaveChanges();
            return mId;*/
        }
        
        public Guid UploadNewMaterial(FormForMaterials formMaterials)
        {
            try
            {
                /*if(formMaterials.CategoryName == Convert.ToInt16(MatCategory.Другое) ||
                   formMaterials.CategoryName == Convert.ToInt16(MatCategory.Презентация) ||
                   formMaterials.CategoryName == Convert.ToInt16(MatCategory.Приложение))
                /*if(formMaterials.CategoryName == 0 ||
                   formMaterials.CategoryName == 1 ||
                   formMaterials.CategoryName == 2)*/
                if(formMaterials.CategoryName == Convert.ToInt16(MatCategory.Другое) ||
                   formMaterials.CategoryName == Convert.ToInt16(MatCategory.Презентация) ||
                   formMaterials.CategoryName == Convert.ToInt16(MatCategory.Приложение))
                {
                    //Создаем материал и сохраняем изменения в BD
                    Material mt1;
                    mt1 = new Material{MaterialDate = DateTime.Now, 
                        MaterialName = formMaterials.Name, 
                        CategoryId = formMaterials.CategoryName};
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
                    Console.WriteLine("Error incorect type of material");
                    return Guid.Empty;
                }
            }
            catch 
            {
                Console.WriteLine("error");
                return Guid.Empty;
            }
        }

        public FileModel GetFileParametrsForDownloadActualVersion(Guid mId)
        {
            FileModel file = new FileModel();
            IEnumerable<MaterialVersion> ActualList(Guid mId) => appDbContent.MaterialVersions.
                Where(m => m.Material.Id == mId).
                ToList().
                OrderByDescending(m =>m.FileDate);
            MaterialVersion ActualVersion = ActualList(mId).Select(m=> m).FirstOrDefault();
            file.fileName = ActualVersion.FileName;
            file.filePath = ActualVersion.PathOfFile + "/" + file.fileName + ".png";
            file.fileType = "application/png";
            
            return (file);
        }
    }
}