using System;
using System.IO;
using System.IO.Enumeration;
using BabaevTask5.Controllers.Models;
using BabaevTask5.Data.Interfaces;
using BabaevTask5.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabaevTask5.Controllers
{
    [Route("/Version")]
    public class VersionController : Controller
    {
        private readonly IVersion _iVersion;
        public IActionResult UploadVersion() => View();

        public VersionController(IVersion iVersion)
        {
            _iVersion = iVersion;
        }

        [HttpPost]
        public IActionResult UploadVersion(FormForVersion formForVersion)
        {
            if (_iVersion.UploadNewVersionOfMaterial(formForVersion) == Guid.Empty)
                return BadRequest("Error");
            return RedirectToAction("UploadVersion");
        }
        
        [HttpGet("Download")]
        public PhysicalFileResult DownloadConcreteVersion(/*Guid mId, */Guid vId)
        {
            /*var versionId = _iVersion.DownloadConcreteVersionByMaterialIdByVersionId(formForVersion);
            if (versionId == Guid.Empty)
                return BadRequest("Error");
            return versionId;*/
            /*MaterialVersion GetOfMaterialVersions(Guid mId, Guid vId) =>
                appDbContent.MaterialVersions.Where(m => m.Material.Id == mId && m.Id == vId).SingleOrDefault();

            string fileType = "aplication/" + $"{Path.GetExtension(GetOfMaterialVersions(mId, vId).FileName)}";
            string fileName = GetOfMaterialVersions(mId, vId).FileName;
            string filePath = Path.Combine(_env.ContentRootPath,
                "MaterialStorage/" + fileName + Path.GetExtension(GetOfMaterialVersions(mId, vId).FileName));*/
            FileModel file = _iVersion.GetFileParametrsForDownload(/*mId, */vId);
            string fileType = file.fileType;
            string fileName = file.fileName;
            string filePath = file.filePath;
            return PhysicalFile(filePath, fileType, fileName);
        }
        
    }
}