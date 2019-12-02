using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDProject.Models.ViewModel
{
    public class VMProductInfo
    {
        public long ID { get; set; }
        public long CategoryID { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> StockQuantity { get; set; }
        public Nullable<bool> IsFavorite { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}