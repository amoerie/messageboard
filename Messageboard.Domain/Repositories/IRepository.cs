using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messageboard.Domain.Contracts;
using Messageboard.Domain.Infrastructure.Filtering;
using Messageboard.Domain.Infrastructure.Including;
using Messageboard.Domain.Infrastructure.Sorting;

namespace Messageboard.Domain.Repositories
{
    public interface IRepository <TModel> where TModel : class, IIdentifiable
    {
        /// <summary>
        ///     Returns the <see cref="IQueryable{TModel}"/> of models ordered with the <paramref name="sorter" />,
        ///     filtered by the <paramref name="filter" /> and with navigational properties eagerly loaded as defined by the <paramref name="includer" />.
        /// </summary>
        /// 
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TModel}" /> that defines how the models should be filtered
        /// </param>
        /// <param name="sorter">
        ///     The <see cref="IEntitySorter{TModel}" /> that defines how the models should be sorted
        /// </param>
        /// <param name="includer">
        ///     The <see cref="IEntityIncluder{TEntity}" /> that defines which navigational properties should be eagerly loaded.
        /// </param>
        /// 
        /// <returns>
        ///     the <see cref="IQueryable{TModel}"/> of models ordered with the <paramref name="sorter" />,
        ///     filtered by the <paramref name="filter" /> and with navigational properties eagerly loaded as defined by the <paramref name="includer" />
        /// </returns>
        IQueryable<TModel> Query(
            IEntityFilter<TModel> filter = default( IEntityFilter<TModel> ),
            IEntitySorter<TModel> sorter = default( IEntitySorter<TModel> ),
            IEntityIncluder<TModel> includer = default( IEntityIncluder<TModel> ));

