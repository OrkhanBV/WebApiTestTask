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

        Material SelectMaterialsById(int mId)
        {
            var mat = appDBContent.Materials.Where(m => m.id == mId).FirstOrDefault();
            return mat;
        }
            
        
        [HttpGet("CategoryType")] //По рекомендации Димы!! Заработало спасибо!
        public Material ChangeCategoryType(int mId)
        {
            var mat = SelectMaterialsById(mId);
            mat.category_type = "Преза"; ///Заменяем на Enum и в БД тоже,
            appDBContent.SaveChanges();
            return mat;
        }
    }
}