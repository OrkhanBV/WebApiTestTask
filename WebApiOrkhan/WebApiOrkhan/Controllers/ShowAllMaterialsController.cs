using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi5.Data.Models;
using WebApiOrkhan.Data;

namespace WebApiOrkhan.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ShowAllMaterialsController : ControllerBase
    {
        private readonly AppDBContent appDBContent;

        public ShowAllMaterialsController(AppDBContent appDbContent)
        {
            this.appDBContent = appDbContent;
        }

        public IEnumerable<Material> GetAllMaterials => appDBContent.Material.ToList();
        
        [HttpGet]
        public IEnumerable<Material> Get()
        {
            return GetAllMaterials;
        }
    }
}

/*namespace WebApi5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetFilesController : ControllerBase
    {
        private readonly AppDBContent appDBContent;

        public GetFilesController(AppDBContent appDBContent) {
            this.appDBContent = appDBContent;
        }
        
        //public IEnumerable<File> GetAllVersionFiles => appDBContent.File.ToList();
        public IEnumerable<File> GetAllVersionFiles =>
            appDBContent.File.Where(c => c.CategoryW.categoryType == "Презентация").ToList();
        /*public IEnumerable<File> GetAllVersionFiles =>
            appDBContent.File.Where(c=> c.CategoryW.Id == 2).ToList();#1#
        [ HttpGet]
        public IEnumerable<File> Get()
        {
            return GetAllVersionFiles;
        }
    }
}*/