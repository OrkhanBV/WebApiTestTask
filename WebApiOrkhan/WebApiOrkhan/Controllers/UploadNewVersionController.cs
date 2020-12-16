/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApiOrkhan.Controllers.Models;
using WebApiOrkhan.Data;
using WebApiOrkhan.Data.Models;
using File = WebApiOrkhan.Data.Models.File;*/

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApiOrkhan.Controllers.Models;
using WebApiOrkhan.Data;
using System.Linq;
using WebApiOrkhan.Data.Models;
using File = WebApiOrkhan.Data.Models.File;

namespace WebApiOrkhan.Controllers
{
    public class UploadVersionController : Controller
    {
        //private IHostingEnvironment _env;
            private IWebHostEnvironment _env;
            private string _dir;
            private readonly AppDBContent appDBContent;
        


        public UploadVersionController(IWebHostEnvironment env, AppDBContent appDbContent)
        {
            _env = env;
            _dir = _env.ContentRootPath  + "/AppStorage";
            this.appDBContent = appDbContent;
        }
        
        /*public IActionResult UploadVersion => View();*/
        
        [HttpPost]
        public IActionResult FileInModelVersion(FormForVersion FormForVersion)
        {
            Data.Models.File f1 = new File
                {
                    file_date = DateTime.Now,
                    file_name = FormForVersion.Name,
                    path_of_file = _dir,
                    size = FormForVersion.File.Length,
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
    
}