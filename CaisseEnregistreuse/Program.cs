using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using CaisseEnregistreuse.Models;
using CaisseEnregistreuse.Data;
using CaisseEnregistreuse.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration de la chaîne de connexion à la base de données
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter le service PanierService pour gérer le panier
builder.Services.AddSingleton<PanierService>();

// Ajouter IHttpContextAccessor pour accéder au contexte HTTP dans PanierService
builder.Services.AddHttpContextAccessor();  

// Configuration des services MVC
builder.Services.AddControllersWithViews();

// Configuration des sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Durée de session
});

var app = builder.Build();

// Utilisation des sessions
app.UseSession();

// Sécurisation de l'application en production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

