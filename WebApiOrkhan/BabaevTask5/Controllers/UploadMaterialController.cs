using System;
using System.Collections.Generic;
using System.IO;
using BabaevTask5.Controllers.Models;
using BabaevTask5.Data;
using BabaevTask5.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BabaevTask5.Controllers
{
    public class UploadMaterialController : Controller
    {
        
        private AppDbContent appDbContent;
        private IWebHostEnvironment _env;
        private string _dir;

        public UploadMaterialController(AppDbContent appDbContent, IWebHostEnvironment env)
        {
            this.appDbContent = appDbContent;
            _env = env;
            _dir = env.ContentRootPath + "/MaterialStorage";
        }
        
        public IActionResult Indexupload() => View();
        public IActionResult UploadNewMaterial(FormForMaterials FormForMaterials)
        {
            if(FormForMaterials.CategoryName == "Приложение" ||
               FormForMaterials.CategoryName == "Презентация" ||
               FormForMaterials.CategoryName == "Другое")
            {
                //Создаем материал и сохраняем изменения в BD
                Material mt1;
                mt1 = new Material{MaterialDate = DateTime.Now, 
                    MaterialName = FormForMaterials.Name, 
                    CategoryType = FormForMaterials.CategoryName};
                appDbContent.SaveChanges();
                //Создаем файл и
                MaterialVersion version = new MaterialVersion
                {
                    FileDate = DateTime.Now, 
                    Material = mt1, 
                    FileName = FormForMaterials.Name, 
                    Size = FormForMaterials.File.Length, 
                    PathOfFile = _dir
                };
                appDbContent.MaterialVersions.AddRange(new List<MaterialVersion>{version});
                appDbContent.SaveChanges();
            }
            else
            {
                return BadRequest("WRONG TYPE OF CATEGORY");
            }
            using (var fileStream = new FileStream(
                Path.Combine(_dir,
                    $"{FormForMaterials.Name}{Path.GetExtension(FormForMaterials.File.FileName)}"),
                FileMode.Create, 
                FileAccess.Write))
            {
                FormForMaterials.File.CopyTo(fileStream);
            }
            return RedirectToAction("Indexupload");
            
        }
    }
}