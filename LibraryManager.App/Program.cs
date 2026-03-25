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
        }
    }
}
// End of Program
