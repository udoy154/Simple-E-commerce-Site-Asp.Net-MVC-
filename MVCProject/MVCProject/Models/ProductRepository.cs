using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class ProductRepository : IRepository<Product>
    {
        public ProductRepository()
        {

        }
        AppDbContext dbContext = new AppDbContext();
        public Product Add(Product svm)
        {
            dbContext.Product.Add(svm);
            dbContext.SaveChanges();
            return svm;
        }

        public Product Get(long id)
        {
            Product product = dbContext.Product.Find(id);
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            return dbContext.Product.ToList();
        }

        public bool Remove(long id)
        {
            Product product = dbContext.Product.Find(id);
            dbContext.Product.Remove(product);
            dbContext.SaveChanges();
            return true;
        }

        public bool Remove(Product svm)
        {
            throw new NotImplementedException();
        }

        public Product Update(Product svm)
        {
            var result = dbContext.Product.Attach(svm);
            dbContext.Entry(svm).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
            return svm;
        }
    }
}