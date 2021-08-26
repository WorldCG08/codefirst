using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Tracing;

namespace CodeFirst
{
    internal class Program
    {
        public static void Main(string[] args)
        {
        }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public CourseLevel Level { get; set; }
        public float FullPrice { get; set; }
        public IList<Tag> Tags { get; set; }
    }
    
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Course> Courses { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Course> Courses { get; set; }
    }

    public enum CourseLevel
    {
        Beginner = 1,
        Intermediate = 2,
        Advanced = 3
    }

    public class PlutoContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }

        public PlutoContext()
            : base("name=DefaultConnection")
        {
            
        }
    }
}