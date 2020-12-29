using System.Collections.Generic;
using System.Linq;
using BabaevTask5.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabaevTask5.Data.Interfaces
{
    public interface IVersionHandler
    {
        /*Info for versions*/
        public string GetInfoAboutMaterialVersion { get; set; }
        
        /*Filters for versions*/
        public IOrderedEnumerable<MaterialVersion> FilterVersionsByDate { get; set; }
        public IOrderedEnumerable<MaterialVersion> FilterVersionsBySize { get; set; }
        public IOrderedEnumerable<MaterialVersion> FilterVersionsByType { get; set; }//
        
        /*Upload & Download version of material*/
        public IActionResult UploadNewVersionOfMaterial { get; set; }
        public PhysicalFileResult DownloadFirstVersionByMaterialId { get; set; }
        public PhysicalFileResult DownloadLastVersionByMaterialId { get; set; }
        public PhysicalFileResult DownloadConcreteVersionByMaterialIdByVersionId { get; set; }
    }
}