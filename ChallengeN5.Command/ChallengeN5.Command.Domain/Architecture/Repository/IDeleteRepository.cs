namespace ChallengeN5.Command.Domain.Architecture.Repository;

using ChallengeN5.Command.Domain.Architecture.SeedWork;


/// <summary>
/// Repository interface for delete method
/// </summary>
/// <typeparam name="TEntity">Entity context</typeparam>
/// <typeparam name="TKey">Primary key of the Entity</typeparam>
public interface IDeleteRepository<in TEntity, TKey> where TEntity : class, IEntity<TKey>
    where TKey : struct, IEquatable<TKey>
{
    /// <summary>
    /// Delete method
    /// </summary>
    /// <param name="entity">Entity context</param>
    void Delete(TEntity entity);
}