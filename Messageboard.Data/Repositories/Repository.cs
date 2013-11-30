using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Messageboard.Domain.Contracts;
using Messageboard.Domain.Infrastructure.Filtering;
using Messageboard.Domain.Infrastructure.Including;
using Messageboard.Domain.Infrastructure.Sorting;
using Messageboard.Domain.Repositories;

namespace Messageboard.Data.Repositories
{
    public class Repository<TModel>: IRepository<TModel> where TModel : class, IIdentifiable, new()
    {
        private readonly DbContext _context;
        private readonly DbSet<TModel> _entitySet;
        private readonly IQueryable<TModel> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entitySet = context.Set<TModel>();
            _entities = _entitySet;
        }

        public IQueryable<TModel> Query(
            IEntityFilter<TModel> filter = null,
            IEntitySorter<TModel> sorter = null,
            IEntityIncluder<TModel> includer = null)
        {
            var entities = _entities;
            if (includer != null)
                entities = includer.ExecuteInclusions(entities);
            if (filter != null)
                entities = filter.Filter(entities);
            if (sorter != null)
                entities = sorter.Sort(entities);
            return entities;
        }

        public Task<int> CountAsync(IEntityFilter<TModel> filter = null)
        {
            return Query(filter).CountAsync();
        }

        public Task<bool> AnyAsync(IEntityFilter<TModel> filter = null)
        {
            return Query(filter).AnyAsync();
        }

        public Task<TModel> SingleOrDefaultAsync(IEntityFilter<TModel> filter = null, IEntityIncluder<TModel> includer = null)
        {
            return Query(filter, includer: includer).SingleOrDefaultAsync();
        }

        public Task<TModel> SingleAsync(IEntityFilter<TModel> filter = null, IEntityIncluder<TModel> includer = null)
        {
            return Query(filter, includer: includer).SingleAsync();
        }

        public Task<TModel> FirstOrDefaultAsync(IEntityFilter<TModel> filter = null, IEntitySorter<TModel> sorter = null, IEntityIncluder<TModel> includer = null)
        {
            return Query(filter, sorter, includer).FirstOrDefaultAsync();
        }

        public Task<TModel> FirstAsync(IEntityFilter<TModel> filter = null, IEntitySorter<TModel> sorter = null, IEntityIncluder<TModel> includer = null)
        {
            return Query(filter, sorter, includer).FirstAsync();
        }

        public IEnumerable<TResult> Select <TResult>(Func<TModel, TResult> selector,
            IEntityFilter<TModel> filter = null,
            IEntitySorter<TModel> sorter = null,
            IEntityIncluder<TModel> includer = null)
        {
            return Query(filter, sorter, includer).Select(selector);
        }

        public Task<TModel> FindAsync(int id)
        {
            return _entitySet.FindAsync(id);
        }

        public void Save(TModel model)
        {
            if (model.Id == 0)
            {
                Add(model);
            }
            else
            {
                Update(model);
            }
        }

        protected virtual void Add(TModel model)
        {
            DbEntityEntry entry = _context.Entry(model);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                _entitySet.Add(model);
            }
        }

        protected virtual void Update(TModel model)
        {
            DbEntityEntry entry = _context.Entry(model);
            if (entry.State == EntityState.Detached)
            {
                _entitySet.Attach(model);
            }
            entry.State = EntityState.Modified;
        }

        public void Delete(TModel model)
        {
            _entitySet.Remove(model);
        }

        public void Delete(IEnumerable<TModel> models)
        {
            _entitySet.RemoveRange(models);
        }
    }
}
