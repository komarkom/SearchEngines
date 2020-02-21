using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SearchEngines.Db.Entities;

namespace SearchEngines.Db.Context
{
    public class SearchEnginesDbContext : DbContext
    {
        public SearchEnginesDbContext(DbContextOptions options) : base(options)
        {
        }

        protected SearchEnginesDbContext()
        {
        }

        public DbSet<SearchSystem> SearchSystems { get; set; }
        public DbSet<SearchRequest> SearchRequests { get; set; }
        public DbSet<SearchResponse> SearchResponses { get; set; }
        public DbSet<SearchResult> SearchResults { get; set; }
    }
}
