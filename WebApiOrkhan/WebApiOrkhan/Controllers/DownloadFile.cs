using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApiOrkhan.Data;

namespace WebApiOrkhan.Controllers
{
    [ApiController]
    [Route("/Material/id/lastFile/Download")]
    public class DownloadFile : Controller
    {
        private readonly AppDBContent appDBContent; 
        public DownloadFile(AppDBContent appDbContent)
        {
            this.appDBContent = appDbContent;
        }
        private readonly IWebHostEnvironment _appEnvironment;
        public DownloadFile(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }
        
        [HttpGet]
        public PhysicalFileResult GetFile()
        {
            // Тип файла - content-type
            string file_type = "application/pdf";
            // Имя файла - необязательно
            string file_name = "file.jpeg";
            // Путь к файлу
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "AppStorage/" + file_name);
            
            return PhysicalFile(file_path, file_type, file_name);
        }
    }
}