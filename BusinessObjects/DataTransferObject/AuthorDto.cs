namespace BusinessObjects.DataTransferObject
{
    /// <summary>
    /// DTO for Author information.
    /// Exposes only necessary data to API clients without exposing internal IDs or relationships.
    /// </summary>
    public class AuthorDto
    {
        /// <summary>
        /// The author's first name.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The author's last name.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Computed property: Full name of the author.
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
    }
}
