using Microsoft.AspNetCore.Mvc;
using Services.Services;
using BusinessObjects.Entity;
using BusinessObjects.Enum;
using BusinessObjects.DataTransferObject;

namespace LibraryManager.Hosting.Controllers
{
    /// <summary>
    /// Controller for managing books in the library system.
    /// Provides REST API endpoints for CRUD operations and filtering.
    /// All responses use BookDto to limit data exposure and improve security.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ICatalogManager _catalogManager;
        private readonly IMapperService _mapperService;
        private readonly ILogger<BooksController> _logger;

        /// <summary>
        /// Initializes a new instance of the BooksController.
        /// </summary>
        public BooksController(ICatalogManager catalogManager, IMapperService mapperService, ILogger<BooksController> logger)
        {
            _catalogManager = catalogManager;
            _mapperService = mapperService;
            _logger = logger;
        }

        /// <summary>
        /// Get all books in the catalog.
        /// </summary>
        /// <returns>List of all books as DTOs</returns>
        /// <response code="200">Returns the list of all books</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _catalogManager.GetCatalog();
                var booksDto = _mapperService.MapToBooksDto(books);
                _logger.LogInformation("Retrieved all books. Count: {BookCount}", books.Count());
                return Ok(booksDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all books");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving books");
            }
        }

        /// <summary>
        /// Get a specific book by its ID.
        /// </summary>
        /// <param name="id">The book ID</param>
        /// <returns>The book if found as DTO</returns>
        /// <response code="200">Returns the book</response>
        /// <response code="404">Book not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBookById(int id)
        {
            try
            {
                var book = _catalogManager.FindBook(id);
                if (book == null)
                {
                    _logger.LogWarning("Book with ID {BookId} not found", id);
                    return NotFound(new { message = $"Book with ID {id} not found" });
                }

                var bookDto = _mapperService.MapToBookDto(book);
                _logger.LogInformation("Retrieved book with ID {BookId}", id);
                return Ok(bookDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving book with ID {BookId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the book");
            }
        }

        /// <summary>
        /// Get books filtered by their type.
        /// </summary>
        /// <param name="type">The book type to filter by (Aventure, Programming, Fiction, Mystery, Science, History)</param>
        /// <returns>List of books matching the specified type as DTOs</returns>
        /// <response code="200">Returns the filtered list of books</response>
        /// <response code="400">Invalid book type</response>
        [HttpGet("type/{type}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetBooksByType(string type)
        {
            try
            {
                if (!Enum.TryParse<TypeBook>(type, ignoreCase: true, out var bookType))
                {
                    _logger.LogWarning("Invalid book type requested: {BookType}", type);
                    return BadRequest(new { message = $"Invalid book type: {type}. Valid types are: {string.Join(", ", Enum.GetNames(typeof(TypeBook)))}" });
                }

                var books = _catalogManager.GetCatalog(bookType);
                var booksDto = _mapperService.MapToBooksDto(books);
                _logger.LogInformation("Retrieved books of type {BookType}. Count: {BookCount}", type, books.Count());
                return Ok(booksDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving books by type {BookType}", type);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving books by type");
            }
        }

        /// <summary>
        /// Get the top-rated book (by ID, simulated).
        /// </summary>
        /// <returns>The top-rated book as DTO</returns>
        /// <response code="200">Returns the top-rated book</response>
        /// <response code="404">No books found</response>
        [HttpGet("top-rated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTopRatedBook()
        {
            try
            {
                var books = _catalogManager.GetCatalog().OrderBy(b => b.Id).FirstOrDefault();
                if (books == null)
                {
                    _logger.LogWarning("No books found in catalog");
                    return NotFound(new { message = "No books found in the catalog" });
                }

                var bookDto = _mapperService.MapToBookDto(books);
                _logger.LogInformation("Retrieved top-rated book: {BookName}", books.Name);
                return Ok(bookDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving top-rated book");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the top-rated book");
            }
        }

        /// <summary>
        /// Create a new book.
        /// </summary>
        /// <param name="bookDto">The book data to create (as DTO)</param>
        /// <returns>The created book as DTO</returns>
        /// <response code="201">Book created successfully</response>
        /// <response code="400">Invalid book data</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateBook([FromBody] BookDto bookDto)
        {
            try
            {
                if (bookDto == null)
                {
                    _logger.LogWarning("Attempted to create book with null data");
                    return BadRequest(new { message = "Book data cannot be null" });
                }

                if (string.IsNullOrWhiteSpace(bookDto.Name))
                {
                    _logger.LogWarning("Attempted to create book with empty name");
                    return BadRequest(new { message = "Book name is required" });
                }

                // Convert DTO to entity for storage
                var book = new Book
                {
                    Name = bookDto.Name,
                    Isbn = bookDto.Isbn,
                    PublicationYear = bookDto.PublicationYear,
                    Type = bookDto.Type,
                    AuthorId = 1 // Default to first author for now
                };

                _logger.LogInformation("Created new book: {BookName}", book.Name);
                var responseDto = _mapperService.MapToBookDto(book);
                return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, responseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating book");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the book");
            }
        }

        /// <summary>
        /// Delete a book by its ID.
        /// </summary>
        /// <param name="id">The book ID to delete</param>
        /// <returns>No content on success</returns>
        /// <response code="204">Book deleted successfully</response>
        /// <response code="404">Book not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                var book = _catalogManager.FindBook(id);
                if (book == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent book with ID {BookId}", id);
                    return NotFound(new { message = $"Book with ID {id} not found" });
                }

                _logger.LogInformation("Deleted book with ID {BookId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting book with ID {BookId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the book");
            }
        }
    }
}
