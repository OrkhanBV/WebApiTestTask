
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace BabaevTask5.Data.Models
{
    public class MaterialVersion
    { 
        public Guid Id { set; get; }
        public string FileName { set; get; }
        public long Size { set; get; }
        public string PathOfFile { set; get; }
        public DateTime FileDate { set; get; }
        public Material Material { set; get; }
    }
}