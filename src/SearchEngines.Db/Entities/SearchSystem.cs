using System;
using System.ComponentModel.DataAnnotations;
using SearchEngines.Db.Entities.Base;

namespace SearchEngines.Db.Entities
{
    ///<summary>
    /// Search system info
    /// </summary>
    ///<inheritdoc cref="ICreated"/>
    ///<inheritdoc cref="IDeleted"/>
    ///<inheritdoc cref="OriginalKeyedRecord"/>
    public class SearchSystem : OriginalKeyedRecord, ICreated, IDeleted
    {
        /// <summary>
        /// Name of search system
        /// </summary>
        [MaxLength(150)]
        public string SystemName { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}