using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiOrkhan.Data;
using File = WebApiOrkhan.Data.Models.File;

/*Скачивание отдельной версии материала.*/

namespace WebApiOrkhan.Controllers
{
    [ApiController]
    [Route("/Material/File/Download")]
    public class DownloadFileById : Controller
    {
        private readonly AppDBContent appDBContent;
        private readonly IWebHostEnvironment _appEnvironment;
        public DownloadFileById (AppDBContent appDbContent, IWebHostEnvironment appEnvironment)
        {
            this.appDBContent = appDbContent;
            _appEnvironment = appEnvironment;
        }
        /*private readonly IWebHostEnvironment _appEnvironment;
        public DownloadFileById (IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }*/
        
        public IEnumerable<File> GetListOfFiles(int materialId) => appDBContent.File.Where(c => c.material.id == materialId).ToList();

        public string NameFile(int materialId, int fileId)
        {
            return (GetListOfFiles(materialId).Select(f => f.file_name).ToList()[fileId]);
        }

        [HttpGet]
        public PhysicalFileResult GetFile(int materialId, int fileId)
        {
            // Тип файла - content-type
            string file_type = "application/pdf";
            // Имя файла - необязательно
            //string file_name = "file.jpeg";
            string file_name = NameFile(materialId, fileId);
            // Путь к файлу
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "AppStorage/" + (file_name + ".txt"));
            
            //string file_name = appDBContent.File.Where(c => c.material.id == materialId).Where(c => c.file_name );
            
            return PhysicalFile(file_path, file_type, file_name);
        }
    }
}