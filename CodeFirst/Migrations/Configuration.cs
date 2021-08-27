
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace CodeFirst.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CodeFirst.PlutoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PlutoContext context)
        {
            context.Authors.AddOrUpdate(a => a.Name, new Author
            {
                Name = "Author 1",
                Courses = new Collection<Course>()
                {
                    new Course { Name = "Course for Author 1", Description = "Descripton" }
                }
            });
        }
    } 
}