using System;

namespace Task3.API.DtoRes
{
    public class MaterialResultDto
    {
        public Guid Id { get; set; }
        public string MaterialName { get; set; }
        public int MatCategoryId { set; get; }
    }
}