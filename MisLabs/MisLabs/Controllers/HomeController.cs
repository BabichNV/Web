using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using JsonRepository;
using MisLabs.Models;

namespace MisLabs.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            JsonRepository.JsonMethods rep = new JsonMethods();
            return View(rep.GetAccounts());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Account account)
        {
            JsonRepository.JsonMethods rep = new JsonMethods();
            rep.AddAccount(account);
            return RedirectToAction("Index");
        }

        public ActionResult Update(string id)
        {
            JsonRepository.JsonMethods rep = new JsonMethods();
            return View(rep.GetAccount(Guid.Parse(id)));
        }

        [HttpPost]
        public ActionResult Update(Account account)
        {
            JsonRepository.JsonMethods rep = new JsonMethods();
            rep.UpdateAccount(account);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            JsonRepository.JsonMethods rep = new JsonMethods();
            rep.RemoveAccount(Guid.Parse(id));
            return RedirectToAction("Index");
        }

        public ActionResult ReportLastMonth()
        {
            JsonRepository.JsonMethods rep = new JsonMethods();
            var ldate = DateTime.Now.AddMonths(-1);
            return View(rep.GetAccountsBetweenDates(ldate, DateTime.Now));
        }

        public ActionResult Report(DateTime ldate, DateTime rdate)
        {
            JsonRepository.JsonMethods rep = new JsonMethods();
            ReportViewModel model = new ReportViewModel();
            model.Title = "c " + ldate.ToShortDateString() + " по " + rdate.ToShortDateString();
            model.Bills = rep.GetAccountsBetweenDates(ldate, rdate);
            return View(model);
        }
    }
}