using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TodoListApp.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        // Constructeur de la classe PaginatedList
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        // Méthode pour créer une instance paginée à partir d'une source IQueryable
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            // Correction pour s'assurer que la pageIndex commence à 1 et est valide
            pageIndex = Math.Max(pageIndex, 1);  // pageIndex ne peut pas être inférieur à 1

            var count = await source.CountAsync();  // Nombre total d'éléments
            var items = await source.Skip((pageIndex - 1) * pageSize)  // Sauter les éléments des pages précédentes
                .Take(pageSize)  // Prendre seulement le nombre d'éléments par page
                .ToListAsync();  // Exécuter la requête de manière asynchrone

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}