using System.Linq;

namespace Messageboard.Domain.Infrastructure.Including
{
    public interface IEntityIncluder<TEntity> where TEntity: class
    {
        /// <summary>
        ///     Executes the inclusions on the <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">The entities on which to perform the inclusions</param>
        /// <returns>The entities that are now marked with the properties that need to be eagerly loaded</returns>
        IQueryable<TEntity> ExecuteInclusions(IQueryable<TEntity> entities);
    }
}
