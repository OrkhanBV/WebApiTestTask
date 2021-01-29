using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Task3.Core;
using Task3.Core.DTO;
using Task3.Core.Models;
using Task3.Core.Services;

namespace Task3.BLL
{
    public class MaterialVersionService : IMaterialVersionServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _env;
        private string _dir;

        public MaterialVersionService(IUnitOfWork unitOfWork, IHostingEnvironment env)
        {
            this._unitOfWork = unitOfWork;
            _env = env;
            _dir = _env.ContentRootPath + "/Task3.DAL/MaterialStorage";
        }
        
        public async Task<IEnumerable<MaterialVersion>> FilterVersionsByDate(Guid mId)
        {
            return await _unitOfWork.MaterialVersions.GetFilterVersionsByDate(mId);
        }

        public async Task<IEnumerable<MaterialVersion>> FilterVersionsBySize(Guid mId)
        {
            return await _unitOfWork.MaterialVersions.GetFilterVersionsBySize(mId);
        }

        public async Task<MaterialVersion> UploadNewMaterialVersion(UploadMaterialVersionDTO materialVersionform)
        {
            MaterialVersion f1 = new MaterialVersion
            {
                FileDate = DateTime.Now,
                FileName = materialVersionform.Name,
                PathOfFile = _dir,
                Size = materialVersionform.File.Length,
                //ВНИМАНИЕ НУЖНО РЕАЛИЗОВАТЬ
                //Material = _unitOfWork.Materials.Where(m => m.Id == materialVersionform.MaterialId).ToList()[0] ////////
            };
            using (var fileStream = new FileStream(
                Path.Combine(_dir,
                    $"{materialVersionform.Name}{Path.GetExtension(materialVersionform.File.FileName)}"),
                    FileMode.Create,
                    FileAccess.Write))
            {
                materialVersionform.File.CopyTo(fileStream);
            }

            await _unitOfWork.MaterialVersions.AddRangeAsync(new List<MaterialVersion> {f1});
            await _unitOfWork.CommitAsync();
            return f1;
        }

        public Task<MaterialVersion> DownloadMaterialVersion(DownloadMaterialVersionDTO materialId)
        {
            throw new NotImplementedException();
        }
    }
}