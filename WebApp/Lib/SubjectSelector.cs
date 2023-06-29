using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Lib
{
    public static class SubjectSelector
    {
        public static DashboardViewModel GetNext()
        {
            using var context = new WebAppContext();

            if (!context.Subjects.Any())
                return new DashboardViewModel();

            var currSeenLock = context.Subjects.Max(s => s.SeenLock);

            if (currSeenLock == 0)
            {
                var subject = context.Subjects.OrderBy(s => EF.Functions.Random()).First();

                subject.SeenLock++;

                context.SaveChanges();

                return new DashboardViewModel(subject);
            }
            else
            {
                var subjectSet = context.Subjects.Where(s => s.SeenLock == currSeenLock - 1);

                if (!subjectSet.Any())
                {
                    var subject = context.Subjects.OrderBy(s => EF.Functions.Random()).First();

                    subject.SeenLock++;

                    context.SaveChanges();

                    return new DashboardViewModel(subject);
                }
                else
                {
                    var subject = context.Subjects.Where(s => s.SeenLock == currSeenLock - 1).OrderBy(s => EF.Functions.Random()).First();

                    subject.SeenLock++;

                    context.SaveChanges();

                    return new DashboardViewModel(subject);
                }
            }
        }
    }
}
