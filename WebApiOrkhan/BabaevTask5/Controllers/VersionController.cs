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
        public PhysicalFileResult DownloadConcreteVersion(Guid vId)
        {
            FileModel file = _iVersion.GetFileParametrsForDownload(vId);
            return PhysicalFile(file.filePath, file.fileType, file.fileName);
        }
        
    }
}