using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListApp.Models;
using TodoListApp.Data;
using Microsoft.AspNetCore.Authorization;  
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TodoListApp.Controllers
{
    [Authorize]  
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int PageSize = 5; // Nombre de tâches par page

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action pour afficher la liste des tâches avec pagination
        public async Task<IActionResult> Index(int page = 1)
        {
            // Vérification de l'authentification dans la session
            var username = HttpContext.Session.GetString("Username"); // Récupérer le nom d'utilisateur de la session
            if (username != null)
            {
                ViewData["Username"] = username;  // Afficher le nom d'utilisateur dans la vue
            }

            // Utilisation de PaginatedList pour récupérer les tâches paginées
            var tasks = await PaginatedList<TodoTask>.CreateAsync(_context.TodoTasks.AsQueryable(), page, PageSize);
            return View(tasks);
        }

        // Action pour afficher le formulaire de tâche (ajouter ou modifier)
        public IActionResult Form(int? id)
        {
            if (id == null)
                return View(new TodoTask()); // Créer une nouvelle tâche

            var task = _context.TodoTasks.Find(id);
            if (task == null)
                return NotFound();

            return View(task);
        }

        // Action pour ajouter ou modifier une tâche
        [HttpPost]
        public async Task<IActionResult> Form(TodoTask task)
        {
            if (ModelState.IsValid)
            {
                if (task.Id == 0)
                    _context.Add(task);  // Ajouter une tâche
                else
                    _context.Update(task);  // Mettre à jour une tâche

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(task);
        }

        // Action pour marquer une tâche comme terminée
        public async Task<IActionResult> MarkAsCompleted(int id)
        {
            var task = await _context.TodoTasks.FindAsync(id);
            if (task == null)
                return NotFound();

            task.IsCompleted = true;
            _context.Update(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Action pour supprimer une tâche
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.TodoTasks.FindAsync(id);
            if (task == null)
                return NotFound();

            _context.TodoTasks.Remove(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Action pour la déconnexion
        public async Task<IActionResult> Logout()
        {
            // Supprime l'authentification et les données de session
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);  // Signer l'utilisateur
            HttpContext.Session.Clear();  // Supprime les données de session
            return RedirectToAction("Index", "Home");  // Redirige vers la page d'accueil ou une page de connexion
        }

    }
}
