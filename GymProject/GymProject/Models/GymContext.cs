using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GymProject.Models
{
    public class GymContext : DbContext
    {
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}