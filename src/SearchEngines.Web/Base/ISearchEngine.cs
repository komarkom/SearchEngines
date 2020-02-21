using System.Threading.Tasks;
using SearchEngines.Db.Entities;

namespace SearchEngines.Web.Base
{
    public interface ISearchEngine
    {
        public SearchResponse Search(string searchText);
    }
}