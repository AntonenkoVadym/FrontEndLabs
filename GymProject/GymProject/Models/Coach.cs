using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymProject.Models
{
    public class Coach
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Sport { get; set; }
    }
}