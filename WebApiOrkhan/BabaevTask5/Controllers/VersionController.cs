using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using BabaevTask5.Controllers.Models;
using BabaevTask5.DAL.Interfaces;
using BabaevTask5.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/*
 * 1)Разобраться в инъекциях
 * 2)Как сделать так чтобы контроллеры возвращали некую универсальную прослойку
 * 3)Как правильно обрабатывать ошибки и что возвращать
 * 4)Связать типы Материалы в Enum
 * 5)Исследовать вопрос с мапперами
 * 6)Доделать контроль ролей
 */

namespace BabaevTask5.Controllers
{
    /*[Authorize(Roles="admin")]*/
    [Route("/Version")]
    public class VersionController : Controller
    {
        private readonly IVersion _iVersion;
        public IActionResult UploadVersion() => View();

        public VersionController(IVersion iVersion)
        {
            _iVersion = iVersion;
        }

        [HttpGet("ByDate")]
        public List<MaterialVersion> FilterByDate(Guid mId)
        {
            return _iVersion.FilterVersionsByDate(mId);
        }
        
        [HttpGet("BySize")]
        public List<MaterialVersion> FilterBySize(Guid mId)
        {
            return _iVersion.FilterVersionsBySize(mId);
        }

        [HttpPost]
        /*[Authorize(Roles = "admin")]*/
        public IActionResult UploadVersion(FormForVersion formForVersion)
        {
            if (_iVersion.UploadNewVersionOfMaterial(formForVersion) == Guid.Empty)
                return BadRequest("Error");
            return RedirectToAction("UploadVersion");
        }
        
        [HttpGet("Download")]
        /*[Authorize(Roles = "admin")]*/
        public PhysicalFileResult DownloadConcreteVersion(Guid vId)
        {
            FileModel file = _iVersion.GetFileParametrsForDownload(vId);
            return PhysicalFile(file.filePath, file.fileType, file.fileName);
        }
        
    }
}