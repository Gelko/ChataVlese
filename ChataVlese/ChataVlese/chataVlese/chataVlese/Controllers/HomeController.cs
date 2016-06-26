using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using chataVlese.Models;

namespace chataVlese.Controllers
{
    public class HomeController : Controller
    {
        private chataVleseContext db = new chataVleseContext();
        public ActionResult Index()
        {
            return View(db.Feedbacks.ToList());
        }

        public ActionResult Info()
        {
            ViewBag.Message = "Info";

            return View();
        }

        public ActionResult Test()
        {
            ViewBag.Message = "Test";

            return View();
        }

        public ActionResult Gallery()
        {
            ViewBag.Message = "Gallery";

            return View();
        }

        public ActionResult Activities()
        {
            ViewBag.Message = "Activities";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }

        public ActionResult Pricelist()
        {
            ViewBag.Message = "Pricelist";

            return View();
        }

        public ActionResult Reservation()
        {
            ViewBag.Message = "Reservation";

            return View();
        }

        public ActionResult Feedback()
        {
            ViewBag.Message = "Feedback";

            return View();
        }

        public ActionResult CreateFeedback()
        {
            return View("/Feedback/Index");
        }

    }
}
