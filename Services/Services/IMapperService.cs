using BusinessObjects.DataTransferObject;
using BusinessObjects.Entity;

namespace Services.Services
{
    /// <summary>
    /// Mapper service for converting entities to DTOs and vice versa.
    /// Centralizes all mapping logic in one place for maintainability.
    /// </summary>
    public interface IMapperService
    {
        /// <summary>
        /// Converts a Book entity to a BookDto.
        /// </summary>
        BookDto MapToBookDto(Book book);

        /// <summary>
        /// Converts multiple Book entities to BookDtos.
        /// </summary>
        IEnumerable<BookDto> MapToBooksDto(IEnumerable<Book> books);

        /// <summary>
        /// Converts an Author entity to an AuthorDto.
        /// </summary>
        AuthorDto MapToAuthorDto(Author author);
    }
}
