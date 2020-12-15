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
using WebApi5.Data.Models;
using WebApiOrkhan.Controllers.Models;
using WebApiOrkhan.Data;
using WebApiOrkhan.Data.Models;
//using File = WebApiOrkhan.Data.Models.Category;
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
        
        
        
        
        /*[HttpPost]*/
        public IActionResult FileInModel(FormForMaterials FormForMaterials)
        {
            /*Попытка реализовать добавление данных в БД, но тут я понял, что усложнил историю с таблицами
             буду переделывать таблички через миграции 
             */
            
            // создание и добавление моделей
            /*public class Player
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Position { get; set; }
                public int Age { get; set; }
 
                public int? TeamId { get; set; }
                public Team Team { get; set; }
            }
            public class Team
            {
                public int Id { get; set; }
                public string Name { get; set; } // название команды
 
                public ICollection<Player> Players { get; set; }
                public Team()
                {
                    Players = new List<Player>();
                }
            }*/
            /*Team t1 = new Team { Name = "Барселона" };
            Team t2 = new Team { Name = "Реал Мадрид" };
            db.Teams.Add(t1);
            db.Teams.Add(t2);
            db.SaveChanges();
            Player pl1 = new Player {Name = "Роналду", Age = 31, Position = "Нападающий", Team=t2 };
            Player pl2 = new Player { Name = "Месси", Age = 28, Position = "Нападающий", Team=t1 };
            Player pl3 = new Player { Name = "Хави", Age = 34, Position = "Полузащитник", Team=t1 };
            db.Players.AddRange(new List<Player>{pl1, pl2,pl3});
            db.SaveChanges();*/
            //Возможно стоит сделать инициализацию или получить через Linq конкретную категорию!!!!!
            //другой вариант это через свиткейс
            if(FormForMaterials.Name == "Приложение")
            {
                Material mt1;
                mt1 = new Material{material_date = DateTime.Now, 
                    material_name = FormForMaterials.Name, 
                    category_type = FormForMaterials.Name};
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

            
            
            
            
            
            /*public int id { set; get; }
            public string material_name { set; get; }
            public DateTime material_date { set; get; }
            public virtual Category Category { set; get; } */
            /*Material materialupload = new Material();
            materialupload.id = 4;
            materialupload.material_name = FormForMaterials.Name;
            materialupload.material_date = DateTime.Now;
            //materialupload.Category = Categories["Приложение"];
            //materialupload.Category = new Category();
            materialupload.Category.category_type = "Приложение";
            materialupload.Category.id = 1;
            appDBContent.Material.Add(materialupload);
            appDBContent.SaveChanges();
            appDBContent.Material.Add(
                new Material
                {
                    Category = {id = 1},
                    material_date = DateTime.Now,
                    material_name = FormForMaterials.Name
                });*/
            
            
               
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