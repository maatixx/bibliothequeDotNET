using System;
using System.Collections.Generic;
using DataAccessLayer.Repository;
using BusinessObjects.Enum;

namespace LibraryManager.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create the BookRepository
            BookRepository bookRepository = new BookRepository();

            // Display all books
            Console.WriteLine("=== All Books ===");
            var allBooks = bookRepository.GetAll();
            foreach (var book in allBooks)
            {
                Console.WriteLine($"- {book.Name} ({book.Type})");
            }

            // Filter and display adventure books only
            Console.WriteLine("\n=== Adventure Books Only ===");
            var adventureBooks = allBooks.Where(book => book.Type == TypeBook.Aventure);
            foreach (var book in adventureBooks)
            {
                Console.WriteLine($"- {book.Name} (ID: {book.Id})");
            }

            // Display a specific book by ID
            Console.WriteLine("\n=== Get Book by ID ===");
            var bookById = bookRepository.Get(1);
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
