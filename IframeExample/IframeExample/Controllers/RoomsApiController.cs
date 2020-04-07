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
using IframeExample.Models;

namespace IframeExample.Controllers
{
    public class RoomsApiController : ApiController
    {
        private GymContext db = new GymContext();
        public IEnumerable<Room> Get()
        {
            return db.Rooms.ToList();
        }

        public Room Get(string id)
        {
            return db.Rooms.Find(Convert.ToInt32(id));
        }

        // POST api/users
        public void Post([FromBody]Room value)
        {
            db.Rooms.Add(value);
            db.SaveChanges();
        }

        // PUT api/users/5
        public void Put(String id, [FromBody]Room value)
        {
            if (ModelState.IsValid)
            {
                db.Entry(value).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE api/users/5
        public void Delete(string id)
        {
            Room Room = db.Rooms.Find(Convert.ToInt32(id));
            db.Rooms.Remove(Room);
            db.SaveChanges();
        }
    }
}