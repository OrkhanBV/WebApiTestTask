using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterialsByDate()
        {
            var materials = await _materialService.GetFilterMaterialsByDate();
            var materialResultDto = _mapper.
                Map<IEnumerable<Material>, IEnumerable<MaterialResultDto>>(materials);
            return Ok(materialResultDto);
        }

        [HttpPost("")]
        public async Task<MaterialResultDto> UploadedMaterial([FromForm] UploadMaterialDTO uploadMaterialForm)
        {
            var validator = new SaveMaterialValidator();
            var validationResult = await validator.ValidateAsync(uploadMaterialForm);
            /*if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);*/
            var material = await _materialService.UploadNewMaterial(uploadMaterialForm);
            var materialResultDto = _mapper.
                Map<Material, MaterialResultDto>(material);
            //return Ok(material);
            return materialResultDto;
        }

        [Route("GetMat")]
        [HttpPost]
        public async Task<ActionResult> DownloadMaterial(Guid mId)
        {
            var fileData = await _materialService.GetDtoForDownloadMaterialAsync(mId);
            byte[] mas = System.IO.File.ReadAllBytes(fileData.filePath);
            return File(mas, fileData.fileType, fileData.fileName);
        }

    }
}