using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi5.Data.Models;
using WebApiOrkhan.Data;
using WebApiOrkhan.Data.Models;

/*This controller allow us to filter materials by DATE*/

namespace WebApiOrkhan.Controllers
{
    [ApiController]
    [Route("/Material/FilterByDate")]
    public class FilterMaterialByDateController : ControllerBase
    {
        private readonly AppDBContent appDBContent;

        public FilterMaterialByDateController(AppDBContent appDbContent)
        {
            this.appDBContent = appDbContent;
        }

        /*public IOrderedEnumerable<Material> FilterMaterialsByDate() => appDBContent.Material.
            ToList().
            OrderByDescending(m => m.material_data);

        [HttpGet]
        public IEnumerable<Material> GetFilterByDate()
        {
            return FilterMaterialsByDate();
        }*/
    }
}
