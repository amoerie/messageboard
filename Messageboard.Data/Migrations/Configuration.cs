using System.Collections.Generic;
using Messageboard.Domain.Models;

namespace Messageboard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Messageboard.Data.Database.MessageboardContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Data.Database.MessageboardContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            if (context.Topics.Any())
                return;

            var examens = new Topic()
            {
                Title = "Angular JS is aanzienlijk beter dan zelf alles doen met jQuery",
                CreationDate = DateTime.Now.AddMinutes(-40),
                Body = "'t Is nogal een verschil zeg, vroeger moest ik met jQuery selectors en manuele ajax calls die inhoud invullen" +
                       " maar nu kan ik in mijn view veel declaratiever programmeren en zit mijn javascript code ook mooi modulair opgebouwd." +
                       "Ik kan zelfs unit testen schrijven voor mijn javascript code nu!",
                Replies = new List<Reply>()
                {
                    new Reply()
                    {
                        Body = "Awel da's goe gezegd",
                        CreationDate = DateTime.Now.AddMinutes(-38)
                    },
                    new Reply()
                    {
                        Body = "So true!",
                        CreationDate = DateTime.Now.AddMinutes(-33)
                    },
                    new Reply()
                    {
                        Body = "Help ik ben niet goed met computers",
                        CreationDate = DateTime.Now.AddMinutes(-31)
                    }
                }
            };
            var workshop = new Topic()
            {
                Title = "Dit is de beste workshop *ever*",
                CreationDate = DateTime.Now.AddMinutes(-90),
                Body = "Ik ga gegarandeerd een positieve review schrijven over deze geweldige kerel",
                Replies = new List<Reply>()
                {
                    new Reply()
                    {
                        Body = "Ik ga voortaan alles in AngularJs maken!",
                        CreationDate = DateTime.Now.AddMinutes(-70)
                    },
                    new Reply()
                    {
                        Body = "Prikbord applicaties zijn een perfect educatief voorbeeld!",
                        CreationDate = DateTime.Now.AddMinutes(-62)
                    },
                    new Reply()
                    {
                        Body = "Ik roep graag dingen!!!!",
                        CreationDate = DateTime.Now.AddMinutes(-58)
                    },
                }
            };

            context.Topics.Add(examens);
            context.Topics.Add(workshop);
            context.SaveChanges();
        }
    }
}
