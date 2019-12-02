using CRUDProject.Models;
using CRUDProject.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDProject.Controllers
{
    public class HomeController : Controller
    {
        private MyEntityConnectionString db = new MyEntityConnectionString();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(VMRegistration vm)
        {
            if (ModelState.IsValid)
            {
                Registration reg = new Registration();
                reg.FirstName = vm.FirstName;
                reg.LastName = vm.LastName;
                reg.UserName = vm.UserName;
                reg.Password = vm.Password;
                reg.Email = vm.Email;
                reg.Phone = vm.Phone;
                reg.Role = vm.Role;
                db.Registration.Add(reg);
                db.SaveChanges();
                Session["UserID"] = vm.UserID;
                Session["UserName"] = vm.UserName;
                Session["Role"] = vm.Role;
                return RedirectToAction("Wellcome", "Home");
            }
            return View(vm);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(VMLogin VMLog)
        {
            if (ModelState.IsValid)
            {
                var Details = (from userList in db.Registration
                               where userList.UserName == VMLog.UserName && userList.Password == VMLog.Password
                               select new { userList.UserID, userList.UserName, userList.Role }).ToList();
                if (Details.FirstOrDefault() != null)
                {
                    Session["UserID"] = Details.FirstOrDefault().UserID;
                    Session["UserName"] = Details.FirstOrDefault().UserName;
                    Session["Role"] = Details.FirstOrDefault().Role;
                    return RedirectToAction("Wellcome", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Credential");
            }
            return View(VMLog);
        }
        public ActionResult Logout()
        {
            Session.Remove("UserID");
            Session.Remove("UserName");
            Session.Remove("Role");
            return RedirectToAction("Login");
        }
        public ActionResult Wellcome()
        {

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}