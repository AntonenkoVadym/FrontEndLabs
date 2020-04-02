using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GymProject.Models
{
    public class GymDbInitializer : DropCreateDatabaseAlways<GymContext>
    {
        protected override void Seed(GymContext db)
        {
            db.Coaches.Add(new Coach { Name = "Война и мир", Age = 50, Sport = "Л. Толстой" });
            db.Coaches.Add(new Coach { Name = "Великий Гетсбі", Age = 38, Sport = "Перегони" });
            db.Coaches.Add(new Coach { Name = "Київ", Age = 1550, Sport = "Либідь" });

            db.Clients.Add(new Client { Name = "Boom Boom", Age = 25, CoachId = 1 });
            db.Clients.Add(new Client { Name = "Timon", Age = 15, CoachId = 3 });
            db.Clients.Add(new Client { Name = "Pumba", Age = 45, CoachId = 2 });

            db.Rooms.Add(new Room { Name = "Strong", Floor = 25, Sport = "Powerlifting" });
            db.Rooms.Add(new Room { Name = "Cardio", Floor = 2, Sport="Running" });
            db.Rooms.Add(new Room { Name = "Swimming Pool", Floor = 5, Sport = "Swimming" });

            base.Seed(db);
        }
    }
}