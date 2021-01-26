using System;
using System.Collections.Generic;

public enum MatCategory
{
    Презентация,
    Приложение,
    Другое
}

namespace Task3.Core.Models
{
    public class Material
    {
        public Guid Id { set; get; }
        public string MaterialName { set; get; }
        public DateTime MaterialDate { set; get; }
        public int MatCategoryId { set; get; }
        public ICollection<MaterialVersion> MaterialVersions { get; set; }
        public Material()
        {
            MaterialVersions = new List<MaterialVersion>();
        }
    }
}