        /// <summary>
        ///     Returns the number of models that satisfy the filter, or the total number of models if no filter is defined
        /// </summary>
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TModel}" /> that defines how the models should be filtered
        /// </param>
        /// <returns>the number of models that satisfy the filter, or the total number of models if no filter is defined</returns>
        Task<int> CountAsync(IEntityFilter<TModel> filter = default( IEntityFilter<TModel> ));

        /// <summary>
        ///     Returns true if any model in the collection satisfies the <paramref name="filter" /> or false otherwise
        /// </summary>
        /// 
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TModel}" /> that defines how the models should be filtered.
        /// </param>
        /// 
        /// <returns>
        ///     True if any model in the collection satisfies the <paramref name="filter" /> or false otherwise
        /// </returns>
        Task<bool> AnyAsync(IEntityFilter<TModel> filter = default( IEntityFilter<TModel> ));

        /// <summary>
        ///     Returns the single model that satisfies the predicate or null if none were found.
        ///     Throws an exception if more than one model is found.
        /// </summary>
        /// 
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TModel}" /> that defines how the models should be filtered
        /// </param>
        /// <param name="includer">
        ///     The <see cref="IEntityIncluder{TModel}" /> that defines which navigational properties should be eagerly loaded.
        /// </param>
        /// 
        /// <exception cref="InvalidOperationException">
        ///     There was more than one model that satisfied the filter
        /// </exception>
        /// 
        /// <returns>
        ///     The <see cref="TModel" />.
        /// </returns>
        Task<TModel> SingleOrDefaultAsync(IEntityFilter<TModel> filter = default( IEntityFilter<TModel> ),
            IEntityIncluder<TModel> includer = default( IEntityIncluder<TModel> ));

        /// <summary>
        ///     Returns the single model that satisfies the predicate.
        ///     Throws an exception if none or more than one model is found.
        /// </summary>
        /// 
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TModel}" /> that defines how the models should be filtered
        /// </param>
        /// <param name="includer">
        ///     The <see cref="IEntityIncluder{TModel}" /> that defines which navigational properties should be eagerly loaded.
        /// </param>
        /// 
        /// <exception cref="InvalidOperationException">
        ///     There were no models that satisfied the filter
        ///     - or -
        ///     There was more than one model that satisfied the filter
        /// </exception>
        /// 
        /// <returns>
        ///     The <see cref="TModel" />.
        /// </returns>
        Task<TModel> SingleAsync(IEntityFilter<TModel> filter = default( IEntityFilter<TModel> ),
            IEntityIncluder<TModel> includer = default( IEntityIncluder<TModel> ));

        /// <summary>
        ///     Returns the first model from that satisfies the filter or null otherwise
        /// </summary>
        /// 
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TModel}" /> that defines how the models should be filtered
        /// </param>
        /// <param name="sorter">
        ///     The <see cref="IEntitySorter{TModel}" /> that defines how the models should be sorted
        /// </param>
        /// <param name="includer">
        ///     The <see cref="IEntityIncluder{TModel}" /> that defines which navigational properties should be eagerly loaded.
        /// </param>
        /// 
        /// <returns>
        ///     The first model that satisfies the <paramref name="filter"/> or null otherwise
        /// </returns>
        Task<TModel> FirstOrDefaultAsync(
            IEntityFilter<TModel> filter = default( IEntityFilter<TModel> ),
            IEntitySorter<TModel> sorter = default( IEntitySorter<TModel> ),
            IEntityIncluder<TModel> includer = default( IEntityIncluder<TModel> ));

        /// <summary>
        ///     Returns the first model from that satisfies the filter
        /// </summary>
        /// 
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TModel}" /> that defines how the models should be filtered
        /// </param>
        /// <param name="sorter">
        ///     The <see cref="IEntitySorter{TModel}" /> that defines how the models should be sorted
        /// </param>
        /// <param name="includer">
        ///     The <see cref="IEntityIncluder{TModel}" /> that defines which navigational properties should be eagerly loaded.
        /// </param>
        /// 
        /// <exception cref="InvalidOperationException">
        ///     There was more than one model that satisfied the filter
        /// </exception>
        /// 
        /// <returns>
        ///     The first model that satisfies the <paramref name="filter"/> or null otherwise
        /// </returns>
        Task<TModel> FirstAsync(
            IEntityFilter<TModel> filter = default( IEntityFilter<TModel> ),
            IEntitySorter<TModel> sorter = default( IEntitySorter<TModel> ),
            IEntityIncluder<TModel> includer = default( IEntityIncluder<TModel> ));

        /// <summary>
        ///     Returns the models ordered with the <paramref name="sorter" />,
        ///     filtered by the <paramref name="filter" /> and with navigational properties eagerly loaded as defined by the <paramref name="includer" />.
        ///     Finally, these models are processed by the <paramref name="selector"/>
        /// </summary>
        /// <param name="selector">
        ///     The <see cref="Func{TModel,TResult}"/> that transforms each <typeparamref name="TModel"/> from the result set into a <typeparamref name="TResult"/>
        /// </param>
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TModel}" /> that defines how the models should be filtered
        /// </param>
        /// <param name="sorter">
        ///     The <see cref="IEntitySorter{TModel}" /> that defines how the models should be sorted
        /// </param>
        /// <param name="includer">
        ///     The <see cref="IEntityIncluder{TModel}" /> that defines which navigational properties should be eagerly loaded.
        /// </param>
        /// 
        /// <typeparam name="TResult">
        ///     The type of the result
        /// </typeparam>
        /// 
        /// <returns>
        ///     The models ordered with the <paramref name="sorter" />,
        ///     filtered by the <paramref name="filter" /> and with navigational properties eagerly loaded as defined by the <paramref name="includer" />.
        ///     Finally, these models are processed by the <paramref name="selector"/>
        /// </returns>
        IEnumerable<TResult> Select <TResult>(
            Func<TModel, TResult> selector,
            IEntityFilter<TModel> filter = default( IEntityFilter<TModel> ),
            IEntitySorter<TModel> sorter = default( IEntitySorter<TModel> ),
            IEntityIncluder<TModel> includer = default( IEntityIncluder<TModel> ));

        /// <summary>
        ///     Returns the model for the given keyValues.
        ///     Throws an exception if more than one model is found.
        /// </summary>
        /// 
        /// <param name="id">
        ///     The id.
        /// </param>
        /// 
        /// <returns>
        ///     The <see cref="TModel" />.
        /// </returns>
        Task<TModel> FindAsync(int id);

        /// <summary>
        ///     Adds a model to the collection or updates the corresponding model from the database.
        /// </summary>
        /// <param name="model">
        ///     The model to add or update
        /// </param>
        void Save(TModel model);

        /// <summary>
        ///     Deletes a model
        /// </summary>
        /// <param name="model">
        ///     The model to delete
        /// </param>
        void Delete(TModel model);

        /// <summary>
        ///     Deletes several models
        /// </summary>
        /// <param name="models">The models to delete</param>
        void Delete(IEnumerable<TModel> models);
    }
}
