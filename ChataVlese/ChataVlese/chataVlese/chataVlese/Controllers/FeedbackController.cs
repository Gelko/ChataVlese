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
    public class FeedbackController : Controller
    {
        private chataVleseContext db = new chataVleseContext();

        //
        // GET: /Feedback/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View(db.Feedbacks.ToList().OrderBy(x => x.Date_In).Reverse());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //
        // GET: /Feedback/Details/5

        public ActionResult Details(int id = 0)
        {
            if (Request.IsAuthenticated)
            {
                Feedback feedback = db.Feedbacks.Find(id);
                if (feedback == null)
                {
                    return HttpNotFound();
                }
                return View(feedback);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        //
        // GET: /Feedback/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Feedback/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Feedback feedback)
        {
            String action = Request.Form["action"];
            String comment = Request.Form["feedback"];
            String name = Request.Form["name"];
            String strRating = Request.Form["rating"];
            int rating = 0;

            if (action.Equals("Zrušiť"))
            {
                return RedirectToAction("Index","Home");
            }

            if (strRating != null)
            {
                rating = Int32.Parse(strRating);
            }

            if (!comment.Equals("") && !name.Equals("") && rating > 0)
            {
                feedback = new Feedback();
                feedback.Name = name;
                feedback.Comment = comment;
                feedback.Rating = rating;
                feedback.Is_Valid = 1;
                feedback.Date_In = DateTime.Now;

                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                
                //odoslanie emailu
                String email_body = comment + " " + name;
                Email.sendEmail("Chata v lese - Nový komentár", email_body, "mazgut.jaroslav@gmail.com");

                return RedirectToAction("Index","Home");
            }

            return View(feedback);
        }

        //
        // GET: /Feedback/Edit/5

        public ActionResult Edit(int id = 0)
        {
            if (Request.IsAuthenticated)
            {
                Feedback feedback = db.Feedbacks.Find(id);
                if (feedback == null)
                {
                    return HttpNotFound();
                }
                return View(feedback);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //
        // POST: /Feedback/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Feedback feedback)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(feedback).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(feedback);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //
        // GET: /Feedback/Delete/5

        public ActionResult Delete(int id = 0)
        {
            if (Request.IsAuthenticated)
            {
                Feedback feedback = db.Feedbacks.Find(id);
                if (feedback == null)
                {
                    return HttpNotFound();
                }
                return View(feedback);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        //
        // POST: /Feedback/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAuthenticated)
            {
                Feedback feedback = db.Feedbacks.Find(id);
                db.Feedbacks.Remove(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}