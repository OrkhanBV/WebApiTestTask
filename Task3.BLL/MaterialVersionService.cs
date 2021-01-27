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
    public class MaterialVersionService : IMaterialServices
    {
        private readonly IUnitOfWork _unitOfWork;
        /*private IWebHostEnvironment _env;*/
        private IHostingEnvironment _env;
        private string _dir;

        public MaterialVersionService(IUnitOfWork unitOfWork, IHostingEnvironment env)
        {
            this._unitOfWork = unitOfWork;
            _env = env;
            _dir = _env.ContentRootPath + "/Task3.DAL/MaterialStorage";
        }

        async Task<IEnumerable<MaterialVersion>> FilterVersionsByDate(Guid mId)
        {
            return await _unitOfWork.MaterialVersions.GetFilterVersionsByDate(mId);//добавить фильтры в материалверсионс
        }

        async Task<IEnumerable<MaterialVersion>> FilterVersionBySize(Guid mId)
        {
            return await _unitOfWork.MaterialVersions.GetFilterVersionsBySize(mId);
        }

        IActionResult UploadNewMaterialVersion(UploadMaterialVersionDTO materialVersionform)
        {
            MaterialVersion f1 = new MaterialVersion
            {
                FileDate = DateTime.Now,
                FileName = materialVersionform.Name,
                PathOfFile = _dir,
                Size = materialVersionform.File.Length,
                //ВНИМАНИЕ НУЖНО РЕШИТЬ ВОПРОС 
                /*Material = _unitOfWork.Materials.Where(m => m.Id == materialVersionform.MaterialId).ToList()[0]*/ ////////
            };
            using (var fileStream = new FileStream(
                Path.Combine(_dir,
                    $"{materialVersionform.Name}{Path.GetExtension(materialVersionform.File.FileName)}"),
                FileMode.Create,
                FileAccess.Write))
            {
                materialVersionform.File.CopyTo(fileStream);
            }

            _unitOfWork.MaterialVersions.AddRangeAsync(new List<MaterialVersion> {f1});
            _unitOfWork.CommitAsync();//надо проверить что это сохраняет
            return OkResult; //Доделать
        }
        
        /*IActionResult DownloadMaterialVersion(DownloadMaterialVersionDTO materialId)
        {
            
        }*/
    }
}