using System;
using System.Collections.Generic;
using System.Linq;
using BabaevTask5.Controllers.Models;
using BabaevTask5.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabaevTask5.Data.Interfaces
{
    /*public class IMaterialHandler<T> : IDisposable where T : class */
    public interface IMaterial
    {
        /*Filter for Materials*/
        public IOrderedEnumerable<Material> FilterMaterialsByDate { get; set; }
        public List<Material> FilterMaterialByType(string type);
        /*Info about Material*/
        public string GetInfoAboutMaterial(Guid id);
        /*Upload new material & Download Material(actual version of)*/
        public IActionResult UploadNewMaterial(FormForMaterials formMaterials);
        //public Guid UploadNewMaterial(FormForMaterials formForMaterials);
    }
}