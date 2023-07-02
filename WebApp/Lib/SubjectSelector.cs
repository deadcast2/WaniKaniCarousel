using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Lib
{
    public static class SubjectSelector
    {
        public static DashboardViewModel GetNext()
        {
            using var context = new WebAppContext();

            var user = context.Users.FirstOrDefault() ?? new();

            if (!user.Subjects.Any())
                return new DashboardViewModel();

            return new DashboardViewModel(user, GetNextSubject(user, context));
        }

        private static Subject GetNextSubject(User user, WebAppContext context)
        {
            var currSeenLock = user.Subjects.Max(s => s.SeenLock);

            // Only show subjects relevant from the past two weeks.
            var subjects = context.Subjects.Where(s => s.UpdatedAt > DateTime.UtcNow.AddDays(-14));

            if (currSeenLock > 0)
            {
                var potentialSubjects = context.Subjects.Where(s => s.SeenLock == currSeenLock - 1);

                // When every subject has been viewed ignore the above predicate and just pick any at random.
                if (potentialSubjects.Any())
                {
                    subjects = potentialSubjects;
                }
            }

            var nextSubject = subjects.OrderBy(s => EF.Functions.Random()).First();

            nextSubject.SeenLock++;

            context.SaveChanges();

            return nextSubject;
        }
    }
}
