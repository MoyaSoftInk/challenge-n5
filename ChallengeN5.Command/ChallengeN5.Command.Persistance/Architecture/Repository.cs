using ChallengeN5.Command.Domain.Architecture.Extension;
using ChallengeN5.Command.Domain.Architecture.Repository;
using ChallengeN5.Command.Domain.Architecture.SeedWork;
using ChallengeN5.Command.Infrastructure.Application.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace ChallengeN5.Command.Infrastructure.Architecture;

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
    private readonly ILogger _logger;

    public Repository(N5Context context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Creating {entity.GetType().Name} - {entity}");
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        _logger.LogInformation($"Created {entity.GetType().Name} - {entity}");
    }

    public void Create(TEntity entity)
    {
        _logger.LogInformation($"Creating {entity.GetType().Name} - {entity}");
        _context.Set<TEntity>().Add(entity);
        _logger.LogInformation($"Created {entity.GetType().Name} - {entity}");
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating {entity.GetType().Name} - {entity}");
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation($"Updated {entity.GetType().Name} - {entity}");
    }

    public void Update(TEntity entity)
    {
        _logger.LogInformation($"Updating {entity.GetType().Name} - {entity}");
        _context.Set<TEntity>().Update(entity);
        _logger.LogInformation($"Updated {entity.GetType().Name} - {entity}");
    }

    public async Task<TEntity?> GetByIdAsync(TKey key, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Getting {typeof(TEntity).Name} by id {key}");
        object[] keys = { key };
        return await _context.Set<TEntity>().FindAsync(keys, cancellationToken);
    }

    public TEntity? GetById(TKey key)
    {
        _logger.LogInformation($"Getting {typeof(TEntity).Name} by id {key}");
        return _context.Set<TEntity>().Find(key);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Getting all {typeof(TEntity).Name}");
        return await _context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public IEnumerable<TEntity> GetAll()
    {
        _logger.LogInformation($"Getting all {typeof(TEntity).Name}");
        return _context.Set<TEntity>().ToList();
    }

    public void Delete(TEntity entity)
    {
        _logger.LogInformation($"Deleting {entity.GetType().Name} - {entity}");
        _context.Set<TEntity>().Remove(entity);
        _logger.LogInformation($"Deleted {entity.GetType().Name} - {entity}");
    }

    public async Task<IEnumerable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>? orderByExpression = null, bool ascending = true,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Getting filtered {typeof(TEntity).Name}");
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
        _logger.LogInformation($"Getting paged {typeof(TEntity).Name}");
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
        _logger.LogInformation($"Getting filtered {typeof(TEntity).Name}");
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
        _logger.LogInformation($"Getting paged {typeof(TEntity).Name}");
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