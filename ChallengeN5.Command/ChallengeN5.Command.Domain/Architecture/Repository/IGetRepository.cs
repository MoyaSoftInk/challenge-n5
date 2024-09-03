namespace ChallengeN5.Command.Domain.Architecture.Repository;

using ChallengeN5.Command.Domain.Architecture.SeedWork;

/// <summary>
/// Repository interface for Get method's
/// </summary>
/// <typeparam name="TEntity">Entity context</typeparam>
/// <typeparam name="TKey">Primary key of the Entity</typeparam>
public interface IGetRepository<TEntity, in TKey> where TEntity : class, IEntity<TKey>
    where TKey : struct, IEquatable<TKey>
{
    /// <summary>
    /// Get async by primary key id
    /// </summary>
    /// <param name="key">primary key</param>
    /// <param name="cancellationToken">cancallation token</param>
    /// <returns></returns>
    Task<TEntity?> GetByIdAsync(TKey key, CancellationToken cancellationToken);

    /// <summary>
    /// Get by key id
    /// </summary>
    /// <param name="key">primary key</param>
    /// <returns></returns>
    TEntity? GetById(TKey key);

    /// <summary>
    /// Get all async method
    /// </summary>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns>IEnumerable of TEntity</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Get all method
    /// </summary>
    /// <returns>IEnumerable of TEntity</returns>
    IEnumerable<TEntity> GetAll();
}