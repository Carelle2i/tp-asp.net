using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TodoListApp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Ajouter l'authentification avec les cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout"; // Vous pouvez ajouter une route de déconnexion ici
    });

// Ajouter la gestion des sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Durée de la session
});

// Ajouter les contrôleurs avec vues
builder.Services.AddControllersWithViews();

// Configurer le DbContext avec la chaîne de connexion de la base de données
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Initialiser la base de données avec des données par défaut (Seeder)
DbInitializer.Initialize(app.Services, app.Services.GetRequiredService<ApplicationDbContext>());

// Configurer le pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Afficher les détails des erreurs en mode développement
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Gestion des erreurs en production
    app.UseHsts(); // Utiliser HSTS en production
}

app.UseHttpsRedirection(); // Rediriger toutes les requêtes HTTP vers HTTPS
app.UseStaticFiles(); // Servir les fichiers statiques (CSS, JS, images, etc.)
app.UseRouting(); // Configurer le routage

// Ajouter l'authentification, l'autorisation et la gestion des sessions
app.UseAuthentication();
app.UseAuthorization();
app.UseSession(); // Ajouter la gestion des sessions

// Configurer la route par défaut
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todo}/{action=Index}/{id?}");

// Lancer l'application
app.Run();
