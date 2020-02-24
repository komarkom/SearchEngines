using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SearchEngines.Db.Entities;
using SearchEngines.Db.Entities.Base;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SearchSystem>().HasData(SearchSystem.DefaultRecord);
        }

        public override int SaveChanges()
        {
            EntityModifyChanges();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            EntityModifyChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Detect changes, override deleted state, set CreatedOn date
        /// </summary>
        private void EntityModifyChanges()
        {
            ChangeTracker.DetectChanges();

            var markedAsDeleted = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);

            foreach (var item in markedAsDeleted)
            {
                if (item.Entity is IDeleted entity)
                {
                    item.State = EntityState.Modified;
                    entity.IsDeleted = true;
                }
            }

            var createdEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);

            foreach (var entityEntry in createdEntities)
            {
                if (entityEntry.Entity is ICreated createdEntity)
                {
                    createdEntity.CreatedOn = DateTime.Now;
                }
            }
        }
    }
}
