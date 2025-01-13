using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactApp.Models;

namespace ContactApp.Controllers;

public class HomeController : Controller
{
    // Route pour afficher la liste des contacts
    public IActionResult Index()
    {
        return View();
    }

    // Route pour afficher un contact spécifique
    public IActionResult Details(int id)
    {
        // Pour l'instant, on passe l'ID du contact dans la vue
        ViewBag.ContactId = id;
        return View();
    }

    // Route pour ajouter un contact
    public IActionResult Add()
    {
        return View();
    }
}

