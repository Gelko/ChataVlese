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
    public class ReservationController : Controller
    {
        private chataVleseContext db = new chataVleseContext();

        //
        // GET: /Reservation/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View(db.Reservations.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //
        // GET: /Reservation/Details/5

        public ActionResult Details(int id = 0)
        {
            if (Request.IsAuthenticated)
            {
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }
                return View(reservation);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //
        // GET: /Reservation/Create

        public ActionResult Create()
        {
            ViewData["DefaultDateStart"] = DateTime.Today;
            ViewData["DefaultDateEnd"] = DateTime.Today;
            ViewData["Reservations"] = db.Reservations.ToList();
            return View();
        }

        //
        // POST: /Reservation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.Date_In = DateTime.Now;
                db.Reservations.Add(reservation);
                db.SaveChanges();

                //email notifikacia
                String email_body = "\n Meno: " + reservation.FirstName;
                email_body = email_body + "\n Priezvisko: " + reservation.LastName;
                email_body = email_body + "\n Tel.číslo: " + reservation.PhoneNr.ToString();
                email_body = email_body + "\n Email: " + reservation.Email;
                email_body = email_body + "\n Počet osôb: " + reservation.Persons.ToString();
                email_body = email_body + "\n Príchod: " + reservation.DateFrom.ToString();
                email_body = email_body + "\n Odchod: " + reservation.DateTo.ToString();
                email_body = email_body + "\n Poznámka: " + reservation.Comment;

                String admin_email = "Na stránke Chaty v lese bola vytvorená nová rezervácia! \n\n" + email_body + "\n\n S pozdravom tvoja Chata v Lese!";
                //email administratorovi
                Email.sendEmail("Chata v lese - Nová rezervácia", admin_email, "lubos.gelcinsky@gmail.com");
                //email zakaznikovi
                String customer_email = "Dobrý deň, \n\n";
                customer_email = customer_email + "ďakujeme Vám za vytvorenie rezervácie na našej chate!\n";
                customer_email = customer_email + "Pre potvrdenie rezervácie Vás budeme kontaktovať najbližšie dni. Ak chcete mať rezervovanú chatu čím skôr neváhajte nám zavolať na číslo uvedené na stránke Chaty v lese v sekcii Kontakty.";
                customer_email = customer_email + "\n\n Detail rezervácie \n";
                customer_email = customer_email + email_body;
                Email.sendEmail("Potvrdenie rezervácie Chaty v lese", customer_email, reservation.Email);

                return RedirectToAction("Create");
            }

            return View(reservation);
        }

        //
        // GET: /Reservation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            if (Request.IsAuthenticated)
            {
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }
                return View(reservation);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //
        // POST: /Reservation/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reservation reservation)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(reservation).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(reservation);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //
        // GET: /Reservation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            if (Request.IsAuthenticated)
            {
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }
                return View(reservation);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //
        // POST: /Reservation/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAuthenticated)
            {
                Reservation reservation = db.Reservations.Find(id);
                db.Reservations.Remove(reservation);
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