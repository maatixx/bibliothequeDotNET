using BusinessObjects.DataTransferObject;
using BusinessObjects.Entity;

namespace Services.Services
{
    /// <summary>
    /// Implementation of mapper service for converting entities to DTOs.
    /// </summary>
    public class MapperService : IMapperService
    {
        /// <summary>
        /// Converts a Book entity to a BookDto.
        /// Hides sensitive internal properties like AuthorId and Rate.
        /// </summary>
        public BookDto MapToBookDto(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            return new BookDto
            {
                Id = book.Id,
                Name = book.Name,
                Isbn = book.Isbn,
                PublicationYear = book.PublicationYear,
                Type = book.Type,
                Author = book.Author != null ? MapToAuthorDto(book.Author) : null
            };
        }

        /// <summary>
        /// Converts multiple Book entities to BookDtos.
        /// </summary>
        public IEnumerable<BookDto> MapToBooksDto(IEnumerable<Book> books)
        {
            if (books == null)
                throw new ArgumentNullException(nameof(books));

            return books.Select(MapToBookDto);
        }

        /// <summary>
        /// Converts an Author entity to an AuthorDto.
        /// </summary>
        public AuthorDto MapToAuthorDto(Author author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            return new AuthorDto
            {
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }
    }
}
