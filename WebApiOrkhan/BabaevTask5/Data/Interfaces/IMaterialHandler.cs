using System;
using System.Collections.Generic;
using System.Linq;
using BabaevTask5.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabaevTask5.Data.Interfaces
{
    /*public class IMaterialHandler<T> : IDisposable where T : class */
    public interface IMaterialHandler
    {
        /*Info about Material*/
        /*public string GetInfoAboutMaterial { get; set; }*/
        /*Filter for Materials*/
        public IOrderedEnumerable<Material> FilterMaterialsByDate { get; set; }
        /*public IOrderedEnumerable<Material> FilterMaterialsBySize { get; set; }
        public IOrderedEnumerable<Material> FilterMaterialByType { get; set; }*/
        /*Upload new material & Download Material(actual version of)*/
        //public IActionResult UploadNewMaterial { get; set; }
        //public PhysicalFileResult DownloadMaterialById { get; set; } //возможно и не нужно , так как можем обойтись скачкой конкретной версии
        
        
        /*public IEnumerable<Material> FilterOnlyFirstVersionMaterial { get; set; }//*/
        
    }
}