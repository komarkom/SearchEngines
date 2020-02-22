using SearchEngines.Db.Entities;

namespace SearchEngines.Web.Util
{
    public interface ISearchManager
    {
        public Result<SearchResponse> Search(string searchText);
    }
}