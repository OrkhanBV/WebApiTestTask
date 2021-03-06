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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using WebApiOrkhan.Data.Models;

namespace WebApiOrkhan.Data.Models
{
    public class File
    { 
            //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { set; get; }
        public string file_name { set; get; }
        public long size { set; get; }
        public string path_of_file { set; get; }
        public DateTime file_date { set; get; }

        public Material material { set; get; }
    }
}