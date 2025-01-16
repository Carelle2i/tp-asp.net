using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaisseEnregistreuse.Data;
using CaisseEnregistreuse.Models;
using CaisseEnregistreuse.Services; 

namespace CaisseEnregistreuse.Controllers
{
    public class CaisseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PanierService _panierService;

        public CaisseController(ApplicationDbContext context, PanierService panierService)
        {
            _context = context;
            _panierService = panierService;
        }

        // Liste des produits
        public async Task<IActionResult> Index()
        {
            var produits = await _context.Produits.Include(p => p.Categorie).ToListAsync();
            return View(produits);
        }

        // Détails d'un produit
        public async Task<IActionResult> Details(int id)
        {
            var produit = await _context.Produits.Include(p => p.Categorie).FirstOrDefaultAsync(p => p.Id == id);
            if (produit == null)
            {
                return NotFound();
            }
            return View(produit);
        }

        // Ajouter un produit au panier
        [HttpPost]
        public IActionResult AjouterAuPanier(int id)
        {
            var produit = _context.Produits.FirstOrDefault(p => p.Id == id);
            if (produit == null)
            {
                return NotFound();
            }

            _panierService.AjouterAuPanier(produit);
            return RedirectToAction("Panier");
        }

        // Afficher le panier
        public IActionResult Panier()
        {
            var panier = _panierService.GetPanier();
            decimal total = panier.Sum(p => p.Prix * p.Quantite);
            ViewBag.Total = total;
            return View(panier);
        }

        // Vider le panier
        public IActionResult ViderPanier()
        {
            _panierService.ViderPanier();
            return RedirectToAction("Panier");
        }

        // Passer la commande (fonctionnalité à définir)
        public IActionResult PasserCommande()
        {
            // Logique pour passer la commande
            _panierService.ViderPanier();
            return RedirectToAction("Index");
        }
    }
}

