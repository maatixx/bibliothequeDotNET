using System;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Services.Services;
using BusinessObjects.Enum;
using BusinessObjects.Entity;
using DataAccessLayer.Repository;

namespace LibraryManager.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 1. Create the host with configured services (Dependency Injection container)
            var host = CreateHostBuilder();

            // 2. Create a service scope to retrieve services
            using var serviceScope = host.Services.CreateScope();
            var services = serviceScope.ServiceProvider;

            // 3. Retrieve the CatalogManager from the DI container
            // All dependencies are automatically injected (BookRepository is injected into CatalogManager)
            ICatalogManager catalogManager = services.GetRequiredService<ICatalogManager>();

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

        /// <summary>
        /// Creates and configures the host with dependency injection.
        /// This is where all services and repositories are registered.
        /// </summary>
        private static IHost CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    // Register repositories with Transient lifetime
                    // Transient: New instance created each time
                    services.AddTransient<IGenericRepository<Book>, BookRepository>();
                    services.AddTransient<IGenericRepository<Author>, AuthorRepository>();
                    services.AddTransient<IGenericRepository<Library>, LibraryRepository>();

                    // Register services with Transient lifetime
                    // The CatalogManager dependency (IGenericRepository<Book>) will be automatically injected
                    services.AddTransient<ICatalogManager, CatalogManager>();
                })
                .Build();
        }
    }
}
