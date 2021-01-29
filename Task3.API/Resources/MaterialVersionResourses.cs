using System;

namespace Task3.API.Resources
{
    public class MaterialVersionResources
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MaterialResources Material { get; set; }
        
    }
}