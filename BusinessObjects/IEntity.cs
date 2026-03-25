namespace BusinessObjects
{
    /// <summary>
    /// Base interface for all entities in the system.
    /// Ensures all entities have an Id property.
    /// </summary>
    public interface IEntity
    {
        int Id { get; set; }
    }
}
