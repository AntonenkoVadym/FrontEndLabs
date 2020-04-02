using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GymProject.Models;

namespace GymProject.Controllers
{
    public class CoachesApiController : ApiController
    {
        private GymContext db = new GymContext();

        // GET: api/CoachesApi
        public IQueryable<Coach> GetCoaches()
        {
            return db.Coaches;
        }

        // GET: api/CoachesApi/5
        [ResponseType(typeof(Coach))]
        public IHttpActionResult GetCoach(int id)
        {
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return NotFound();
            }

            return Ok(coach);
        }

        // PUT: api/CoachesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoach(int id, Coach coach)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coach.Id)
            {
                return BadRequest();
            }

            db.Entry(coach).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoachExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CoachesApi
        [ResponseType(typeof(Coach))]
        public IHttpActionResult PostCoach(Coach coach)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Coaches.Add(coach);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = coach.Id }, coach);
        }

        // DELETE: api/CoachesApi/5
        [ResponseType(typeof(Coach))]
        public IHttpActionResult DeleteCoach(int id)
        {
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return NotFound();
            }

            db.Coaches.Remove(coach);
            db.SaveChanges();

            return Ok(coach);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoachExists(int id)
        {
            return db.Coaches.Count(e => e.Id == id) > 0;
        }
    }
}