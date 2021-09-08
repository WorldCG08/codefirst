using System;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            // var context = new PlutoContext();
            //
            // // LINQ syntax
            // var query =
            //     from c in context.Courses
            //     where c.Name.Contains("c#")
            //     orderby c.Name
            //     select c;
            //
            // foreach (var course in query)
            // {
            //     //Console.WriteLine(course.Name);
            // }
            //
            // // Extension methods
            // var courses = context.Courses
            //     .Where(c => c.Name.Contains("c#"))
            //     .OrderBy(c => c.Name);
            //
            // foreach (var course in courses)
            // {
            //     Console.WriteLine(course.Name);
            // }
            
            
            var context = new PlutoContext();
            
            var restriction =
                from c in context.Courses
                where c.Level == 1 && c.Author.Id == 1
                select c;
            
            var ordering =
                from c in context.Courses
                where c.Author.Id == 1
                orderby c.Level descending, c.Name
                select c;
            
            var ordering2 =
                from c in context.Courses
                where c.Author.Id == 1
                orderby c.Level descending, c.Name
                select c;
            
            var projection =
                from c in context.Courses
                where c.Author.Id == 1
                orderby c.Level descending, c.Name
                select new { Namme = c.Name, Author = c.Author.Name };

            var grouping =
                from c in context.Courses
                group c by c.Level
                into g
                select g;


        }
    }
}
