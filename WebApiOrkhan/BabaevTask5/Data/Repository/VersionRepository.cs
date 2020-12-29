/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BabaevTask5.Controllers.Models;
using BabaevTask5.Data.Interfaces;
using BabaevTask5.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BabaevTask5.Data.Repository
{
    public class VersionRepository : IVersion
    {
        private IWebHostEnvironment _env;
        private string _dir;
        private readonly AppDbContent appDbContent;
        


        public VersionRepository(IWebHostEnvironment env, AppDbContent appDbContent)
        {
            _env = env;
            _dir = _env.ContentRootPath  + "/MaterialStorage";
            this.appDbContent = appDbContent;
        }
        
        public string GetInfoAboutMaterialVersion
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public IOrderedEnumerable<MaterialVersion> FilterVersionsByDate
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public IOrderedEnumerable<MaterialVersion> FilterVersionsBySize
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public IOrderedEnumerable<MaterialVersion> FilterVersionsByType
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public IActionResult UploadNewVersionOfMaterial(FormForVersion formForVersion)
        {
            Data.Models.MaterialVersion f1 = new MaterialVersion
            {
                FileDate = DateTime.Now,
                FileName = formForVersion.Name,
                PathOfFile = _dir,
                Size = FormForVersion.File.Length,
                material = appDBContent.Materials.Where(m => m.id == FormForVersion.materialId).ToList()[0]////////
            };
            //вроде я должен был здесь добавить материал в БД, но когда
            //делал забыл, но всё равно работает, видимо из-за того что добавляю позже при создании файла
            appDBContent.SaveChanges();
                
            appDBContent.Files.AddRange(new List<File>{f1});
            appDBContent.SaveChanges();
         
            using (var fileStream = new FileStream(
                Path.Combine(_dir,
                    $"{FormForVersion.Name}_version{Path.GetExtension(FormForVersion.File.FileName)}"),
                FileMode.Create, 
                FileAccess.Write))
            {
                FormForVersion.File.CopyTo(fileStream);
            }
            return RedirectToAction("UploadVersion");
        }

        public IActionResult UploadVersion() => View();
        }
        

        public PhysicalFileResult DownloadFirstVersionByMaterialId
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public PhysicalFileResult DownloadLastVersionByMaterialId
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public PhysicalFileResult DownloadConcreteVersionByMaterialIdByVersionId
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }
    }
}*/