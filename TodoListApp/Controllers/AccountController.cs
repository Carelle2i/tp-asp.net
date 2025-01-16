using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TodoListApp.Controllers
{
    public class AccountController : Controller
    {
        // Page de connexion
        public IActionResult Login()
        {
            return View();
        }

        // Action de connexion
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Validation des informations d'identification (exemple basique)
            if (username == "admin" && password == "password") // Utilisez une validation plus robuste dans la production
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Todo");
            }

            ModelState.AddModelError("", "Nom d'utilisateur ou mot de passe incorrect");
            return View();
        }

        // Action de déconnexion
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Todo");
        }
    }
}