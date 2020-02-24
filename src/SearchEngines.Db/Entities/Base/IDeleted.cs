namespace SearchEngines.Db.Entities.Base
{
    /// <summary>
    /// Removable entities
    /// </summary>
    public interface IDeleted
    {
        /// <summary>
        /// Deleted attribute
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}