using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class ProductMGController : Controller
    {
        ProductMGRepository productMGRepository = new ProductMGRepository();
        public ActionResult Index()
        {
            IEnumerable<ProductMG> model = productMGRepository.GetAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var productMG = productMGRepository.Get(id);
            return View(productMG);
        }

        [HttpPost]
        public ActionResult Edit(ProductMG model)
        {
            if (ModelState.IsValid)
            {
                var productImage = productMGRepository.Get(model.ProductID);
                if (productImage != null)
                {
                    productImage.ImgPath = model.ImgPath;
                    productImage.IsDefaultImg = model.IsDefaultImg;
                    productImage.ThumbnailPath = model.ThumbnailPath;
                    productImage.DisplayOrder = model.DisplayOrder;
                }
                productMGRepository.Update(productImage);
                return RedirectToAction("index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            productMGRepository.Remove(id);
            return RedirectToAction("index");
        }
    }
}