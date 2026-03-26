using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using BusinessObjects.Entity;
using BusinessObjects.Enum;

namespace DataAccessLayer.Contexts
{
    /// <summary>
    /// LibraryContext est le contexte Entity Framework pour la base de données de la bibliothèque.
    /// Il gère la connexion à SQLite et définit tous les DbSets pour les entités.
    /// </summary>
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        // DbSets représentant les tables de la base de données
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Library> Libraries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurer la conversion TypeBook string ↔ enum
            var converter = new ValueConverter<TypeBook, string>(
                v => v.ToString(),
                v => (TypeBook)Enum.Parse(typeof(TypeBook), v));

            modelBuilder.Entity<Book>()
                .Property(e => e.Type)
                .HasConversion(converter);

            // Configuration de la relation Book-Author (One-to-Many)
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration de la relation Book-Library (Many-to-Many)
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Libraries)
                .WithMany(l => l.Books)
                .UsingEntity(j => j.ToTable("book_library"));
        }
    }
}



