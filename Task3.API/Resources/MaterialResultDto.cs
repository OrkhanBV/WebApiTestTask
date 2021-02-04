using System;

namespace Task3.API.Resources
{
    public class MaterialResultDto
    {
        public Guid Id { get; set; }
        public string MaterialName { get; set; }
        public int MatCategoryId { set; get; }
    }
}