using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BabaevTask5.Data.Interfaces;
using BabaevTask5.Data.Models;
using System.Linq;
using BabaevTask5.Controllers.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BabaevTask5.Data.Repository
{
    public class MaterialRepository : IMaterialHandler
    {
        private AppDbContent appDbContent;
        private IWebHostEnvironment _env;
        private string _dir;

        public MaterialRepository(AppDbContent appDbContent, IWebHostEnvironment env)
        {
            this.appDbContent = appDbContent;
            _env = env;
            _dir = env.ContentRootPath + "/MaterialStorage";
        }

        public IOrderedEnumerable<Material> FilterMaterialsByDate
        {
            get => appDbContent.Materials.ToList().OrderByDescending(m => m.MaterialDate);
            set => throw new NotImplementedException();
        }

        /*IOrderedEnumerable<Material> IMaterialHandler.FilterMaterialsByDate
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }*/

        /*public IOrderedEnumerable<Material> FilterMaterialsBySize
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public IOrderedEnumerable<Material> FilterMaterialByType
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public IEnumerable<Material> FilterOnlyFirstVersionMaterial
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }*/
    }
}