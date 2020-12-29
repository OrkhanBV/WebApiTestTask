using System;
using System.Collections.Generic;
using System.Linq;
using BabaevTask5.Controllers.Models;
using BabaevTask5.Data;
using BabaevTask5.Data.Interfaces;
using BabaevTask5.Data.Models;
using BabaevTask5.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BabaevTask5.Controllers
{
    [Route("/Material")]
    public class MaterialController : Controller
    {
        
        private readonly IMaterialHandler _materialHandler;
        public IActionResult Indexupload() => View();

        public MaterialController(IMaterialHandler iMaterialHandler)
        {
            _materialHandler = iMaterialHandler;
        }

        [HttpGet("ByDate")]
        public IEnumerable<Material> ShowMaterialsByDate()
        {
            return _materialHandler.FilterMaterialsByDate;
        }
        
        /*Пример https://localhost:5001/Material/ByType?type=другое*/
        /*Переделать CategoryType в ENUM in BD*/
        [HttpGet("ByType")]
        public IEnumerable<Material> ShowMaterialsByType(string type)
        {
            return _materialHandler.FilterMaterialByType(type);
        }

        [HttpGet("Info")]
        public string ShowInfo(Guid id)
        {
            return _materialHandler.GetInfoAboutMaterial(id);
        }

        [HttpPost]
        public IActionResult UploadMaterial(FormForMaterials formForMaterials)
        {
            _materialHandler.UploadNewMaterial(formForMaterials);
            return RedirectToAction("Indexupload");
        }
        
    }
    
}