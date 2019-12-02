using CRUDProject.Models;
using CRUDProject.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDB.WebForm.PJ1.DAL.Repository
{
    public class ProductMGRepository : IRepository<VMProductMG>
    {
        MyEntityConnectionString db = new MyEntityConnectionString();
        public VMProductMG Add(VMProductMG svm)
        {
            ProductMG productMG = new ProductMG();
            productMG.ProductID = svm.ProductID;
            productMG.ImgPath = svm.ImgPath;
            productMG.ThumbnailPath = svm.ThumbnailPath;
            productMG.DisplayOrder = svm.DisplayOrder;
            productMG.IsDefaultImg = svm.IsDefaultImg;
            db.ProductMG.Add(productMG);
            db.SaveChanges();
            svm.ID = productMG.ID;
            return svm;
        }

        public VMProductMG GetImg(int id)
        {
            VMProductMG SingleProductMG = db.ProductMG.Select(s => new VMProductMG
            {
                ID = s.ID,
                ProductID = s.ProductID,
                ImgPath = s.ImgPath,
                ThumbnailPath = s.ThumbnailPath,
                DisplayOrder = s.DisplayOrder,
                IsDefaultImg = s.IsDefaultImg
            }).Where(s => s.ID == id).FirstOrDefault();
            return SingleProductMG;
        }

        public IEnumerable<VMProductMG> GetAll()
        {
            IEnumerable<VMProductMG> data = db.ProductMG.Select(s => new VMProductMG
            {
                ID = s.ID,
                ProductID = s.ProductID,
                ImgPath = s.ImgPath,
                ThumbnailPath = s.ThumbnailPath,
                DisplayOrder = s.DisplayOrder,
                IsDefaultImg = s.IsDefaultImg
            });
            return data;
        }

        public bool Remove(long id)
        {
            ProductMG p = db.ProductMG.Find(id);
            db.ProductMG.Remove(p);
            db.SaveChanges();
            return true;
        }

        public bool Remove(VMProductMG svm)
        {
            ProductMG p = db.ProductMG.Find(svm.ID);
            db.ProductMG.Remove(p);
            db.SaveChanges();
            return true;
        }

        public VMProductMG Update(VMProductMG svm)
        {
            ProductMG productMG = db.ProductMG.Find(svm.ID);
            productMG.ProductID = svm.ProductID;
            productMG.ImgPath = svm.ImgPath;
            productMG.ThumbnailPath = svm.ThumbnailPath;
            productMG.DisplayOrder = svm.DisplayOrder;
            productMG.IsDefaultImg = svm.IsDefaultImg;

            db.Entry(productMG).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return svm;
        }

        public VMProductMG Get(long? id)
        {
            VMProductMG SingleProductMG = db.ProductMG.Select(s => new VMProductMG
            {
                ID = s.ID,
                ProductID = s.ProductID,
                ImgPath = s.ImgPath,
                ThumbnailPath = s.ThumbnailPath,
                DisplayOrder = s.DisplayOrder,
                IsDefaultImg = s.IsDefaultImg
            }).Where(s => s.ProductID == id).FirstOrDefault();
            return SingleProductMG;
        }

        //public VMProductMG GetEditItem(long id)
        //{
        //    VMProductMG SingleProductMG = db.ProductMG.Select(s => new VMProductMG
        //    {
        //        ID = s.ID,
        //        ProductID = s.ProductID,
        //        ImgPath = s.ImgPath,
        //        ThumbnailPath = s.ThumbnailPath,
        //        DisplayOrder = s.DisplayOrder,
        //        IsDefaultImg = s.IsDefaultImg
        //    }).Where(s => s.ProductID == id).FirstOrDefault();
        //    return SingleProductMG;
        //}
    }
}