using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketPurchasingSystem.Models;

namespace TicketPurchasingSystem.Controllers
{
    public class CreateTokenController : Controller
    {
        RailwayDBContext railwayDbContexts = new RailwayDBContext();
        CreateTokenModel createTokenModel = new CreateTokenModel();
        PersonsModel personsModel = new PersonsModel();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string PersonID)
        {
            int personID = 0;
            Int32.TryParse(PersonID, out personID);
            Session["ID"] = personID;
            if (personID != 0)
            {
                PersonsModel persons = personsModel.GetPersonalDetails(personID);
                if (persons == null)
                {
                    ModelState.AddModelError("PersonID", "We Did't Find Your ID Please Regiter First And Try Again");
                    return View("Index");

                }
                else
                {
                    return RedirectToAction("PersonsDetails", persons);
                }
            }
            else
            {
                ModelState.AddModelError("PersonID", "Please Enter a Valid ID");
                return View("Index");
            }
            
        }
        public ActionResult PersonsDetails()
        {
            return View("PersonsDetails");
        }
        [HttpPost]
        public ActionResult PersonsDetails(Person person)
        {
            Session["Persons"] = person;
            return RedirectToAction("GenerateToken");
        }
        public ActionResult GenerateToken()
        {
            ViewBag.JourneyStart = new SelectList(railwayDbContexts.City, "CityName", "CityName");
            ViewBag.JourneyEnd = new SelectList(railwayDbContexts.City, "CityName", "CityName");
            return View();
        }
        [HttpPost]
        public ActionResult GenerateToken(CreateTokenModel createTokenModel)
        {
            createTokenModel.personID = Convert.ToInt32(Session["ID"]);
            Session["CreateToken"] = createTokenModel;
            createTokenModel.insertData(createTokenModel);
            Journey journey = createTokenModel.GetDetails();
            UpdateTicketStatusTotalToPending(journey.CoatchType);
            return View("FullTicket", journey);
        }
        public void UpdateTicketStatusTotalToPending(string CoatchType)
        {
            switch (CoatchType)
            {
                case "AC":
                    Session["AvailableACTicket"] = Convert.ToInt32(Session["AvailableACTicket"]) - 1;
                    Session["NumberOfACPendingTicket"] = Convert.ToInt32(Session["NumberOfACPendingTicket"]) + 1;
                    break;
                case "S_CHAIR":
                    Session["AvailableS_ChairTicket"] = Convert.ToInt32(Session["AvailableS_ChairTicket"]) - 1;
                    Session["NumberOfS_ChairPendingTicket"] = Convert.ToInt32(Session["NumberOfS_ChairPendingTicket"]) + 1;
                    break;

            }
        }
    }
}