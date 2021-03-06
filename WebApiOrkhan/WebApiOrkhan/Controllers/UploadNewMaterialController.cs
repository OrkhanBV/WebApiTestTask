using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApiOrkhan.Controllers.Models;
using WebApiOrkhan.Data;
using WebApiOrkhan.Data.Models;
using File = WebApiOrkhan.Data.Models.File;

/*
 
 Реализацию один ко многим разобрал на сайте,
    https://metanit.com/sharp/entityframework/3.7.php
    
*/

namespace WebApiOrkhan.Controllers
{
    /*[ApiController]
    [Route("/Material/Upload")]*/
    public class UploadMaterialController : Controller
    {
        private IWebHostEnvironment _env;
        private string _dir;
        private readonly AppDBContent appDBContent;
        public UploadMaterialController(IWebHostEnvironment env, AppDBContent appDbContent)
        {
            _env = env;
            _dir = _env.ContentRootPath  + "/AppStorage";
            this.appDBContent = appDbContent;
        }
        public IActionResult Indexupload() => View();
        
        [HttpPost]
        public IActionResult FileInModel(FormForMaterials FormForMaterials)
        {
            //заменить на Enum
            if(FormForMaterials.CategoryName == "Приложение" ||
               FormForMaterials.CategoryName == "Презентация" ||
               FormForMaterials.CategoryName == "Другое")
            {
                //Создаем материал и сохраняем изменения в BD
                Material mt1;
                mt1 = new Material{material_date = DateTime.Now, 
                    material_name = FormForMaterials.Name, 
                    category_type = FormForMaterials.CategoryName};
                appDBContent.SaveChanges();
                //Создаем файл и
                File file = new File
                {
                    file_date = DateTime.Now, 
                    material = mt1, 
                    file_name = FormForMaterials.Name, 
                    size = FormForMaterials.File.Length, 
                    path_of_file = _dir
                };
                appDBContent.Files.AddRange(new List<File>{file});
                appDBContent.SaveChanges();
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