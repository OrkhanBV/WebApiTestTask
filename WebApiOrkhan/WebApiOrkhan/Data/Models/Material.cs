using System.Collections.Generic;
using WebApiOrkhan.Data.Models;
using System;

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


namespace WebApi5.Data.Models
{
    public class Material
    {
        public int id { set; get; }
        public string material_name { set; get; }
        public string catecory_type { set; get; }
        public DateTime material_data { set; get; }
        /*public List<File> files { set; get; }*/
        public virtual Category Category { set; get; } //Правильно я указываю связь и как заполнять
    }
}