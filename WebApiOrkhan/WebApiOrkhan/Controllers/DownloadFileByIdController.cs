using System;
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
    public class DownloadFileByIdController : Controller
    {
        /*Скачивание конкретного материала
         Я это реализовал через подачу конкретных Id*/
        
        private readonly AppDBContent appDBContent;
        private readonly IWebHostEnvironment _appEnvironment;
        public DownloadFileByIdController (AppDBContent appDbContent , IWebHostEnvironment appEnvironment)
        {
            this.appDBContent = appDbContent;
            _appEnvironment = appEnvironment;
        }
        
        public IEnumerable<File> GetListOfFiles(int materialId) => appDBContent.File.
            Where(c => c.material.id == materialId).ToList();

        public string NameFile(int materialId, int fileId)
        {
            if (appDBContent.Material.Count() >= materialId &&
                GetListOfFiles(materialId).Count() >= fileId &&
                materialId >= 0 && 
                fileId >= 0)
                return (GetListOfFiles(materialId).Select(f => f.file_name).ToList()[fileId]);
            else
            {
                //Нужно написать правильное исключение
                return ("Попытка скачать не существующий файл");
            }
        }

        [HttpGet]
        public PhysicalFileResult GetFile(int materialId, int fileId)
        {
            // Тип файла - content-type, например pdf можно указать универсальный формат
            string file_type = "application/pdf";
            // Имя файла - необязательно
            string file_name = NameFile(materialId, fileId);
            // Путь к файлу
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "AppStorage/" + file_name);
            return PhysicalFile(file_path, file_type, file_name);
            
            
            /*
             
             Альтернатива с ручной обработкой исключений
             
             try
            {
                // Тип файла - content-type
                string file_type = "application/pdf";
                // Имя файла - необязательно
                //string file_name = "file.jpeg";
                string file_name = NameFile(materialId, fileId);
                // Путь к файлу
                string file_path = Path.Combine(_appEnvironment.ContentRootPath, "AppStorage/" + (file_name));
                return PhysicalFile(file_path, file_type, file_name);
            }
            catch (Exception e)
            {
                прописать правильное исключение
                Console.WriteLine(e);
                throw;
            }*/
        }
    }
}