using System.Collections.Generic;
using System.Linq;
using BabaevTask5.Data;
using BabaevTask5.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabaevTask5.Controllers
{
    public class HelperClass
    {
        private readonly AppDbContent appDbContent;

        /*public HelperClass(AppDbContent appDbContent)
        {
            this.appDbContent = appDbContent;
        }*/

        public HelperClass(AppDbContent appDbContent)
        {
            this.appDbContent = appDbContent;
        }

        public IOrderedEnumerable<Material> FilterMaterialsByDate(AppDbContent appDbContent) => appDbContent.Materials.
            ToList().
            OrderByDescending(m => m.MaterialDate);

        
        public IEnumerable<Material> GetFilterByDate(AppDbContent appDbContent)
        {
            return FilterMaterialsByDate(appDbContent);
        }
    }

}