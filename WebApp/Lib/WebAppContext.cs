using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Lib
{
    public class WebAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectMeaning> SubjectMeanings { get; set; }
        public DbSet<SubjectReading> SubjectReadings { get; set; }

        public static string DbPath
        {
            get
            {
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);

                return Path.Join(path, "WaniKaniCarousel.db");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
