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
            var maxDisplayCount = user.Subjects.Max(s => s.DisplayCount);

            // Only show subjects relevant from the past week.
            var subjects = context.Subjects.Where(s => s.UpdatedAt > DateTime.UtcNow.AddDays(-7));

            if (maxDisplayCount > 0)
            {
                var potentialSubjects = context.Subjects.Where(s => s.DisplayCount == maxDisplayCount - 1);

                // When every subject has been viewed ignore the above predicate and just pick any at random.
                if (potentialSubjects.Any())
                {
                    subjects = potentialSubjects;
                }
            }

            var nextSubject = subjects.OrderBy(s => EF.Functions.Random()).First();

            nextSubject.DisplayCount++;

            context.SaveChanges();

            return nextSubject;
        }
    }
}
