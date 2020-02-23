using System;
using System.Collections;
using System.Collections.Generic;
using SearchEngines.Db.Entities.Base;

namespace SearchEngines.Db.Entities
{
    /// <summary>
    /// Search request
    /// </summary>
    ///<inheritdoc cref="ICreated"/>
    ///<inheritdoc cref="IDeleted"/>
    ///<inheritdoc cref="OriginalKeyedRecord"/>
    public class SearchRequest : OriginalKeyedRecord, ICreated, IDeleted
    {
        /// <summary>
        /// Search user text
        /// </summary>
        public string SearchText { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Result for this search request
        /// </summary>
        public virtual SearchResponse SearchResponse { get; set; }
    }
}