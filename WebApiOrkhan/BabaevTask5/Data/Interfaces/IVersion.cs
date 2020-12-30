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
        /*Filters for versions*/
        /*public IOrderedEnumerable<MaterialVersion> FilterVersionsByDate { get; set; }
        public IOrderedEnumerable<MaterialVersion> FilterVersionsBySize { get; set; }
        public IOrderedEnumerable<MaterialVersion> FilterVersionsByType { get; set; }//*/
        
        /*Upload & Download version of material*/
        public Guid UploadNewVersionOfMaterial(FormForVersion formForVersion);
        public FileModel GetFileParametrsForDownload(Guid vId);
    }
}