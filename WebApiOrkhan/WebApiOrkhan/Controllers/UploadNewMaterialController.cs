using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
//using WebApi5.Data.Models;
using WebApiOrkhan.Controllers.Models;
using WebApiOrkhan.Data;
using WebApiOrkhan.Data.Models;
using File = WebApiOrkhan.Data.Models.File;

namespace WebApiOrkhan.Controllers
{
    /*[ApiController]
    [Route("/Material/Upload")]*/
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
        
        /*[HttpPost]*/
        public IActionResult FileInModel(FormForMaterials FormForMaterials)
        {
            if(FormForMaterials.CategoryName == "Приложение" ||
               FormForMaterials.CategoryName == "Презентация" ||
               FormForMaterials.CategoryName == "Другое")
            {
                Material mt1;
                mt1 = new Material{material_date = DateTime.Now, 
                    material_name = FormForMaterials.Name, 
                    category_type = FormForMaterials.CategoryName};
                appDBContent.SaveChanges();
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
            using (var fileStream = new FileStream(Path.Combine(_dir, $"{FormForMaterials.Name}.pdf"), 
                FileMode.Create, 
                FileAccess.Write))
            {
                FormForMaterials.File.CopyTo(fileStream);
            }
            return RedirectToAction("Indexupload");
        }
    }
}