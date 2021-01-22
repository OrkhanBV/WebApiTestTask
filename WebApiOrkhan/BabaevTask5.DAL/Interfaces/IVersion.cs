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
        public List<MaterialVersion> FilterVersionsByDate(Guid mId);
        public List<MaterialVersion> FilterVersionsBySize(Guid mId);

        /*Upload & Download version of material*/
        public Guid UploadNewVersionOfMaterial(FormForVersion formForVersion);
        public FileModel GetFileParametrsForDownload(Guid vId);
    }
}