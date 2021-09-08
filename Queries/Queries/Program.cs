using System;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();
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

            foreach (var group in grouping)
            {
                Console.WriteLine(group.Key);

                foreach (var course in group)
                {
                    Console.WriteLine("\t{0}", course.Name);
                }
            }
            
            var aggregation  =
                from c in context.Courses
                group c by c.Level
                into g
                select g;

            foreach (var group in aggregation)
            {
                Console.WriteLine("{0} ({1})", group.Key, group.Count());
            }
            
            // Joins (Inner, Group, Cross)
            var innerJoinLike =
                from c in context.Courses
                select new {CourseName = c.Name, AuthorName = c.Author.Name};
            
            var innerJoin = 
                from c in context.Courses
                join a in context.Authors on c.AuthorId equals a.Id
                select new {CourseName = c.Name, AuthorName = a.Name};
            
            // Group join
            var groupJoin =
                from a in context.Authors
                join c in context.Courses on a.Id equals c.AuthorId into g
                select new {AuthorName = a.Name, CoursesCount = g.Count()};
            
            var crossJoin = 
                from a in context.Authors
                from c in context.Courses
                select new {AuthorName = a.Name, CourseName = c.Name};
        }
    }
}
