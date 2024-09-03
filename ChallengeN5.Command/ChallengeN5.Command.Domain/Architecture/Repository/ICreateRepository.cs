namespace ChallengeN5.Command.Domain.Architecture.Repository;

using ChallengeN5.Command.Domain.Architecture.SeedWork;

/// <summary>
/// Repository interface for create method
/// </summary>
/// <typeparam name="TEntity">Entity context</typeparam>
/// <typeparam name="TKey">Primary key of the Entity</typeparam>
public interface ICreateRepository<in TEntity, TKey> where TEntity : class, IEntity<TKey>
    where TKey : struct, IEquatable<TKey>
{
    /// <summary>
    /// Create method Async
    /// </summary>
    /// <param name="entity">Entity Context</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns></returns>
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Create method
    /// </summary>
    /// <param name="entity">Entity context</param>
    void Create(TEntity entity);
}