using System;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Services.Services;
using BusinessObjects.Enum;
using BusinessObjects.Entity;
using BusinessObjects;
using DataAccessLayer.Repository;
using DataAccessLayer.Contexts;

namespace LibraryManager.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 1. Créer le host avec la configuration des services (conteneur d'injection de dépendances)
            var host = CreateHostBuilder();

            // 2. Créer une portée de service pour récupérer les services
            using var serviceScope = host.Services.CreateScope();
            var services = serviceScope.ServiceProvider;

            // 3. Récupérer le CatalogManager du conteneur DI
            // Toutes les dépendances sont automatiquement injectées
            ICatalogManager catalogManager = services.GetRequiredService<ICatalogManager>();

            // Afficher tous les livres
            Console.WriteLine("=== Catalogue Complet (depuis la Base de Données) ===");
            var allBooks = catalogManager.GetCatalog();
            foreach (var book in allBooks)
            {
                Console.WriteLine($"- {book.Name} ({book.Type})");
            }

            // Filtrer et afficher uniquement les livres d'aventure
            Console.WriteLine("\n=== Livres d'Aventure Uniquement ===");
            var adventureBooks = catalogManager.GetCatalog(TypeBook.Aventure);
            foreach (var book in adventureBooks)
            {
                Console.WriteLine($"- {book.Name} (ID: {book.Id})");
            }

            // Afficher un livre spécifique par son ID
            Console.WriteLine("\n=== Rechercher un Livre par ID ===");
            var bookById = catalogManager.FindBook(1);
            if (bookById != null)
            {
                Console.WriteLine($"Livre trouvé : {bookById.Name} - {bookById.Type}");
            }
            else
            {
                Console.WriteLine("Livre non trouvé");
            }
        }

        /// <summary>
        /// Crée et configure le host avec l'injection de dépendances.
        /// C'est ici que tous les services et repositories sont enregistrés.
        /// </summary>
        private static IHost CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    // Configurer Entity Framework Core avec SQLite
                    string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "library.db");
                    services.AddDbContext<LibraryContext>(options =>
                    {
                        options.UseSqlite($"Data Source={dbPath}");
                    });

                    // Enregistrer GenericRepository<T> pour remplacer tous les repositories spécifiques
                    // Utilise la réflexion pour gérer tout type d'entité qui implémente IEntity
                    services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

                    // Enregistrer les services avec une durée de vie Transient
                    // La dépendance du CatalogManager (IGenericRepository<Book>) sera automatiquement injectée
                    services.AddTransient<ICatalogManager, CatalogManager>();
                })
                .Build();
        }
    }
}
