using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Controllers
{
    public class HomeController : Controller
    {
        // Action pour la page d'accueil (par défaut)
        public IActionResult Index()
        {
            // Vous pouvez afficher une vue d'accueil ou simplement une page de bienvenue.
            return View();
        }

        // Action pour la page "About"
        public IActionResult About()
        {
            ViewData["Message"] = "Votre application de gestion des contacts.";
            return View();
        }

        // Action pour la page "Contact"
        public IActionResult Contact()
        {
            ViewData["Message"] = "Page de contact.";
            return View();
        }

        // Action pour la page "Privacy"
        public IActionResult Privacy()
        {
            return View();
        }
    }
}

