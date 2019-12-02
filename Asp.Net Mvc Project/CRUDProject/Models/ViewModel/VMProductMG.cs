using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDProject.Models.ViewModel
{
    public class VMProductMG
    {
        public long ID { get; set; }
        public long ProductID { get; set; }
        public string ImgPath { get; set; }
        public string ThumbnailPath { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<bool> IsDefaultImg { get; set; }
    }
}