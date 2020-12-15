using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApiOrkhan.Data;
using File = WebApiOrkhan.Data.Models.File;

/*    Скачивание отдельной версии материала.
    как улучшить ?
    - разобраться с форматами файлов, хочется чтобы любой считывался
    - ("AppStorage/" + file_name + ".pdf") это не должно так выглядеть
    - создать класс DBHandler  в котором будут реализованы все необходимые методы
    чтобы не прописывать их реализацию непосредственно в контроллерах
 */

/*
    Скачивание актуальной версии материала
    Я это понял как актульный файл в конкретном материале 
    кототорый мы определяем через materialId
*/

namespace WebApiOrkhan.Controllers
{
    [ApiController]
    [Route("/Material/File/DownloadActual/")]
    public class DownloadActualFileController: Controller
    {
        private readonly AppDBContent appDBContent;
        private readonly IWebHostEnvironment _appEnvironment;
        public DownloadActualFileController (AppDBContent appDbContent, IWebHostEnvironment appEnvironment)
        {
            this.appDBContent = appDbContent;
            _appEnvironment = appEnvironment;
        }

        public IEnumerable<File> GetMaterialByIdSortFiles(int mId) =>
            appDBContent.Files.Where(c => c.material.id == mId).
                ToList().
                OrderByDescending(m => m.file_date);

        public string NameOfActualFile(int mId)
        {
            return (GetMaterialByIdSortFiles(mId).Select(c => c.file_name).ToList()[0]);
        }
        
        [HttpGet]
        public PhysicalFileResult GetFile(int mId, int fileId)
        {
            string file_type = "application/pdf";
            string file_name = NameOfActualFile(mId);
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "AppStorage/" + file_name + ".pdf");
            return PhysicalFile(file_path, file_type, file_name);
        }
    }
}