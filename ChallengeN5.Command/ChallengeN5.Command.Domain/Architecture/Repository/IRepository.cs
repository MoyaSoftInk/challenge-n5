namespace ChallengeN5.Command.Domain.Architecture.Repository;

using ChallengeN5.Command.Domain.Architecture.SeedWork;
using System;

/// <summary>
/// Base repository with a basic implementation of all method associated in the CRUD with async methods
/// </summary>
/// <typeparam name="TEntity">Entity in the context</typeparam>
/// <typeparam name="TKey">Primary Key of the entity <see cref="TEntity"/></typeparam>
public interface IRepository<TEntity, TKey>
    :
        ICreateRepository<TEntity, TKey>,
        IUpdateRepository<TEntity, TKey>,
        IGetRepository<TEntity, TKey>,
        IDeleteRepository<TEntity, TKey>,
        IPagedRepository<TEntity, TKey>,
        IFilteredRepository<TEntity, TKey>
    where TEntity : class,
    IEntity<TKey>
    where TKey : struct,
    IEquatable<TKey>
{
}
