using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GymProject.Models;

namespace GymProject.Controllers
{
    public class CoachesController : Controller
    {
        public const String COACH_ID = "coach_id";
        private GymContext db = new GymContext();

        // GET: Coaches
        public ActionResult Index()
        {
            //return View(db.Coaches.ToList());
            return View();
        }

        [HttpGet]
        public ActionResult GetCoaches()
        {
            return Json(db.Coaches.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Coaches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Coaches/Create
        [HttpPost]
        public ActionResult Create(Coach coach)
        {
            if (ModelState.IsValid)
            {
                db.Coaches.Add(coach);
                db.SaveChanges();
                return RedirectToAction("Index");
        }
            return View(coach);
    }

        // GET: Coaches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,Sport")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coach);
        }

        // GET: Coaches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return Json(coach);
        }

        [HttpDelete]
        public ActionResult Delete(int id, Coach collection)
        {
            try
            {
                Coach coach = db.Coaches.Find(id);
                db.Coaches.Remove(coach);
                db.SaveChanges();
                return Json(coach);
            }
            catch
            {
                return View();
            }
        }

        //// POST: Coaches/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Coach coach = db.Coaches.Find(id);
        //    db.Coaches.Remove(coach);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public ActionResult SelectCoach(string id)
        {
            Session.Add(COACH_ID, id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
