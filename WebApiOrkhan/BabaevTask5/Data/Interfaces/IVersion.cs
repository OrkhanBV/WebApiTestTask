using System;
using System.Collections.Generic;
using System.Linq;
using BabaevTask5.Controllers.Models;
using BabaevTask5.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabaevTask5.Data.Interfaces
{
    public interface IVersion
    {
        /*Info for versions*/
        public string GetInfoAboutMaterialVersion { get; set; }
        
        /*Filters for versions*/
        public IOrderedEnumerable<MaterialVersion> FilterVersionsByDate { get; set; }
        public IOrderedEnumerable<MaterialVersion> FilterVersionsBySize { get; set; }
        public IOrderedEnumerable<MaterialVersion> FilterVersionsByType { get; set; }//
        
        /*Upload & Download version of material*/
        /*public IActionResult UploadNewVersionOfMaterial(FormForVersion formForVersion);*/
        public Guid UploadNewVersionOfMaterial(FormForVersion formForVersion);
        /*public PhysicalFileResult DownloadFirstVersionByMaterialId { get; set; }
        public PhysicalFileResult DownloadLastVersionByMaterialId { get; set; }
        public PhysicalFileResult DownloadConcreteVersionByMaterialIdByVersionId { get; set; }*/
    }
}