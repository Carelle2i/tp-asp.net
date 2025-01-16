using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TodoListApp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using TodoListApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuration des services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Durée de la session
});

// Configuration de l'authentification avec cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout"; // Ajoutez une route de déconnexion si nécessaire
    });

builder.Services.AddControllersWithViews();

// Configuration du DbContext avec la chaîne de connexion à la base de données
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter Identity avec votre ApplicationUser personnalisé
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
// Construction de l'application
var app = builder.Build();

// Initialisation de la base de données après que l'application ait été construite
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Initialize(services, context);  // Appel de la méthode DbInitializer
}

// Middleware pour gérer l'authentification, l'autorisation et la session
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

// Configuration du pipeline HTTP
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Configurer les routes de l'application
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todo}/{action=Index}/{id?}");

app.Run();

