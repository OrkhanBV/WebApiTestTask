using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BabaevTask5.Controllers.Models;
using BabaevTask5.Data.Interfaces;
using BabaevTask5.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BabaevTask5.Data.Repository
{
    public class VersionRepository : IVersion
    {
        private IWebHostEnvironment _env;
        private string _dir;
        private readonly AppDbContent appDbContent;
        


        public VersionRepository(IWebHostEnvironment env, AppDbContent appDbContent)
        {
            _env = env;
            _dir = _env.ContentRootPath  + "/MaterialStorage";
            this.appDbContent = appDbContent;
        }
        
        public string GetInfoAboutMaterialVersion
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public IOrderedEnumerable<MaterialVersion> FilterVersionsByDate
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public IOrderedEnumerable<MaterialVersion> FilterVersionsBySize
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public IOrderedEnumerable<MaterialVersion> FilterVersionsByType
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public Guid UploadNewVersionOfMaterial(FormForVersion formForVersion)
        {
            
            Data.Models.MaterialVersion f1 = new MaterialVersion
            {
                FileDate = DateTime.Now,
                FileName = formForVersion.Name,
                PathOfFile = _dir,
                Size = formForVersion.File.Length,
                Material = appDbContent.Materials.Where(m => m.Id == formForVersion.MaterialId).ToList()[0]////////
            };
            using (var fileStream = new FileStream(
                Path.Combine(_dir,
                    $"{formForVersion.Name}_version{Path.GetExtension(formForVersion.File.FileName)}"),
                FileMode.Create, 
                FileAccess.Write))
            {
                formForVersion.File.CopyTo(fileStream);
            }
            appDbContent.MaterialVersions.AddRange(new List<MaterialVersion>{f1});
            appDbContent.SaveChanges();

            Guid GetVersionId() => appDbContent.MaterialVersions
                .Where(v => v == f1).SingleOrDefault().Id;
            return GetVersionId();
        }

        public PhysicalFileResult DownloadFirstVersionByMaterialId
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public PhysicalFileResult DownloadLastVersionByMaterialId
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public PhysicalFileResult DownloadConcreteVersionByMaterialIdByVersionId
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /*public IActionResult UploadVersion() => View();*/
        }
        

        /*public PhysicalFileResult DownloadFirstVersionByMaterialId
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public PhysicalFileResult DownloadLastVersionByMaterialId
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public PhysicalFileResult DownloadConcreteVersionByMaterialIdByVersionId
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }*/
    
}