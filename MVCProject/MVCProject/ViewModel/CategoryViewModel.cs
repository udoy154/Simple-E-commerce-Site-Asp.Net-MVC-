using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.ViewModel
{
    public class CategoryViewModel
    {
        public long ID { get; set; }
        public long? ParentCatID { get; set; }
        public string Name { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}