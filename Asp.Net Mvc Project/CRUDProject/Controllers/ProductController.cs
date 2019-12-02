using CRUDProject.Models;
using CRUDProject.Models.ViewModel;
using IDB.WebForm.PJ1.DAL;
using IDB.WebForm.PJ1.DAL.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CRUDProject.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository db = new ProductRepository();
        ProductMGRepository imgdb = new ProductMGRepository();
        CatRepository catdb = new CatRepository();
        athurize Athurize = new athurize();
        // GET: Product
        public ActionResult Index()
        {
            if (Athurize.CheckNotLogin())
            {
                return RedirectToAction("Registration", "Home");
            }
            if (Athurize.CheckUser())
            {
                return RedirectToAction("Index", "Home");
            }

            var model = db.GetAll().ToList();
            var category = catdb.GetAll().ToList();
            ViewBag.category = category;
            var img = imgdb.GetAll().ToList();
            ViewBag.Img = img;
            return View(model);
        }

        public ActionResult Detail(long? ID)
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(catdb.GetAll().ToList(), "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMProductInfo product, HttpPostedFileBase Image, HttpPostedFileBase Thumbnail)
        {
            var path1 = "~/Content/Images/ProductsImg";
            var path2 = "~/Content/Images/ProductsThum";
            var InvalideFileFormate = "";
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] { ".jpg", ".png", ".jpg", "jpeg" };
                if (Image != null)
                {
                    var ext1 = Path.GetExtension(Image.FileName).ToLower();
                    if (allowedExtensions.Contains(ext1))
                    {
                        var fileName1 = Path.GetFileName(Image.FileName);
                        path1 = Path.Combine(Server.MapPath(path1), fileName1);

                        Image.SaveAs(path1);
                    }
                    else
                    {
                        InvalideFileFormate = "Please choose only Image file";
                        ViewBag.Error = InvalideFileFormate;
                        ViewBag.CategoryID = new SelectList(catdb.GetAll().ToList(), "ID", "Name", product.CategoryID);
                        return View(product);
                    }
                }

                if (Thumbnail != null)
                {

                    var ext2 = Path.GetExtension(Thumbnail.FileName).ToLower();
                    if (allowedExtensions.Contains(ext2))
                    {
                        var fileName2 = Path.GetFileName(Thumbnail.FileName);
                        path2 = Path.Combine(Server.MapPath(path2), fileName2);

                        Thumbnail.SaveAs(path2);
                    }
                    else
                    {
                        InvalideFileFormate = "Please choose only  Image file";
                        ViewBag.Error = InvalideFileFormate;
                        ViewBag.CategoryID = new SelectList(catdb.GetAll().ToList(), "ID", "Name", product.CategoryID);
                        return View(product);
                    }
                }

                var newProduct = db.Add(product);
                VMProductMG img = new VMProductMG();
                img.ProductID = newProduct.ID;
                img.ImgPath = path1;
                img.ThumbnailPath = path2;
                imgdb.Add(img);
                

                return RedirectToAction("Index");

            }
           
            ViewBag.CategoryID = new SelectList(catdb.GetAll().ToList(), "ID", "Name", product.CategoryID);
            return View(product); 
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VMProductInfo product = db.Get(id);
            var category = catdb.Get(product.CategoryID);
            ViewBag.CategoryName = category.Name;

            var Img = imgdb.Get(id);
            ViewBag.Img = Path.GetFileName(Img.ImgPath);
            ViewBag.Thum = Path.GetFileName(Img.ThumbnailPath);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VMProductInfo product = db.Get(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(catdb.GetAll().ToList(), "ID", "Name", product.CategoryID);

            var Img = imgdb.Get(id);

            ViewBag.Thum = Path.GetFileName(Img.ThumbnailPath);
            ViewBag.Img = Path.GetFileName(Img.ImgPath);


            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VMProductInfo product, HttpPostedFileBase Image, HttpPostedFileBase Thumbnail)
        {
            var img = imgdb.Get(product.ID);
            var path1 = img.ImgPath;
            var path2 = img.ThumbnailPath;
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] { ".jpg", ".png", ".jpg", "jpeg" };                
                if (Image != null)
                {
                    var ext1 = Path.GetExtension(Image.FileName).ToLower();
                    if (allowedExtensions.Contains(ext1))
                    {
                        var fileName1 = Path.GetFileName(Image.FileName);
                        path1 = Path.Combine(Server.MapPath("~/Content/Images/ProductsImg"), fileName1);

                        if (System.IO.File.Exists(img.ImgPath))
                        {
                            System.IO.File.Delete(img.ImgPath);
                        }

                        Image.SaveAs(path1);
                    }
                }
                else
                {
                    ViewBag.Error = "Please choose only Image file";
                }
                if (Thumbnail != null)
                {
                   
                    var ext2 = Path.GetExtension(Thumbnail.FileName).ToLower();
                    if (allowedExtensions.Contains(ext2))
                    {
                        var fileName2 = Path.GetFileName(Thumbnail.FileName);
                        path2 = Path.Combine(Server.MapPath("~/Content/Images/ProductsThum"), fileName2);


                        if (System.IO.File.Exists(img.ThumbnailPath))
                        {
                            System.IO.File.Delete(img.ThumbnailPath);
                        }
                        Image.SaveAs(path2);
                    }
                }
                else
                {
                    ViewBag.Error = "Please choose only Image file";
                }

                img.ImgPath = path1;
                img.ThumbnailPath = path2;
                imgdb.Update(img);
                db.Update(product);

                return RedirectToAction("Index");

            }
       
            ViewBag.Thum = Path.GetFileName(img.ThumbnailPath);
            ViewBag.Img = Path.GetFileName(img.ImgPath);
            ViewBag.CategoryID = new SelectList(catdb.GetAll().ToList(), "ID", "Name", product.CategoryID);
            return View(product);
        }
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VMProductInfo product = db.Get(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(catdb.GetAll().ToList(), "ID", "Name", product.CategoryID);

            var Img = imgdb.Get(id);

            ViewBag.Thum = Path.GetFileName(Img.ThumbnailPath);
            ViewBag.Img = Path.GetFileName(Img.ImgPath);


            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            
            var Image =  imgdb.Get(id);
            imgdb.Remove(Image);
            db.Remove(id);
            return RedirectToAction("Index");

        }
    }
}