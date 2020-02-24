using System.ComponentModel.DataAnnotations;
using SearchEngines.Db.Entities.Base;

namespace SearchEngines.Db.Entities
{
    ///<summary>
    /// Search system info
    /// </summary>
    ///<inheritdoc cref="IDeleted"/>
    ///<inheritdoc cref="OriginalKeyedRecord"/>
    public class SearchSystem : OriginalKeyedRecord, IDeleted
    {
        /// <summary>
        /// Name of search system
        /// </summary>
        [MaxLength(150)]
        public string SystemName { get; set; }

        public bool IsDeleted { get; set; }

        public static readonly SearchSystem[] DefaultRecord =
        {
            new SearchSystem() {Id = 1, IsDeleted = false, SystemName = "yandex"},
            new SearchSystem() {Id = 2, IsDeleted = false, SystemName = "google"},
            new SearchSystem() {Id = 3, IsDeleted = false, SystemName = "bing"}
        };
    }
}