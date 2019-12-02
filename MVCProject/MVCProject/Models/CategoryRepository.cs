using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class CategoryRepository
    {
        AppDbContext dbContext = new AppDbContext();
        public CategoryRepository()
        {

        }
        public Category Add(Category svm)
        {

            dbContext.Category.Add(svm);
            dbContext.SaveChanges();
            return svm;
        }

        public Category Get(long id)
        {
            Category category = dbContext.Category.Find(id);
            return category;
        }

        public IEnumerable<Category> GetAll()
        {
            return dbContext.Category.ToList();
        }

        public bool Remove(long id)
        {
            Category category = dbContext.Category.Find(id);
            dbContext.Category.Remove(category);
            dbContext.SaveChanges();
            return true;
        }

        public bool Remove(Category svm)
        {
            throw new NotImplementedException();
        }

        public Category Update(Category svm)
        {
            var Result = dbContext.Category.Attach(svm);
            dbContext.Entry(svm).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
            return svm;
        }
    }
}