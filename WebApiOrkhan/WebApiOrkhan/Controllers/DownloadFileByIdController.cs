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
         1)Можно реализовать по конкретному id file в таблице file
         2)Нужно создать поле в таблице файлы с порядковым номером версии конкретного материала
         --можно поробовать реализовать метод который позволит записывать номер версии при загрузке*/
        
        private readonly AppDBContent appDBContent;
        private readonly IWebHostEnvironment _appEnvironment;
        public DownloadFileByIdController (AppDBContent appDbContent , IWebHostEnvironment appEnvironment)
        {
            this.appDBContent = appDbContent;
            _appEnvironment = appEnvironment;
        }
        
        public IEnumerable<File> GetListOfFiles(int materialId) => appDBContent.Files.
            Where(c => c.material.id == materialId).ToList();

        public string NameFile(int materialId, int IndexInList)
        {
            if (appDBContent.Materials.Count() >= materialId ||
                GetListOfFiles(materialId).Count() >= IndexInList ||
                materialId >= 0 || 
                IndexInList >= 0)
                return (GetListOfFiles(materialId).Select(f => f.file_name).ToList()[IndexInList]);
            else
            {
                //Нужно написать правильное исключение
                return ("Попытка скачать не существующий файл");
            }
        }

        [HttpGet]
        public PhysicalFileResult GetFile(int materialId, int fileId)
        {
            string file_type = "application/pdf";
            string file_name = NameFile(materialId, fileId);
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "AppStorage/" + file_name + ".pdf");
            return PhysicalFile(file_path, file_type, file_name);
        }
    }
}