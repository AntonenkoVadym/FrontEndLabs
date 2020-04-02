using GymProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GymProject.Controllers
{
    public class ClientsApiController : ApiController
    {
        private GymContext db = new GymContext();
        public IEnumerable<Client> Get()
        {
            return db.Clients.ToList();
        }

        public Client Get(string id)
        {
            return db.Clients.Find(Convert.ToInt32(id));
        }

        // POST api/users
        public void Post([FromBody]Client value)
        {
                db.Clients.Add(value);
                db.SaveChanges();
        }

        // PUT api/users/5
        public void Put(String id, [FromBody]Client value)
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
            Client client = db.Clients.Find(Convert.ToInt32(id));
            db.Clients.Remove(client);
            db.SaveChanges();
        }


    }
}
