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
            var materialResources = _mapper.
                Map<IEnumerable<Material>, IEnumerable<MaterialResources>>(materials);
            return Ok(materials);
        }
        
        /*[Route("GetMat")]
        [HttpPost]
        public async Task<ActionResult> GetMaterialNew([FromForm] UploadMaterialDTO uploadMaterialForm)
        {
            var validator = new SaveMaterialValidator();
            var validationResult = await validator.ValidateAsync(uploadMaterialForm);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var material = await _materialService.UploadNewMaterial(uploadMaterialForm);
            return Ok(material);
        }*/
        
        [HttpPost("")]
        public async Task<ActionResult> UploadedMaterial([FromForm] UploadMaterialDTO uploadMaterialForm)
        {
            var validator = new SaveMaterialValidator();
            var validationResult = await validator.ValidateAsync(uploadMaterialForm);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var material = await _materialService.UploadNewMaterial(uploadMaterialForm);
            return Ok(material);
        }

        [Route("GetMat")]
        [HttpPost]
        public async Task<ActionResult> DownloadMaterialqqqq(Guid mId)
        {
            var fileForD = await _materialService.GetDtoForDownloadMaterialAsync(mId);
            byte[] mas = System.IO.File.ReadAllBytes(fileForD.filePath);
            return File(mas, fileForD.fileType, fileForD.fileName); //PhysicalFile(fileForD.filePath, fileForD.fileType, fileForD.fileName);
        }

    }
}