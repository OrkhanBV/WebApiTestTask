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
        
        private readonly IMaterial _imaterial;
        public IActionResult Indexupload() => View();

        public MaterialController(IMaterial iMaterial)
        {
            _imaterial = iMaterial;
        }

        [HttpGet("ByDate")]
        public IEnumerable<Material> ShowMaterialsByDate()
        {
            return _imaterial.FilterMaterialsByDate;
        }
        
        /*Пример https://localhost:5001/Material/ByType?type=другое*/
        /*Переделать CategoryType в ENUM in BD*/
        [HttpGet("ByType")]
        public IEnumerable<Material> ShowMaterialsByType(string type)
        {
            return _imaterial.FilterMaterialByType(type);
        }
        //пример https://localhost:5001/Material/Info/?id=86306de9-b97c-4c6c-b94a-7bfa61fccb0f
        [HttpGet("Info")]
        public string ShowInfo(Guid id)
        {
            return _imaterial.GetInfoAboutMaterial(id);
        }

        [HttpPost]
        public IActionResult UploadMaterial(FormForMaterials formForMaterials)
        {
            _imaterial.UploadNewMaterial(formForMaterials);
            return RedirectToAction("Indexupload");
        }
        
    }
    
}
