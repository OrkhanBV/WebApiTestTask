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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using WebApi5.Data.Models;
using WebApiOrkhan.Controllers.Models;
using WebApiOrkhan.Data;
using WebApiOrkhan.Data.Models;
using File = WebApiOrkhan.Data.Models.File;

/*Еще не определился с реализацией либо
через модель(с возможностью назвать файл, задать тип файла и т.д.)
либо чисто файл*/

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
        
        
        /*Пример как записывать в БД
            db.TruckTable.AddObject(trucktbl);
            db.SaveChanges();
         */
        
        
        /*[HttpPost]*/
        public IActionResult FileInModel(FormForMaterials FormForMaterials)
        {
            /*Попытка реализовать добавление данных в БД, но тут я понял, что усложнил историю с таблицами
             буду переделывать таблички через миграции 
             1)нужно переделать табличку с материалами оставить 
                - убрать список файлов
                - возможно ссылку на таблицу с категориями (не уверен)
                - тип категории лишняя информация
                - категори тайп
             */
            appDBContent.Material.Add(
                new Material
                {
                    Category = {id = 1},
                    material_date = DateTime.Now,
                    material_name = FormForMaterials.Name
                });
        
               
            using (var fileStream = new FileStream(Path.Combine(_dir, $"{FormForMaterials.Name}.pdf"), 
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