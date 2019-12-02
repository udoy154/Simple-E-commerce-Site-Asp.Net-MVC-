using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Product
    {
        public Product()
        {
            this.ProImage = new HashSet<ProductMG>();
        }

        public long ID { get; set; }
        public long CategoryID { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public double StockQuantity { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsActive { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ProductMG> ProImage { get; set; }
    }
}