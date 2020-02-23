using System.Collections.Generic;

namespace SearchEngines.Web.DTO
{
    public class SearchResponseDto
    {
        public string Data { get; set; }
        public string Error { get; set; }
        public bool HasError { get; set; }
        public string SearchSystem { get; set; }
        public virtual ICollection<SearchResultDto> SearchResults { get; set; }
    }
}