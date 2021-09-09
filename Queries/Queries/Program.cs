using System;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();
            
            // LINQ Extension Methods
            context.Courses
                .Where(c => c.Level == 1)
                .OrderBy(c => c.Name)
                .ThenByDescending(c => c.Level);
            
            // Projection
            context.Courses
                .Where(c => c.Level == 1)
                .OrderBy(c => c.Name)
                .ThenByDescending(c => c.Level)
                .Select(c => new { CourseName = c.Name, AuthorName = c.Author.Name });
            
            var courses = context.Courses
                .Where(c => c.Level == 1)
                .OrderBy(c => c.Name)
                .ThenByDescending(c => c.Level)
                .Select(c => c.Tags);
            
            var tags = context.Courses
                .Where(c => c.Level == 1)
                .OrderBy(c => c.Name)
                .ThenByDescending(c => c.Level)
                .SelectMany(c => c.Tags);

            foreach (var course in courses)
            {
                foreach (var tag1 in course)
                {
                    Console.WriteLine(tag1);
                }
            }
            
            // Set operators
            var distinct = context.Courses
                .Where(c => c.Level == 1)
                .OrderBy(c => c.Name)
                .ThenByDescending(c => c.Level)
                .SelectMany(c => c.Tags)
                .Distinct();
            
            // Grouping
            var groups = context.Courses.GroupBy(c => c.Level);

            foreach (var group in groups)
            {
                Console.WriteLine("Key: " + group.Key);

                foreach (var course in group)
                {
                    Console.WriteLine("\t" + course.Name);
                }
            }
            
            // joins
            // Inner join
            context.Courses.Join(context.Authors,
                c => c.AuthorId,
                a => a.Id,
                (course, author) => new
            {
                CourseName = course.Name,
                AuthorName = author.Name
            });
            
            // Group Join
            context.Authors.GroupJoin(context.Courses, a => a.Id, c => c.AuthorId, (author, coursess) => new
            {
                Author = author.Name,
                courses = coursess.Count()
            });
            
            // Cross join
            context.Authors.SelectMany(a => context.Courses, (author, course) => new
            {
                AuthorName = author.Name,
                CourseName = course.Name
            });
            
            // Partition
            var partition = context.Courses.Skip(10).Take(10);
            context.Courses.OrderBy(c => c.Level).First();
            context.Courses.OrderBy(c => c.Level).FirstOrDefault(c => c.FullPrice > 100);
            context.Courses.First();
            context.Courses.SingleOrDefault(c => c.Id == 1);
            context.Courses.Single(c => c.Id == 1);
            
            
            // Quantifying
            context.Courses.All(c => c.FullPrice > 10);
            context.Courses.Any(c => c.Level == 1);
            
            // Aggregating
            var count = context.Courses.Count();
            var countWhere = context.Courses.Where(c => c.Level == 1).Count();
            
            var max = context.Courses.Max(c => c.FullPrice);
            var min = context.Courses.Min(c => c.FullPrice);
            var average = context.Courses.Average(c => c.FullPrice);

        }
        
        // Rename to Main if need to work
        static void Main2(string[] args)
        {
            var context = new PlutoContext();
            
            // LINQ syntax
            var query =
                from c in context.Courses
                where c.Name.Contains("c#")
                orderby c.Name
                select c;
            
            foreach (var course in query)
            {
                //Console.WriteLine(course.Name);
            }
            
            // Extension methods
            var courses = context.Courses
                .Where(c => c.Name.Contains("c#"))
                .OrderBy(c => c.Name);
            
            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
            }


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
