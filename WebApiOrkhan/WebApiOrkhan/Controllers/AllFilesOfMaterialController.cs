using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi5.Data.Models;
using WebApiOrkhan.Data;
using WebApiOrkhan.Data.Models;

/*This controller allow us to get all file versions by ID of material*/
/*Usage https://localhost:5001/Material/AllFiles/?id=1*/

namespace WebApiOrkhan.Controllers
{
    [ApiController]
    [Route("/Material/AllFiles")]
    public class AllFilesOfMaterialController : ControllerBase
    {
        private readonly AppDBContent appDBContent;

        public AllFilesOfMaterialController(AppDBContent appDbContent)
        {
            this.appDBContent = appDbContent;
        }
        public IEnumerable<File> GetAllFileInMaterial(int id) => appDBContent.File.Where(c => c.material.id == id).ToList();
        [HttpGet]
        public IEnumerable<File> Get(int id)
        {
            /*Из-за условия которое я написал ниже для защиты установки
            не входящего в диапозон id возникает стронное поведение браузера*/
            if (appDBContent.Material.Count() >= id & id != 0)
                return GetAllFileInMaterial(id);
            else
            {
                return null;
            }
        }
    }
}
