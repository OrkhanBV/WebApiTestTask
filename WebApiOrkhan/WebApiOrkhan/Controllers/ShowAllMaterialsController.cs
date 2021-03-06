using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApiOrkhan.Data.Models;
using WebApiOrkhan.Data;

namespace WebApiOrkhan.Controllers
{
    [ApiController]
    [Route("/Materials/ShowAll")]
    public class ShowAllMaterialsController : ControllerBase
    {
        private readonly AppDBContent appDBContent;

        public ShowAllMaterialsController(AppDBContent appDbContent)
        {
            this.appDBContent = appDbContent;
        }

        public IEnumerable<Material> GetAllMaterials => appDBContent.Materials.ToList();
        
        [HttpGet]
        public IEnumerable<Material> Get()
        {
            return GetAllMaterials;
        }
    }
}