using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using WebApiOrkhan.Data;
using WebApiOrkhan.Data.Models;

namespace WebApiOrkhan.Controllers
{
    [ApiController]
    [Route("/MaterialCat/")]
    public class CategoryController : Controller
    {
        private readonly AppDBContent appDBContent;
        public CategoryController(AppDBContent appDbContent)
        {
            this.appDBContent = appDbContent;
        }
        
        //Вопрос какой запрос у меня здесь должен быть по хорошему Get или Put и так далее
        [HttpGet("CategoryType")] //По рекомендации Димы!! Заработало спасибо!
        public Material ChangeCategoryType(int mId) //Для теста сырая версия!!
        {
            var mat = appDBContent.Materials.Where(m => m.id == mId).FirstOrDefault();
            mat.category_type = "Преза"; ///Заменяем на Enum и в БД тоже,
            appDBContent.SaveChanges();
            return mat;
        }
    }
}