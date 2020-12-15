using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiOrkhan.Data;
using WebApiOrkhan.Data.Models;

/*This controller allow us to get all file versions by ID of material*/
/*Usage 1 parametr https://localhost:5001/Material/AllFiles/?id=1
        2 parametrs pattern = http://mysite.ru/?a=2&b=3             */

namespace WebApiOrkhan.Controllers
{
    [ApiController]
    [Route("/Material/AllFiles")]
    public class AllFilesOfMaterialByIdController : ControllerBase
    {
        private readonly AppDBContent appDBContent;

        public AllFilesOfMaterialByIdController(AppDBContent appDbContent)
        {
            this.appDBContent = appDbContent;
        }
        
        
        public IEnumerable<File> OrderAllFilesOfMaterial(int mId) =>
            appDBContent.Files.Where(m => m.material.id == mId).ToList();

        [HttpGet]
        public IEnumerable<File> Get(int mId)
        {
           
            if (appDBContent.Materials.Count() >= mId & mId != 0)
                return OrderAllFilesOfMaterial(mId);
            else
            { 
                return null; //Как правильно обработать событие в таком случае??
            }
        }
        
        /*
        ПРИМЕР СОРТИРОВКИ ПО ДАТЕ
        public IEnumerable<File> OrderAllFilesOfMaterial(int mId) =>
            appDBContent.Files.Where(m => m.material.id == mId).ToList().
                OrderByDescending(f => f.file_date);
        */
        
        /*public IEnumerable<string> NameFile()
        {
            return (GetFileById(1).Select(f => f.file_name));
        }*/
        
        /*public string NameFile()
        {
            return (GetFileById(1).Select(f => f.file_name).ToList()[0]);
        }*/
        
        /*public string NameFile(int materialId, int fileId)
        {
            return (GetAllFileInMaterial(materialId).Select(f => f.file_name).ToList()[fileId]);
        }*/
    }
}
