using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<Material>>> Get()
        {
            var materials = await _materialService.GetFilterMaterialsByDate();
            var materialResources = _mapper.Map<IEnumerable<Material>, IEnumerable<
            return Ok(materials);
        }
        
    }
}