using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Task3.API.Resources;
using Task3.API.Validations;
using Task3.Core.DTO;
using Task3.Core.Models;
using Task3.Core.Services;

namespace Task3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialServices _materialService;
        private readonly IMapper _mapper;

        public MaterialController(IMaterialServices materialService, IMapper mapper)
        {
            this._mapper = mapper;
            this._materialService = materialService;
        }

        [HttpGet("/api/materials/date")]
        public async Task<ActionResult<IEnumerable<MaterialResultDto>>> GetMaterialsByDate()
        {
            var materials = await _materialService.GetFilterMaterialsByDate();
            var materialResultDto = _mapper.
                Map<IEnumerable<Material>, IEnumerable<MaterialResultDto>>(materials);
            return Ok(materialResultDto);
        }

        [HttpPost("/api/material/upload")]
        public async Task<ActionResult<MaterialResultDto>> UploadedMaterial([FromForm] UploadMaterialDTO uploadMaterialForm)
        {
            var validator = new SaveMaterialValidator();
            var validationResult = await validator.ValidateAsync(uploadMaterialForm);
            if (!validationResult.IsValid)
                return BadRequest("Not valid data");
            var material = await _materialService.UploadNewMaterial(uploadMaterialForm);
            var materialResultDto = _mapper.
                Map<Material, MaterialResultDto>(material);
            return Ok(materialResultDto);
        }

        [Route("/api/material/{mId}/download")]
        [HttpPost]
        public async Task<ActionResult> DownloadMaterial(Guid mId)
        {
            var fileData = await _materialService.GetDtoForDownloadMaterialAsync(mId);
            return File(fileData.mas, fileData.fileType, fileData.fileName);
        }
        
        [Route("/api/{mId}/versions/date/")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialVersionResultDto>>>  GetVersionsOrdergniByDate(Guid mId)
        {
            var versions = await  _materialService.FilterVersionsByDate(mId);
            var materialVersionResultDto = _mapper.
                Map<IEnumerable<MaterialVersion>, IEnumerable<MaterialVersionResultDto>>(versions);
            return Ok(materialVersionResultDto);
        }
        
        [Route("/api/{mId}/versions/syze/")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialVersionResultDto>>>  GetVersionsOrdergniBySyze(Guid mId)
        {
            var versions = await  _materialService.FilterVersionsBySize(mId);
            var materialVersionResultDto = _mapper.
                Map<IEnumerable<MaterialVersion>, IEnumerable<MaterialVersionResultDto>>(versions);
            return Ok(materialVersionResultDto);
        }

        [Route("/api/version/upload/")]
        [HttpPost]
        public async Task<ActionResult> UploadNewVersionOfMaterial([FromForm] UploadMaterialVersionDTO materialVersionform)
        {
            var versionOfMaterial = await _materialService.UploadNewMaterialVersion(materialVersionform);
            return Ok(versionOfMaterial);
        }
        
        [Route("/api/version/{vId}/download")]
        [HttpPost]
        public async Task<ActionResult> DownloadVersionOfMaterial(Guid vId)
        {
            var fileData = await _materialService.GetMaterialVersionFile(vId);
            return File(fileData.mas, fileData.fileType, fileData.fileName);
        }

    }
}