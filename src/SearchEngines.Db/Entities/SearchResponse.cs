using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SearchEngines.Db.Entities.Base;

namespace SearchEngines.Db.Entities
{
    ///<summary>
    /// Response of search request
    /// </summary>
    ///<inheritdoc cref="ICreated"/>
    ///<inheritdoc cref="IDeleted"/>
    ///<inheritdoc cref="OriginalKeyedRecord"/>
    public class SearchResponse : OriginalKeyedRecord, ICreated, IDeleted
    {
        public SearchResponse()
        {
            SearchResults = new HashSet<SearchResult>();
        }

        /// <summary>
        /// Information result
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Response return with error message
        /// </summary>
        public bool HasError { get; set; }

        /// <summary>
        /// Link to search request
        /// </summary>
        public long? SearchRequestId { get; set; }

        /// <summary>
        /// Search request
        /// </summary>
        [ForeignKey(nameof(SearchRequestId))]
        public virtual SearchRequest SearchRequest { get; set; }

        /// <summary>
        /// Link to search system
        /// </summary>
        public long? SearchSystemId { get; set; }

        /// <summary>
        /// Search system
        /// </summary>
        [ForeignKey(nameof(SearchSystemId))]
        public virtual SearchSystem SearchSystem { get; set; }

        public virtual ICollection<SearchResult> SearchResults { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}