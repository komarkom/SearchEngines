using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SearchEngines.Db.Entities;
using SearchEngines.Web.Util.Model;

namespace SearchEngines.Web.Util
{
    /// <summary>
    /// Implementation of ISearchManager
    /// </summary>
    ///<inheritdoc cref="ISearchManager"/>
    public class SearchManager: ISearchManager
    {
        /// <summary>
        /// Service with collection of search engines
        /// </summary>
        private readonly SearchEngineServices _searchEngineServices;

        public SearchManager(SearchEngineServices searchEngineServices)
        {
            _searchEngineServices = searchEngineServices;
        }

        public Result<SearchResponse> Search(string searchText)
        {
            var result = new Result<SearchResponse>();

            var cts = new CancellationTokenSource();
            var tasks = new List<Task<SearchResponse>>();
            foreach (var searchEngine in _searchEngineServices.SearchEngines)
            {
                tasks.Add(new Task<SearchResponse>(() => searchEngine.Search(searchText, cts).GetAwaiter().GetResult()));
            }

            foreach (var task in tasks)
            {
                task.Start();
            }

            try
            {
                Task.WaitAll(tasks.ToArray(), cts.Token);
            }
            catch (OperationCanceledException e)
            {
                if (!e.CancellationToken.IsCancellationRequested)
                {
                    result.IsOk = false;
                    result.ErrorMessage = "Cancellation token has no requested";

                    return result;
                }
            }
            catch (Exception e)
            {
                result.IsOk = false;
                result.ErrorMessage = e.ToString();
                return result;
            }

            var completedTask = tasks.FirstOrDefault(x => x.IsCompletedSuccessfully);

            if (completedTask == null)
            {
                result.IsOk = false;
                result.ErrorMessage = "Has no any completed tasks";

                return result;
            }

            var res = completedTask.Result;

            if (res.HasError)
            {
                result.IsOk = false;
                result.ErrorMessage = res.Error;
                result.Value = res;
                return result;
            }

            result.IsOk = true;
            result.Value = res;
            return result;
        }
    }
}