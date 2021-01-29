using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task3.API.Resources;
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

        /*[HttpPost("")]
        public async Task<ActionResult<UploadMaterialDTO>> UploadedMaterial([FromBody] UploadMaterialDTO uploadMaterialForm)
        {
            var materialToCreate = _mapper.Map<UploadMaterialDTO, Material>(uploadMaterialForm);
            var material = _materialService.UploadNewMaterial(uploadMaterialForm);
            var materialResources = _mapper.Map<Material, UploadMaterialDTO>(await material);
            return Ok(materialResources);
            /*return BadRequest();#1#
        }*/
        [HttpPost("")]
        public async Task<ActionResult> UploadedMaterial(/*IFormFile file,*/ [FromForm] UploadMaterialDTO uploadMaterialForm)
        {
            /*var materialToCreate = _mapper.Map<UploadMaterialDTO, Material>(uploadMaterialForm);*/
            var material = await _materialService.UploadNewMaterial(/*file,*/uploadMaterialForm);
            /*var materialResources = _mapper.Map<Material, UploadMaterialDTO>(await material);*/
            return Ok(material);
            /*return BadRequest();*/
        }
        
    }
}