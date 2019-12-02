using MVCProject.Models;
using MVCProject.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        ProductMGRepository productMGRepository = new ProductMGRepository();
        AppDbContext dbContext = new AppDbContext();
        public ActionResult Index()
        {
            var model = productRepository.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()

        {
            var data = dbContext.Category.ToList();
            ViewBag.Data = new SelectList(data, "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel model, int? IsDefaultCheckBox)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product product = new Product
                    {
                        Name = model.Name,
                        Brand = model.Brand,
                        CategoryID = model.CategoryID,
                        Details = model.Details,
                        IsActive = model.IsActive,
                        IsFavorite = model.IsFavorite,
                        Price = model.Price,
                        StockQuantity = model.StockQuantity
                    };
                    productRepository.Add(product);
                    string uniqueFileName = null;
                    int i = 1;
                    if (model.ImagePath != null && model.ImagePath.Count > 0)
                    {
                        foreach (var photo in model.ImagePath)
                        {
                            uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                            string uploaddFile = Path.Combine(Server.MapPath("~/ProductPhoto"), uniqueFileName);
                            photo.SaveAs(uploaddFile);
                            ProductMG productMG = new ProductMG
                            {
                                ImgPath = uniqueFileName,
                                DisplayOrder = i,
                                ProductID = product.ID,
                                ThumbnailPath = ""
                            };
                            if (i == IsDefaultCheckBox)
                            {
                                productMG.IsDefaultImg = true;
                            }
                            productMGRepository.Add(productMG);
                            i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var data = dbContext.Category.ToList();
            ViewBag.Data = new SelectList(data, "ID", "Name");
            var product = productRepository.Get(id);
            var productMG = dbContext.ProductMG.Where(x => x.ProductID == id).ToList();
            List<string> path = new List<string>();
            if (product != null)
            {
                ProductEditViewModel productEditViewModel = new ProductEditViewModel
                {
                    ID = product.ID,
                    Brand = product.Brand,
                    CategoryID = product.CategoryID,
                    Details = product.Details,
                    IsActive = product.IsActive,
                    IsFavorite = product.IsFavorite,
                    Price = product.Price,
                    Name = product.Name,
                    StockQuantity = product.StockQuantity,
                    //StImagePath = productMG.ImgPath,
                    //ExistenceImagePath = productMG.ImgPath,
                };
                if (productMG != null)
                {
                    foreach (var images in productMG)
                    {
                        path.Add(images.ImgPath);
                    }
                    productEditViewModel.StImagePath = path;
                }
                return View(productEditViewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = productRepository.Get(model.ID);
                var productMG = productMGRepository.Get(model.ID);
                if (product != null)
                {
                    product.Name = model.Name;
                    product.Brand = model.Brand;
                    product.CategoryID = model.CategoryID;
                    product.Details = model.Details;
                    product.IsActive = model.IsActive;
                    product.IsFavorite = model.IsFavorite;
                    product.Price = model.Price;
                    product.StockQuantity = model.StockQuantity;

                    if (productMG != null)
                    {
                        string uniqueFileName = null;
                        int i = 1;
                        if (model.ImagePath[0] != null && model.ImagePath.Count > 0)
                        {
                            if (model.ExistenceImagePath != null)
                            {
                                string photoPath = Path.Combine(Server.MapPath("~/ProductPhoto"), model.ExistenceImagePath);
                                System.IO.File.Delete(photoPath);
                            }
                            foreach (var photo in model.ImagePath)
                            {
                                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                                string uploaddFile = Path.Combine(Server.MapPath("~/ProductPhoto"), uniqueFileName);
                                photo.SaveAs(uploaddFile);
                                productMG.ImgPath = uniqueFileName;
                                productMGRepository.Update(productMG);
                                i++;
                            }
                        }
                    }
                    productRepository.Update(product);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            productRepository.Remove(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            var product = productRepository.Get(id);
            var productMG = dbContext.ProductMG.Where(x => x.ProductID == id).ToList();
            List<string> path = new List<string>();
            if (product != null)
            {
                ProductEditViewModel productEditViewModel = new ProductEditViewModel
                {
                    ID = product.ID,
                    Brand = product.Brand,
                    CategoryID = product.CategoryID,
                    Details = product.Details,
                    IsActive = product.IsActive,
                    IsFavorite = product.IsFavorite,
                    Price = product.Price,
                    Name = product.Name,
                    StockQuantity = product.StockQuantity,
                    //StImagePath = productMG.ImgPath,
                    //ExistenceImagePath = productMG.ImgPath,
                };
                if (productMG != null)
                {
                    foreach (var images in productMG)
                    {
                        path.Add(images.ImgPath);
                    }
                    productEditViewModel.StImagePath = path;
                }
                return View(productEditViewModel);
            }
            return RedirectToAction("Index");
        }
    }
}