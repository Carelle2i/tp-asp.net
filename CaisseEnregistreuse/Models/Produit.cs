using CaisseEnregistreuse.Models;

namespace CaisseEnregistreuse.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public decimal Prix { get; set; }
        public int Quantite { get; set; }
        public string Image { get; set; }
        public int CategorieId { get; set; } 
        public Categorie Categorie { get; set; } 
    }
}