using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Category
    {
        public Category()
        {
            this.Product = new HashSet<Product>();
        }

        public long ID { get; set; }
        public Nullable<long> ParentCatID { get; set; }
        public string Name { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}