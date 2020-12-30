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
        
        /*private readonly IVersion _iVersion;*/



        public VersionRepository(IWebHostEnvironment env, AppDbContent appDbContent/*, IVersion iVersion*/)
        {
            _env = env;
            _dir = _env.ContentRootPath  + "/MaterialStorage";
            /*_iVersion = iVersion;*/
            this.appDbContent = appDbContent;
        }

        public Guid UploadNewVersionOfMaterial(FormForVersion formForVersion)
        {
            try
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
                        $"{formForVersion.Name}{Path.GetExtension(formForVersion.File.FileName)}"),
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
            catch
            {
                Console.WriteLine("We have a problem");
                return Guid.Empty;
            }
            /*Data.Models.MaterialVersion f1 = new MaterialVersion
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
            return GetVersionId();*/
        }

        /*public FileModel GetFileParametrsForDownload(Guid vId)
        {
            throw new NotImplementedException();
        }*/

        public FileModel GetFileParametrsForDownload(/*Guid mId, */Guid vId)
        {
            try
            {
                MaterialVersion GetOfMaterialVersion(Guid vId) =>
                    appDbContent.MaterialVersions.Where(m => m.Id == vId).SingleOrDefault();
                 FileModel fileModel = new FileModel();
                 
                 fileModel.fileType = "application/png";/* + $"{Path.GetExtension(GetOfMaterialVersions(mId, vId).FileName)}"*/
                 fileModel.fileName = GetOfMaterialVersion(vId).FileName;
                 fileModel.filePath = Path.Combine(_env.ContentRootPath, "MaterialStorage/" + fileModel.fileName + ".png");/*Path.GetExtension(GetOfMaterialVersion(vId).FileName)*/
                 return(fileModel);
            }
            catch 
            {
                Console.WriteLine("Error");
                throw;
            }
        }

    }

}