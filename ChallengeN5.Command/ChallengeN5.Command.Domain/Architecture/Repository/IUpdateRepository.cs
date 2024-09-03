namespace ChallengeN5.Command.Domain.Architecture.Repository;

using ChallengeN5.Command.Domain.Architecture.SeedWork;


/// <summary>
/// Repository interface for update method
/// </summary>
/// <typeparam name="TEntity">Entity context</typeparam>
/// <typeparam name="TKey">Primary key of the Entity</typeparam>
public interface IUpdateRepository<in TEntity, TKey> where TEntity : class, IEntity<TKey>
    where TKey : struct, IEquatable<TKey>
{
    /// <summary>
    /// Update TEntity method async
    /// </summary>
    /// <param name="entity">TEntity context</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Update TEntity method
    /// </summary>
    /// <param name="entity">TEntity context</param>
    void Update(TEntity entity);
}