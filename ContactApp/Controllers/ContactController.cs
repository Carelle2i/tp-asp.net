using Microsoft.AspNetCore.Mvc;
using ContactApp.Models;
using System.Collections.Generic;

namespace ContactApp.Controllers
{
    public class ContactController : Controller
    {
        // Données factices pour afficher la liste des contacts
        private List<Contact> GetContacts()
        {
            return new List<Contact>
            {
                new Contact { Id = 1, Name = "Alice", Email = "alice@example.com", Phone = "123-456-7890" },
                new Contact { Id = 2, Name = "Bob", Email = "bob@example.com", Phone = "987-654-3210" },
                new Contact { Id = 3, Name = "Charlie", Email = "charlie@example.com", Phone = "555-555-5555" }
            };
        }

        // Route pour afficher la liste des contacts
        public IActionResult Index()
        {
            // Utilisation de ViewBag pour envoyer un message
            ViewBag.Message = "Voici la liste des contacts.";

            // Utilisation de ViewData pour une autre donnée
            ViewData["Description"] = "Liste des contacts disponibles dans notre application.";

            // Utilisation de Model pour passer la liste complète des contacts
            var contacts = GetContacts();
            return View(contacts);  // Envoi de la liste des contacts à la vue
        }

        // Route pour afficher un contact spécifique
        public IActionResult Details(int id)
        {
            var contact = GetContacts().Find(c => c.Id == id);
            
            // Passer le contact via Model
            return View(contact);  // Envoi du contact spécifique à la vue
        }

        // Route pour ajouter un contact
        [HttpGet]
        public IActionResult Add()
        {
            return View();  // Affichage du formulaire d'ajout
        }

        // Action pour traiter le formulaire d'ajout de contact
        [HttpPost]
        public IActionResult Add(Contact contact)
        {
            if (ModelState.IsValid)
            {
                // Ici on peut ajouter une logique pour enregistrer le contact (simulation)
                return RedirectToAction("Index");  // Rediriger vers la liste après ajout
            }
            return View(contact);  // Si le modèle est invalide, rester sur la page Add
        }
    }
}
