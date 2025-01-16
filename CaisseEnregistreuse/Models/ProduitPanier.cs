using CaisseEnregistreuse.Models;

namespace CaisseEnregistreuse.Models
{
    public class ProduitPanier
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public decimal Prix { get; set; }
        public int Quantite { get; set; }
    }
}