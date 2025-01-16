using TodoListApp.Models;  

namespace TodoListApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.TodoTasks.Any()) 
                return;
            
            var tasks = new TodoTask[]
            {
                new TodoTask { Title = "Rangement", Description = "Préparer la venue de belle-maman", IsCompleted = false },
                new TodoTask { Title = "Promenade", Description = "Promener Médor et faire attention au chien de la voisine", IsCompleted = true }
            };

            context.TodoTasks.AddRange(tasks);  // Utilisation correcte de AddRange
            context.SaveChanges();

        }
    }
}