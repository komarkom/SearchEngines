using System;

namespace SearchEngines.Db.Entities.Base
{
    /// <summary>
    /// Logging created entities
    /// </summary>
    public interface ICreated
    {
        /// <summary>
        /// Date and time when entity was created 
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}