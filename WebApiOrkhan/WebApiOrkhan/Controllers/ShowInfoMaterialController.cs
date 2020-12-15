using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApiOrkhan.Data;
using WebApiOrkhan.Data.Models;

/*Необходимо дорелиозовать обработку ошибок, протестировать и т.д.
 Реализовать отдельный класс со всеми Linq фильтрами чтобы решить проблему повтора кода*/

namespace WebApiOrkhan.Controllers
{
    [ApiController]
    [Route("/Material/Info")]
    public class ShowInfoMaterialController : ControllerBase
    {
        private readonly AppDBContent appDBContent;

        public ShowInfoMaterialController(AppDBContent appDbContent)
        {
            this.appDBContent = appDbContent;
        }

        /*public int ShowCountFiles(int materialId) => appDBContent.File.
            Where(m => m.material.id == materialId).
            Count();
        
        public IEnumerable<File> GetLastVersionFiles(int materialId) =>
            appDBContent.File.
                Where(m => m.material.id == materialId).
                ToList().
                OrderByDescending(m => m.file_data);
        
        [HttpGet]
        public string GetInfo(int materialId)
        {
            int count = ShowCountFiles(materialId);
            string lastUpdate = GetLastVersionFiles(materialId).Select(f => f.file_data).ToList()[0].ToString();
            return ($"Count ov versions of material = {count} \n" +
                    $"Last update = {lastUpdate}");
        }*/
    }
}