namespace ChallengeN5.Command.Persistance.Architecture;

using ChallengeN5.Command.Domain.Architecture.Repository;
using ChallengeN5.Command.Domain.Architecture.SeedWork;
using ChallengeN5.Command.Persistance.Application.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Base repository with a basic implementation of all method associated in the <see cref="ICreateRepository"/>
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public class CreateRepository<TEntity, TKey> : ICreateRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct, IEquatable<TKey>
{
    private readonly N5Context _context;

    public CreateRepository(N5Context context)
    {
        _context = context;
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Create(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        _context.SaveChanges();
    }
}