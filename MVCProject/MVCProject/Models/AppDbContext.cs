using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext() : base("AppDbContext")
        {

        }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductMG> ProductMG { get; set; }
    }
}