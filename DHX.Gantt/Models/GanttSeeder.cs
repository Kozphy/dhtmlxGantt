using Microsoft.EntityFrameworkCore;

namespace DHX.Gantt.Models
{
    public static class GanttSeeder
    {
        public static void Seed(GanttContext context)
        {
            if (context.Tasks.Any())
            {
                return;
            }

            using (var transaction = context.Database.BeginTransaction())
            {
                // tasks
                List<Task> tasks = new List<Task>()
                {
                    new Task()
                    {
                        Id = 1,
                        Text = "Project #2",
                        StartDate = DateTime.Today.AddDays(-3),
                        Duration = 18,
                        Progress = 0.4m,
                        ParentId = null
                    }
                };
                tasks.ForEach(s => context.Tasks.Add(s));
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Tasks ON");
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Tasks OFF");

                // Link
                List<Link> links = new List<Link>()
                {
                     new Link(){Id = 1, SourceTaskId = 1,TargetTaskId = 2,Type = "1"},
                };

                links.ForEach(s => context.Links.Add(s));
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Links ON");
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Links OFF");
                transaction.Commit();
            }
        }
    }
}
