namespace CaisseEnregistreuse.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public List<Produit> Produits { get; set; }
    }
    
}