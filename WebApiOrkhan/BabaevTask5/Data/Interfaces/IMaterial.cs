using System;
using System.Collections.Generic;
using System.Linq;
using BabaevTask5.Controllers.Models;
using BabaevTask5.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabaevTask5.Data.Interfaces
{
    public interface IMaterial
    {
        public IOrderedEnumerable<Material> FilterMaterialsByDate { get; set; }
        public List<Material> FilterMaterialByType(string type);
        public string GetInfoAboutMaterial(Guid id);
        public Guid UploadNewMaterial(FormForMaterials formForMaterials);
        public Guid ChangeCetagoryOfMaterial(Guid mId, string type);
        public FileModel GetFileParametrsForDownloadActualVersion(Guid mId);
    }
}