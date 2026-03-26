using Xunit;
using Moq;
using Services.Services;
using BusinessObjects.Entity;
using BusinessObjects.Enum;
using DataAccessLayer.Repository;

namespace Services.Test
{
    /// <summary>
    /// Tests unitaires pour la classe CatalogManager.
    /// Utilise des mocks pour tester isolément la logique métier.
    /// </summary>
    public class CatalogManagerTest
    {
        /// <summary>
        /// Test : GetCatalog() retourne tous les livres
        /// Pattern AAA : Arrange - Act - Assert
        /// </summary>
        [Fact]
        public void GetCatalog_ShouldReturnAllBooks()
        {
            // Arrange : Préparer les données de test
            var mockRepository = new Mock<IGenericRepository<Book>>();
            var booksTestData = new List<Book>
            {
                new Book 
                { 
                    Id = 1, 
                    Name = "Les Aventures de Tom Sawyer", 
                    Type = TypeBook.Aventure,
                    Isbn = "978-0-123456-78-9",
                    PublicationYear = 1876,
                    AuthorId = 1
                },
                new Book 
                { 
                    Id = 2, 
                    Name = "Clean Code", 
                    Type = TypeBook.Programming,
                    Isbn = "978-0-132350-88-6",
                    PublicationYear = 2008,
                    AuthorId = 2
                },
                new Book 
                { 
                    Id = 3, 
                    Name = "Le Petit Prince", 
                    Type = TypeBook.Fiction,
                    Isbn = "978-0-156012-95-9",
                    PublicationYear = 1943,
                    AuthorId = 3
                }
            };

            mockRepository.Setup(repo => repo.GetAll()).Returns(booksTestData);
            var catalogManager = new CatalogManager(mockRepository.Object);

            // Act : Exécuter la méthode à tester
            var result = catalogManager.GetCatalog();

            // Assert : Vérifier le résultat
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            mockRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        /// <summary>
        /// Test : GetCatalog(type) retourne uniquement les livres du type spécifié
        /// </summary>
        [Fact]
        public void GetCatalog_WithTypeFilter_ReturnsBooksOfSpecificType()
        {
            // Arrange
            var mockRepository = new Mock<IGenericRepository<Book>>();
            var booksTestData = new List<Book>
            {
                new Book 
                { 
                    Id = 1, 
                    Name = "Les Aventures de Tom Sawyer", 
                    Type = TypeBook.Aventure,
                    Isbn = "978-0-123456-78-9",
                    PublicationYear = 1876,
                    AuthorId = 1
                },
                new Book 
                { 
                    Id = 2, 
                    Name = "Clean Code", 
                    Type = TypeBook.Programming,
                    Isbn = "978-0-132350-88-6",
                    PublicationYear = 2008,
                    AuthorId = 2
                }
            };

            mockRepository.Setup(repo => repo.GetAll()).Returns(booksTestData);
            var catalogManager = new CatalogManager(mockRepository.Object);

            // Act
            var result = catalogManager.GetCatalog(TypeBook.Aventure);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Les Aventures de Tom Sawyer", result.First().Name);
            Assert.Equal(TypeBook.Aventure, result.First().Type);
        }

        /// <summary>
        /// Test : GetCatalog(type) retourne une liste vide si aucun livre du type spécifié
        /// </summary>
        [Fact]
        public void GetCatalog_WithTypeFilter_ReturnsEmptyWhenNoMatchingBooks()
        {
            // Arrange
            var mockRepository = new Mock<IGenericRepository<Book>>();
            var booksTestData = new List<Book>
            {
                new Book 
                { 
                    Id = 1, 
                    Name = "Les Aventures de Tom Sawyer", 
                    Type = TypeBook.Aventure,
                    Isbn = "978-0-123456-78-9",
                    PublicationYear = 1876,
                    AuthorId = 1
                }
            };

            mockRepository.Setup(repo => repo.GetAll()).Returns(booksTestData);
            var catalogManager = new CatalogManager(mockRepository.Object);

            // Act
            var result = catalogManager.GetCatalog(TypeBook.Programming);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        /// <summary>
        /// Test : FindBook(id) retourne le livre avec l'ID spécifié
        /// </summary>
        [Fact]
        public void FindBook_WithValidId_ReturnsCorrectBook()
        {
            // Arrange
            var mockRepository = new Mock<IGenericRepository<Book>>();
            var bookToFind = new Book 
            { 
                Id = 1, 
                Name = "Les Aventures de Tom Sawyer", 
                Type = TypeBook.Aventure,
                Isbn = "978-0-123456-78-9",
                PublicationYear = 1876,
                AuthorId = 1
            };

            mockRepository.Setup(repo => repo.Get(1)).Returns(bookToFind);
            var catalogManager = new CatalogManager(mockRepository.Object);

            // Act
            var result = catalogManager.FindBook(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Les Aventures de Tom Sawyer", result.Name);
            mockRepository.Verify(repo => repo.Get(1), Times.Once);
        }

        /// <summary>
        /// Test : FindBook(id) retourne null si l'ID n'existe pas
        /// </summary>
        [Fact]
        public void FindBook_WithInvalidId_ReturnsNull()
        {
            // Arrange
            var mockRepository = new Mock<IGenericRepository<Book>>();
            mockRepository.Setup(repo => repo.Get(999)).Returns((Book?)null);
            var catalogManager = new CatalogManager(mockRepository.Object);

            // Act
            var result = catalogManager.FindBook(999);

            // Assert
            Assert.Null(result);
            mockRepository.Verify(repo => repo.Get(999), Times.Once);
        }

        /// <summary>
        /// Test : Vérifie que le repository est appelé exactement une fois
        /// </summary>
        [Fact]
        public void GetCatalog_VerifiesRepositoryIsCalledOnce()
        {
            // Arrange
            var mockRepository = new Mock<IGenericRepository<Book>>();
            mockRepository.Setup(repo => repo.GetAll()).Returns(new List<Book>());
            var catalogManager = new CatalogManager(mockRepository.Object);

            // Act
            catalogManager.GetCatalog();

            // Assert
            mockRepository.Verify(repo => repo.GetAll(), Times.Once);
            mockRepository.Verify(repo => repo.Get(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Test : GetCatalog retourne une énumération non-nulle
        /// </summary>
        [Fact]
        public void GetCatalog_AlwaysReturnsNonNullEnumerable()
        {
            // Arrange
            var mockRepository = new Mock<IGenericRepository<Book>>();
            mockRepository.Setup(repo => repo.GetAll()).Returns(new List<Book>());
            var catalogManager = new CatalogManager(mockRepository.Object);

            // Act
            var result = catalogManager.GetCatalog();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Book>>(result);
        }
    }
}
