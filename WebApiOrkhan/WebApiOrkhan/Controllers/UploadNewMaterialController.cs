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
    [ApiController]
    [Route("/Material/Upload")]
    public class UploadMaterialController : Controller
    {
        //private IHostingEnvironment _env;
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
            //заменить на кейсы 
            if(FormForMaterials.CategoryName == "Приложение" ||
               FormForMaterials.CategoryName == "Презентация" ||
               FormForMaterials.CategoryName == "Другое")
            {
                //Создаем материал и сохраняем изменения
                Material mt1;
                mt1 = new Material{material_date = DateTime.Now, 
                    material_name = FormForMaterials.Name, 
                    category_type = FormForMaterials.CategoryName};
                //вроде я должен был здесь добавить материал в БД, но когда
                //делал забыл, но всё равно работает, видимо из-за того что добавляю позже при создании файла
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
                /*Path.Combine(_dir,
                    $"{FormForMaterials.Name}{FormForMaterials.File.FileName.Substring(FormForMaterials.File.FileName.LastIndexOf('.'))}"), //path.GetExtension() <-плохо работает с формами
                FileMode.Create, 
                FileAccess.Write))*/
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