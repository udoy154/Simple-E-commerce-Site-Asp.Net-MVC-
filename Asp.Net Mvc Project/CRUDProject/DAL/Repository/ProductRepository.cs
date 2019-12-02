
using CRUDProject.Models;
using CRUDProject.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDB.WebForm.PJ1.DAL
{
    public class ProductRepository : IRepository<VMProductInfo>
    {
        MyEntityConnectionString db = new MyEntityConnectionString();
        public VMProductInfo Add(VMProductInfo svm)
        {
            Product product = new Product();
            product.CategoryID = svm.CategoryID;
            product.Brand = svm.Brand;
            product.Name = svm.Name;
            product.Details = svm.Details;
            product.Price = svm.Price;
            product.StockQuantity = svm.StockQuantity;
            product.IsFavorite = svm.IsFavorite;
            product.IsActive = svm.IsActive;

            db.Product.Add(product);
            db.SaveChanges();
            svm.ID = product.ID;

            return svm;
        }

        public VMProductInfo Get(long? id)
        {
            VMProductInfo SingleProduct = db.Product.Select(s => new VMProductInfo
            {
                ID = s.ID,
                CategoryID = s.CategoryID,
                Brand = s.Brand,
                Name = s.Name,
                Details = s.Details,
                Price = s.Price,
                StockQuantity = s.StockQuantity,
                IsFavorite = s.IsFavorite,
                IsActive = s.IsActive
            }).Where(q => q.ID == id).FirstOrDefault();
            return SingleProduct;
        }

        public IEnumerable<VMProductInfo> GetAll()
        {
            IEnumerable<VMProductInfo> data = db.Product.Select(s => new VMProductInfo
            {
                ID = s.ID,
                CategoryID = s.CategoryID,
                Brand = s.Brand,
                Name = s.Name,
                Details = s.Details,
                Price = s.Price,
                StockQuantity = s.StockQuantity,
                IsFavorite = s.IsFavorite,
                IsActive = s.IsActive
            });
            return data;
        }

        public bool Remove(long id)
        {
            Product p = db.Product.Find(id);
            db.Product.Remove(p);
            db.SaveChanges();
            return true;
        }

        public bool Remove(VMProductInfo svm)
        {
            Product p = db.Product.Find(svm.ID);
            db.Product.Remove(p);
            db.SaveChanges();
            return true;
        }

        public VMProductInfo Update(VMProductInfo svm)
        {
            Product product = db.Product.Find(svm.ID);
            product.CategoryID = svm.CategoryID;
            product.Brand = svm.Brand;
            product.Name = svm.Name;
            product.Details = svm.Details;
            product.Price = svm.Price;
            product.StockQuantity = svm.StockQuantity;
            product.IsFavorite = svm.IsFavorite;
            product.IsActive = svm.IsActive;

            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return svm;
        }       
    }//c
}//ns