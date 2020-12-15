using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiOrkhan.Data;
using WebApiOrkhan.Data.Models;
using File = WebApiOrkhan.Data.Models.File;

/*Скачивание отдельной версии материала.*/

namespace WebApiOrkhan.Controllers
{
    [ApiController]
    [Route("/Material/File/DownloadActual/")]
    public class DownloadActualFileController: Controller
    {
        /*Скачивание актуальной версии материала
         Я это понял как актульный файл в конкретном материале 
         котоорый мы определяем через materialId*/
        
        private readonly AppDBContent appDBContent;
        private readonly IWebHostEnvironment _appEnvironment;
        public DownloadActualFileController (AppDBContent appDbContent, IWebHostEnvironment appEnvironment)
        {
            this.appDBContent = appDbContent;
            _appEnvironment = appEnvironment;
        }

        public IEnumerable<File> GetMaterialById(int mId) =>
            appDBContent.Files.Where(c => c.material.id == mId).
                ToList().
                OrderByDescending(m => m.file_date);

        public string NameOfActualFile(int mId)
        {
            return (GetMaterialById(mId).Select(c => c.file_name).ToList()[0]);
        }
        /*public IEnumerable<File> GetLastVersionFiles(int materialId) =>
            appDBContent.Files.
                Where(m => m.material.id == materialId).
                ToList().
                OrderByDescending(m => m.file_date);
        
        public string NameOfActualFile(int materialId)
        {
            return (GetLastVersionFiles(materialId).Select(f => f.file_name).ToList()[0]);
        }*/

        [HttpGet]
        public PhysicalFileResult GetFile(int mId, int fileId)
        {
            // Тип файла - content-type, например pdf можно указать универсальный формат
            string file_type = "application/pdf";
            // Имя файла - необязательно
            string file_name = NameOfActualFile(mId);
            // Путь к файлу
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "AppStorage/" + file_name + ".pdf");
            return PhysicalFile(file_path, file_type, file_name);
        }
    }
}