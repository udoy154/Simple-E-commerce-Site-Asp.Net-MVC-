using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProject.ViewModel
{
    public class ProductViewModel
    {
        AppDbContext dbContext = new AppDbContext();
        public long ID { get; set; }

        [Required]
        public long CategoryID { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double StockQuantity { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsActive { get; set; }
        public List<HttpPostedFileBase> ImagePath { get; set; }
        public bool IsDefaultImg { get; set; }
        [NotMapped]
        public List<Category> Category { get; set; }
    }
}