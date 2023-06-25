using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp
{
    public class WebAppContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectMeaning> SubjectMeanings { get; set; }
        public DbSet<SubjectReading> SubjectReadings { get; set; }

        public string DbPath { get; }

        public WebAppContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);

            DbPath = Path.Join(path, "webapp.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
