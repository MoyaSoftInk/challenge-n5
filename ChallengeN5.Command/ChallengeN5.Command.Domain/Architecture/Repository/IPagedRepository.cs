namespace ChallengeN5.Command.Domain.Architecture.Repository;

using ChallengeN5.Command.Domain.Architecture.SeedWork;
using System.Linq.Expressions;


/// <summary>
/// Repository interface for get paged method
/// </summary>
/// <typeparam name="TEntity">Entity context</typeparam>
/// <typeparam name="TKey">Primary key of the Entity</typeparam>
public interface IPagedRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    where TKey : struct, IEquatable<TKey>
{
    /// <summary>
    /// Get Entity paged
    /// </summary>
    /// <param name="pageIndex">The current page</param>
    /// <param name="pageCount">Number of elements per page</param>
    /// <param name="filter">Filter</param>
    /// <param name="orderByExpression">Expression</param>
    /// <param name="ascending">Order</param>
    /// <returns></returns>
    PagedElements<TEntity> GetPaged(
        int pageIndex,
        int pageCount,
        Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, object>>? orderByExpression = null,
        bool ascending = true);

    /// <summary>
    /// Get Entity paged async
    /// </summary>
    /// <param name="pageIndex">The current page</param>
    /// <param name="pageCount">Number of elements per page</param>
    /// <param name="filter">Filter</param>
    /// <param name="orderByExpression">Expression</param>
    /// <param name="ascending">Order</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<PagedElements<TEntity>> GetPagedAsync(
        int pageIndex,
        int pageCount,
        Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, object>>? orderByExpression = null,
        bool ascending = true, CancellationToken cancellationToken = default);
}