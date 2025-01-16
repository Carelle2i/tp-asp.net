using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TodoListApp.Models;
using System.Threading.Tasks;

namespace TodoListApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Page de connexion
        public IActionResult Login()
        {
            return View();
        }

        // Traitement de la connexion
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Todo");  // Redirige vers la page d'accueil ou une page de votre choix
            }
            ModelState.AddModelError("", "Nom d'utilisateur ou mot de passe incorrect.");
            return View();
        }

        // Page d'inscription
        public IActionResult Register()
        {
            return View();
        }

        // Traitement de l'inscription
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new ApplicationUser { UserName = username };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Todo");  // Rediriger vers la page d'accueil après inscription
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }

        // Déconnexion
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();  // Déconnexion
            return RedirectToAction("Index", "Home");  // Rediriger vers la page d'accueil
        }
    }
}
