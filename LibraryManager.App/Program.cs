using System;
using System.Collections.Generic;

namespace LibraryManager.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create a list of books and ensure at least one is of type "Aventure"
            List<Book> books = new List<Book>
            {
                new Book { Name = "Les Aventures de Tom Sawyer", Type = "Aventure" },
                new Book { Name = "Clean Code", Type = "Programming" },
                new Book { Name = "Le Petit Prince", Type = "Fiction" }
            };

            Console.WriteLine("Book list:");
            foreach (var book in books)
            {
                Console.WriteLine($"- {book.Name} ({book.Type})");
            }

            // Exercice LINQ : Filtrer les livres de type "Aventure"
            Console.WriteLine("\n--- Livres de type 'Aventure' ---");
            
            // Méthode 1 : Filtrer puis afficher
            var adventureBooks = books.Where(book => book.Type == "Aventure");
            foreach (var book in adventureBooks)
            {
                Console.WriteLine($"Aventure : {book.Name}");
            }

            // Méthode 2 : Tout en une ligne (chaînage de méthodes)
            Console.WriteLine("\n--- Avec ForEach ---");
            books.Where(book => book.Type == "Aventure")
                 .ToList()
                 .ForEach(book => Console.WriteLine($"Aventure : {book.Name}"));
        }
    }
}
// End of Program
