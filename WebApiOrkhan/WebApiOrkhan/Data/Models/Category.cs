using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi5.Data.Models;
using WebApiOrkhan.Data.Models;

/***********************************************************************
************************************************************************
        Все сущности в приложении принято выделять в отдельные модели. 
В зависимости от поставленной задачи и сложности приложения можно 
выделить различное количество моделей.

        Модели представляют собой простые классы и располагаются в 
проекте в каталоге Models. Модели описывают логику данных.

        Модель необязательно состоит только из свойств, 
кроме того, она может иметь конструктор, какие-нибудь вспомогательные 
методы. Но главное не перегружать класс модели и помнить, что его 
предназначение - ОПИСЫВАТЬ ДАННЫЕ.

        Манипуляции с данными и бизнес-логика - это больше сфера КОНТРОЛЛЕРА.

        Данные моделей хранятся в базе данных. Чтобы взаимодействовать 
с базой данных, очень удобно пользоваться фреймворком Entity Framework. 

https://metanit.com/sharp/mvc/5.1.php
************************************************************************
************************************************************************/


/*namespace WebApiOrkhan.Data.Models
{
    public class Category
    {
        //[Column(TypeName="SERIAL")] //не сработало
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { set; get; } //у нас будет всего три категории поэтому можно по id определять
        public string category_name { set; get; }
        public ICollection<Material> Materials { get; set; }
        public Category()
        {
            Materials = new List<Material>();
        }
        //public List<Material> material { set; get; }
    }
}*/

/*namespace WebApi5.Data.Models
{
    public class Material
    {
        /*public int materialId { set; get; }
        public string materialName { set; get; }
        public string catecoryType { set; get; }
        //public virtual Category Category { set; get; }
        public List<File> File { set; get; }#1#
    }
}*/