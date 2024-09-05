using ChallengeN5.Command.Domain.Architecture.Extension;
using ChallengeN5.Command.Domain.Architecture.Repository;
using ChallengeN5.Command.Domain.Architecture.SeedWork;
using ChallengeN5.Command.Persistance.Application.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ChallengeN5.Command.Persistance.Architecture;

/// <summary>
/// Base repository with a basic implementation of all method associated in the CRUD with async methods
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct, IEquatable<TKey>
{
    private readonly N5Context _context;

    public Repository(N5Context context)
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

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        _context.SaveChanges();
    }

    public async Task<TEntity?> GetByIdAsync(TKey key, CancellationToken cancellationToken)
    {
        object[] keys = { key };
        return await _context.Set<TEntity>().FindAsync(keys, cancellationToken);
    }

    public TEntity? GetById(TKey key)
    {
        return _context.Set<TEntity>().Find(key);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        _context.SaveChanges();
    }

    public async Task<IEnumerable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>? orderByExpression = null, bool ascending = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Query().Where(filter);

        if (orderByExpression == null)
        {
            return await query.ToListAsync(cancellationToken);
        }

        return ascending
            ? await query.OrderBy(orderByExpression).ToListAsync(cancellationToken)
            : await query.OrderByDescending(orderByExpression).ToListAsync(cancellationToken);
    }

    public async Task<PagedElements<TEntity>> GetPagedAsync(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>? orderByExpression = null,
        bool ascending = true, CancellationToken cancellationToken = default)
    {
        //TODO MUST BE CREATE A HANDLE EXCEPTION FOR VALIDATE TO ARGUMENTS
        if (orderByExpression is null)
        {
            throw new ArgumentNullException(nameof(orderByExpression));
        }

        IQueryable<TEntity> query = Query().Where(filter);

        int total = query.Count();

        return ascending
            ? new PagedElements<TEntity>(
                await query.OrderBy(orderByExpression)
                    .Page(pageIndex, pageCount)
                    .ToListAsync(cancellationToken),
                total)
            : new PagedElements<TEntity>(
                await query.OrderByDescending(orderByExpression)
                    .Page(pageIndex, pageCount)
                    .ToListAsync(cancellationToken),
                total);
    }

    public IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>? orderByExpression = null, bool ascending = true)
    {
        //TODO MUST BE CREATE A HANDLE EXCEPTION FOR VALIDATE TO ARGUMENTS
        IQueryable<TEntity> query = Query().Where(filter);

        if (orderByExpression == null)
        {
            return query.ToList();
        }

        return ascending
            ? query.OrderBy(orderByExpression).ToList()
            : query.OrderByDescending(orderByExpression).ToList();
    }

    public PagedElements<TEntity> GetPaged(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>? orderByExpression = null,
        bool ascending = true)
    {
        if (orderByExpression is null)
        {
            throw new ArgumentNullException(nameof(orderByExpression));
        }

        IQueryable<TEntity> query = Query().Where(filter);

        int total = query.Count();

        return ascending
            ? new PagedElements<TEntity>(
                query.OrderBy(orderByExpression)
                    .Page(pageIndex, pageCount)
                    .ToList(),
                total)
            : new PagedElements<TEntity>(
                query.OrderByDescending(orderByExpression)
                    .Page(pageIndex, pageCount)
                    .ToList(),
                total);
    }

    private IQueryable<TEntity> Query()
    {
        return _context.Set<TEntity>();
    }
}