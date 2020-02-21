using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SearchEngines.Db.Entities.Base;

namespace SearchEngines.Db.Entities
{
    ///<summary>
    /// Result of search request
    /// </summary>
    ///<inheritdoc cref="ICreated"/>
    ///<inheritdoc cref="IDeleted"/>
    ///<inheritdoc cref="OriginalKeyedRecord"/>
    public class SearchResult : OriginalKeyedRecord, ICreated, IDeleted
    {
        /// <summary>
        /// Result header text
        /// </summary>
        public string HeaderLinkText { get; set; }

        /// <summary>
        /// Result Url
        /// </summary>
        public string Url { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Link to search response
        /// </summary>
        public long? SearchResponseId { get; set; }

        /// <summary>
        /// Search response
        /// </summary>
        [ForeignKey(nameof(SearchResponseId))]
        public virtual SearchResponse SearchResponse { get; set; }
    }
}