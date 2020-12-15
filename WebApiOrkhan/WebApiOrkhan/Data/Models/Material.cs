using System.Collections.Generic;
using WebApiOrkhan.Data.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

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


namespace WebApiOrkhan.Data.Models
{
    public class Material
    {         
        //ДАННОЕ РЕШЕНИЕ ПОЗВОЛИТ МНЕ РЕАЛИЗОВАТЬ АВТОИНКРИМЕНТИРОВАНИЕ И РЕШИТЬ ПРОБЛЕМУ С duplicate key value violates unique constraint
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { set; get; }
        public string material_name { set; get; }
        public DateTime material_date { set; get; }
        public string category_type { get; set; }
        public ICollection<File> Files { get; set; }
        public Material()
        {
            Files = new List<File>();
        }
        
    }
}