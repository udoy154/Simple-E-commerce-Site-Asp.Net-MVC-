using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class ProductMG
    {
        public long ID { get; set; }
        public long ProductID { get; set; }
        public string ImgPath { get; set; }
        public string ThumbnailPath { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDefaultImg { get; set; }

        public virtual Product Product { get; set; }
    }
}