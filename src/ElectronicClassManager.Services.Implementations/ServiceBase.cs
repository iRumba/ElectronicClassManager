using System.Linq.Expressions;
using ElectronicClassManager.Entities;
using ElectronicClassManager.Entities.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ElectronicClassManager.Services.Implementations;

public class ServiceBase
{
    public EntitiesDbContext DbContext { get; }

    protected ServiceBase(EntitiesDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<TEntity?> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return await ApplyFilter(predicate).SingleOrDefaultAsync();
    }

    public async Task<TEntity[]> FindManyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return await ApplyFilter(predicate).ToArrayAsync();
    }

    public async Task<TEntity[]> DeleteByPredicateAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
        where TEntity : class
    {
        var entities = await FindManyAsync(predicate);

        DbContext.RemoveRange(entities.AsEnumerable());

        await DbContext.SaveChangesAsync();

        return entities;
    }

    public async Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class
    {
        DbContext.Add(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    protected IQueryable<TEntity> ApplyFilter<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return DbContext.Set<TEntity>().Where(predicate);
    }
}

public class ServiceBase<TEntity> : ServiceBase
    where TEntity : IdEntity
{
    /// <inheritdoc />
    protected ServiceBase(EntitiesDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<TEntity?> GetByIdAsync(Guid id) => await FindOneAsync(GetByIdPredicate(id));

    public async Task<TEntity?> DeleteByIdAsync(Guid id) =>
        (await DeleteByPredicateAsync(GetByIdPredicate(id))).SingleOrDefault();

    public async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        DbContext.Update(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        DbContext.Add(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity[]> FindAsync(params Expression<Func<TEntity, bool>>[] filters) => await FindAsync(filters.AsEnumerable());

    public async Task<TEntity[]> FindAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters)
    {
        var query = DbContext.Set<TEntity>().AsQueryable();

        foreach (var filter in filters)
        {
            query = query.Where(filter);
        }

        return await query.ToArrayAsync();
    }

    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate) => await base.FindOneAsync(predicate);

    protected static Expression<Func<TEntity, bool>> GetByIdPredicate(Guid id)
    {
        return x => x.Id == id;
    }
}