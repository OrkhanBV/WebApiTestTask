using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
            return Ok(versions);
        }

        [Route("UploadVersion")]
        [HttpGet]
        public async Task<ActionResult> UploadNewVersionOfMaterial(UploadMaterialVersionDTO materialVersionform)
        {
            var versionOfMaterial = await _versionService.UploadNewMaterialVersion(materialVersionform);
            return Ok(versionOfMaterial);
        }
        /*
        public async Task<MaterialVersion> UploadNewMaterialVersion(UploadMaterialVersionDTO materialVersionform)
        {
            MaterialVersion uploadedVersion = new MaterialVersion
            {
                FileDate = DateTime.Now,
                FileName = materialVersionform.Name,
                PathOfFile = _dir,
                Size = materialVersionform.File.Length,
                Material = await _unitOfWork.Materials.GetMaterialById(materialVersionform.MaterialId)
            };
            using (var fileStream = new FileStream(
                Path.Combine(_dir,
                    $"{materialVersionform.Name}{Path.GetExtension(materialVersionform.File.FileName)}"),
                FileMode.Create,
                FileAccess.Write))
            {
                materialVersionform.File.CopyTo(fileStream);
            }

            await _unitOfWork.MaterialVersions.AddRangeAsync(new List<MaterialVersion> {uploadedVersion});
            await _unitOfWork.CommitAsync();
            return uploadedVersion;
        }

        public Task<MaterialVersion> DownloadMaterialVersion(DownloadFileDTO materialId)
        {
            */
        throw new NotImplementedException();
        }
    }
}