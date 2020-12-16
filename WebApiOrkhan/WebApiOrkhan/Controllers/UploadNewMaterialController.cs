using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApiOrkhan.Controllers.Models;
using WebApiOrkhan.Data;
using WebApiOrkhan.Data.Models;
using File = WebApiOrkhan.Data.Models.File;

/*
 
 Реализацию один ко многим разобрал на сайте,
    https://metanit.com/sharp/entityframework/3.7.php
    
*/

namespace WebApiOrkhan.Controllers
{
    /*[ApiController]
    [Route("/Material/Upload")]*/
    public class UploadMaterialController : Controller
    {
        //private IHostingEnvironment _env;
        private IWebHostEnvironment _env;
        private string _dir;
        private readonly AppDBContent appDBContent;
        
        
        public UploadMaterialController(IWebHostEnvironment env, AppDBContent appDbContent)
        {
            _env = env;
            _dir = _env.ContentRootPath  + "/AppStorage";
            this.appDBContent = appDbContent;
        }
        
        public IActionResult Indexupload() => View();
        
        /*[HttpPost]*/
        public IActionResult FileInModel(FormForMaterials FormForMaterials)
        {
            //заменить на кейсы 
            if(FormForMaterials.CategoryName == "Приложение" ||
               FormForMaterials.CategoryName == "Презентация" ||
               FormForMaterials.CategoryName == "Другое")
            {
                //Создаем материал и сохраняем изменения
                Material mt1;
                mt1 = new Material{material_date = DateTime.Now, 
                    material_name = FormForMaterials.Name, 
                    category_type = FormForMaterials.CategoryName};
                //вроде я должен был здесь добавить материал в БД, но когда
                //делал забыл, но всё равно работает, видимо из-за того что добавляю позже при создании файла
                appDBContent.SaveChanges();
                
                
                //Создаем файл и
                File file = new File
                {
                    file_date = DateTime.Now, 
                    material = mt1, 
                    file_name = FormForMaterials.Name, 
                    size = FormForMaterials.File.Length, 
                    path_of_file = _dir
                };
                appDBContent.Files.AddRange(new List<File>{file});
                appDBContent.SaveChanges();
            }
            else
            {
                return BadRequest("WRONG TYPE OF CATEGORY");
            }
            using (var fileStream = new FileStream(
                /*Path.Combine(_dir,
                    $"{FormForMaterials.Name}{FormForMaterials.File.FileName.Substring(FormForMaterials.File.FileName.LastIndexOf('.'))}"), //path.GetExtension() <-плохо работает с формами
                FileMode.Create, 
                FileAccess.Write))*/
                Path.Combine(_dir,
                    $"{FormForMaterials.Name}{Path.GetExtension(FormForMaterials.File.FileName)}"),
                FileMode.Create, 
                FileAccess.Write))
            {
                FormForMaterials.File.CopyTo(fileStream);
            }
            return RedirectToAction("Indexupload");
        }
    }
}

/*I'm surprised that a lot of you seem to want to save files on the server.
 Solution to keep everything in memory is as follows:


Пример пост запроса Вариант 1
[HttpPost("api/upload")]
public async Task<IHttpActionResult> Upload()
{
if (!Request.Content.IsMimeMultipartContent())
throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType); 

var provider = new MultipartMemoryStreamProvider();
await Request.Content.ReadAsMultipartAsync(provider);
    foreach (var file in provider.Contents)
{
    var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
    var buffer = await file.ReadAsByteArrayAsync();
    //Do whatever you want with filename and its binary data.
}

return Ok();
}*/

/*
 Вариант 2
 
 public HttpResponseMessage Post()
{
var httpRequest = HttpContext.Current.Request;
    if (httpRequest.Files.Count < 1)
{
    return Request.CreateResponse(HttpStatusCode.BadRequest);
}

foreach(string file in httpRequest.Files)
{
    var postedFile = httpRequest.Files[file];
    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
    postedFile.SaveAs(filePath);
    // NOTE: To store in memory use postedFile.InputStream
}

return Request.CreateResponse(HttpStatusCode.Created);
}*/

/*
 Вариант 3 с Формой
 
 [HttpPost("UploadFiles")]
public async Task<IActionResult> Post(List<IFormFile> files)
{
long size = files.Sum(f => f.Length);

// full path to file in temp location
var filePath = Path.GetTempFileName();

    foreach (var formFile in files)
{
    if (formFile.Length > 0)
    {
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
        }
    }
}

// process uploaded files
// Don't rely on or trust the FileName property without validation.

return Ok(new { count = files.Count, size, filePath});
}*/