namespace ChallengeN5.Command.Domain.Architecture.Repository;

using ChallengeN5.Command.Domain.Architecture.SeedWork;
using System.Linq.Expressions;

/// <summary>
/// Repository interface for filtered method
/// </summary>
/// <typeparam name="TEntity">Entity context</typeparam>
/// <typeparam name="TKey">Primary key of the Entity</typeparam>
public interface IFilteredRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    where TKey : struct, IEquatable<TKey>
{
    /// <summary>
    /// Get TEntity by filter an order
    /// </summary>
    /// <param name="filter">Expression filter</param>
    /// <param name="orderByExpression">Expresion order</param>
    /// <param name="ascending">Order ascending</param>
    /// <returns>TEntinty filter</returns>
    IEnumerable<TEntity> GetFiltered(
        Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, object>>? orderByExpression = null,
        bool ascending = true);

    /// <summary>
    /// Get TEntity by filter an order async
    /// </summary>
    /// <param name="filter">Expression filter</param>
    /// <param name="orderByExpression">Expresion order</param>
    /// <param name="ascending">Order ascending</param>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns>TEntinty filter</returns>
    Task<IEnumerable<TEntity>> GetFilteredAsync(
        Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, object>>? orderByExpression = null,
        bool ascending = true, CancellationToken cancellationToken = default);
}