using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using WebApiOrkhan.Controllers.Models;

/*Еще не определился с реализацией либо
через модель(с возможностью назвать файл, задать тип файла и т.д.)
либо чисто файл*/

namespace WebApiOrkhan.Controllers
{
    /*[ApiController]
    [Route("/Material/Upload")]*/
    public class UploadMaterialController : Controller
    {
        private IHostingEnvironment _env;
        private string _dir;
        
        
        public UploadMaterialController(IHostingEnvironment env)
        {
            _env = env;
            _dir = _env.ContentRootPath;
        }
        public IActionResult Indexupload() => View();
        
        /*[HttpPost]*/
        public IActionResult FileInModel(FormForMaterials FormForMaterials)
        {
            using (var fileStream = new FileStream(Path.Combine(_dir, $"{FormForMaterials.Name}.png"), 
                FileMode.Create, 
                FileAccess.Write))
            {
                FormForMaterials.File.CopyTo(fileStream);
            }
            return RedirectToAction("Indexupload");
        }
        
        //ИЛИ
        
        public IActionResult SingleFile(IFormFile file)
        {
            var dir = _env.ContentRootPath;
            using (var fileStream = new FileStream(Path.Combine(_dir, "file.png"), FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }
            return RedirectToAction("Indexupload");
        }
        
    }
}