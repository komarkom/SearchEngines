using System.Collections.Generic;

namespace SearchEngines.Web.Models
{
    public class SearchResponseModel
    {
        public SearchResponseModel()
        {
            SearchResults = new HashSet<SearchResultModel>();
        }

        public string Data { get; set; }

        public bool HasError { get; set; }

        public string Error { get; set; }

        public virtual ICollection<SearchResultModel> SearchResults { get; set; }
    }
}