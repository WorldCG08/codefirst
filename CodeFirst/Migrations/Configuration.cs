
using System;
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
    } 
}