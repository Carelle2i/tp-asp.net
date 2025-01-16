using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using CaisseEnregistreuse.Models;  
using CaisseEnregistreuse.Services; 

namespace CaisseEnregistreuse.Services
{
    public class PanierService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PanierService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<ProduitPanier> GetPanier()
        {
            var panier = _httpContextAccessor.HttpContext.Session.GetString("Panier");
            return string.IsNullOrEmpty(panier) ? new List<ProduitPanier>() : JsonSerializer.Deserialize<List<ProduitPanier>>(panier);
        }

        public void AjouterAuPanier(Produit produit)
        {
            var panier = GetPanier();
            var produitPanier = panier.FirstOrDefault(p => p.Id == produit.Id);
            if (produitPanier == null)
            {
                panier.Add(new ProduitPanier
                {
                    Id = produit.Id,
                    Nom = produit.Nom,
                    Prix = produit.Prix,
                    Quantite = 1
                });
            }
            else
            {
                produitPanier.Quantite++;
            }
            _httpContextAccessor.HttpContext.Session.SetString("Panier", JsonSerializer.Serialize(panier));
        }

        public void ViderPanier()
        {
            _httpContextAccessor.HttpContext.Session.Remove("Panier");
        }
    }
}