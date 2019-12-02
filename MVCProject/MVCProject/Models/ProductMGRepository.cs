using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class ProductMGRepository
    {
        public ProductMGRepository()
        {

        }
        AppDbContext dbContext = new AppDbContext();
        public ProductMG Add(ProductMG svm)
        {
            dbContext.ProductMG.Add(svm);
            dbContext.SaveChanges();
            return svm;
        }

        public ProductMG Get(long id)
        {
            return dbContext.ProductMG.Where(x => x.ProductID == id).FirstOrDefault();
        }

        public IEnumerable<ProductMG> GetAll()
        {
            return dbContext.ProductMG;
        }

        public bool Remove(long id)
        {
            var productImage = dbContext.ProductMG.Find(id);
            dbContext.ProductMG.Remove(productImage);
            dbContext.SaveChanges();
            return true;
        }

        public bool Remove(ProductMG svm)
        {
            throw new NotImplementedException();
        }

        public ProductMG Update(ProductMG svm)
        {
            var status = dbContext.ProductMG.Attach(svm);
            dbContext.Entry(svm).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
            return svm;
        }
    }
}