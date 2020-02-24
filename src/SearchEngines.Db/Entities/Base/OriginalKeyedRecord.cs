using System.ComponentModel.DataAnnotations;

namespace SearchEngines.Db.Entities.Base
{
    /// <summary>
    /// Base entitie keyed class
    /// </summary>
    public class OriginalKeyedRecord
    {
        /// <summary>
        /// Unique identifier 
        /// </summary>
        [Key]
        public long Id { get; set; }
    }
}
