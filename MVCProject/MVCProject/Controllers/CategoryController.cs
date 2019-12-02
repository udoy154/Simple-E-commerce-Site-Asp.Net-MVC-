using MVCProject.Models;
using MVCProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository categoryRepository = new CategoryRepository();
        public CategoryController()
        {

        }
        AppDbContext dbContext = new AppDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Category> model = categoryRepository.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category newCategory = new Category
                {
                    Name = model.Name,
                    ParentCatID = model.ParentCatID,
                    DisplayOrder = model.DisplayOrder,
                    IsActive = model.IsActive
                };
                categoryRepository.Add(newCategory);
                return RedirectToAction("Index", "Category");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category model = categoryRepository.Get(id);
            CategoryViewModel CategoryViewModel = new CategoryViewModel
            {
                ID = model.ID,
                DisplayOrder = model.DisplayOrder,
                IsActive = model.IsActive,
                Name = model.Name,
                ParentCatID = model.ParentCatID
            };
            return View(CategoryViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category exitsCategory = categoryRepository.Get(model.ID);
                if (exitsCategory != null)
                {
                    exitsCategory.Name = model.Name;
                    exitsCategory.ParentCatID = model.ParentCatID;
                    exitsCategory.IsActive = model.IsActive;
                    exitsCategory.DisplayOrder = model.DisplayOrder;

                    categoryRepository.Update(exitsCategory);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            var products = dbContext.Product.Where(x => x.CategoryID == id).ToList();
            Category category = categoryRepository.Get(id);
            CategoryDetailsViewModel model = new CategoryDetailsViewModel
            {
                ID = category.ID,
                DisplayOrder = category.DisplayOrder,
                Name = category.Name,
                IsActive = category.IsActive,
                ParentCatID = category.ParentCatID
            };
            if (products != null)
            {
                model.Products = products;
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            categoryRepository.Remove(id);
            return RedirectToAction("Index");
        }
    }
}