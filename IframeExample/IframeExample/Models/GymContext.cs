using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IframeExample.Models
{
    public class GymContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
    }
}