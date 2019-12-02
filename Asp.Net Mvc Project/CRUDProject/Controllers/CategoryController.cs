using CRUDProject.Models;
using CRUDProject.Models.ViewModel;
using IDB.WebForm.PJ1.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CRUDProject.Controllers
{
    public class CategoryController : Controller
    {
        CatRepository catDB = new CatRepository();
        athurize Athurize = new athurize();
        // GET: Category
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
            var model = catDB.GetAll().ToList();
            return View(model);
        }
        public ActionResult Details(long? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VMCategoryInfo model = catDB.Get(ID);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMCategoryInfo category)
        {
            if (ModelState.IsValid)
            {
                catDB.Add(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public ActionResult Edit(long? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VMCategoryInfo model = catDB.Get(ID);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VMCategoryInfo category)
        {
            if (ModelState.IsValid)
            {
                catDB.Update(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public ActionResult Delete(long? ID)
        {

            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VMCategoryInfo model = catDB.Get(ID);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int ID)
        {
          
            catDB.Remove(ID);
            return RedirectToAction("Index");
        }
    }
}