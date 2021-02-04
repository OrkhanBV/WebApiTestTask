using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Task3.Core.DTO;
using Task3.Core.Models;
using Task3.Core.Services;

namespace Task3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialVersionController : ControllerBase
    {
        private readonly IMaterialVersionServices _versionService;
        private readonly IMapper _mapper;

        public MaterialVersionController(IMaterialVersionServices versionService, IMapper mapper)
        {
            this._mapper = mapper;
            this._versionService = versionService;
        }
        
        [Route("GetVersionsByDate")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialVersion>>>  GetVersionsOrdergniByDate(Guid mId)
        {
            var versions = await  _versionService.FilterVersionsByDate(mId);
            return Ok(versions);
        }
        
        [Route("GetVersionsBySyze")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialVersion>>>  GetVersionsOrdergniBySyze(Guid mId)
        {
            var versions = await  _versionService.FilterVersionsBySize(mId);
            /*var versionResult = new*/
            var versionsRes = versions;
            
            return Ok(versionsRes);
            //return Ok(versions);
        }

        [Route("UploadVersion")]
        [HttpPost]
        public async Task<ActionResult> UploadNewVersionOfMaterial([FromForm] UploadMaterialVersionDTO materialVersionform)
        {
            var versionOfMaterial = await _versionService.UploadNewMaterialVersion(materialVersionform);
            return Ok(versionOfMaterial);
        }
        
        [Route("DownloadVersion")]
        [HttpPost]
        public async Task<ActionResult> DownloadVersionOfMaterial(Guid vId)
        {
            var fileData = await _versionService.GetMaterialVersionFile(vId);
            byte[] mas = System.IO.File.ReadAllBytes(fileData.filePath);
            return File(mas, fileData.fileType, fileData.fileName);
        }
    }
}