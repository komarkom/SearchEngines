﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SearchEngines.Db.Entities;
using SearchEngines.Web.Models;

namespace SearchEngines.Web.Util
{
    public class SearchManager: ISearchManager
    {
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
                tasks.Add(new Task<SearchResponse>(() => searchEngine.Search(searchText, cts)));
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

                var completedTask = tasks.FirstOrDefault(x => x.IsCompletedSuccessfully);

                if (completedTask == null)
                {
                    result.IsOk = false;
                    result.ErrorMessage = "Has no any completed tasks";
                 
                    return result;
                }

                var res = completedTask.Result;

                result.IsOk = true;
                result.Value = res;
                return result;

            }
            catch (Exception e)
            {
                result.IsOk = false;
                result.ErrorMessage = e.ToString();
                return result;
            }

            result.IsOk = false;
            return result;
        }
    }
}