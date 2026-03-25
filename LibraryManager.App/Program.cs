using System;
using System.Collections.Generic;
using Services.Services;
using BusinessObjects.Enum;

namespace LibraryManager.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create the CatalogManager (service layer)
            // This replaces direct repository access with business logic
            CatalogManager catalogManager = new CatalogManager();

            // Display all books using the service
            Console.WriteLine("=== Complete Catalog ===");
            var allBooks = catalogManager.GetCatalog();
            foreach (var book in allBooks)
            {
                Console.WriteLine($"- {book.Name} ({book.Type})");
            }

            // Filter and display adventure books only using the service
            Console.WriteLine("\n=== Adventure Books Only ===");
            var adventureBooks = catalogManager.GetCatalog(TypeBook.Aventure);
            foreach (var book in adventureBooks)
            {
                Console.WriteLine($"- {book.Name} (ID: {book.Id})");
            }

            // Display a specific book by ID using the service
            Console.WriteLine("\n=== Get Book by ID ===");
            var bookById = catalogManager.FindBook(1);
            if (bookById != null)
            {
                Console.WriteLine($"Book found: {bookById.Name} - {bookById.Type}");
            }
            else
            {
                Console.WriteLine("Book not found");
            }
        }
    }
}
