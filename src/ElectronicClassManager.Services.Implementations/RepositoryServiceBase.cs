using System.Linq.Expressions;
using ElectronicClassManager.Entities;
using ElectronicClassManager.Entities.Storage;
using Microsoft.EntityFrameworkCore;

namespace ElectronicClassManager.Services.Implementations;

public class RepositoryServiceBase
{
    protected EntitiesDbContext DbContext { get; }

    protected RepositoryServiceBase(EntitiesDbContext dbContext)
    {
        DbContext = dbContext;
    }

    protected async Task<TEntity?> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return await ApplyFilter(predicate).SingleOrDefaultAsync();
    }

    protected async Task<TEntity[]> FindManyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return await ApplyFilter(predicate).ToArrayAsync();
    }

    protected async Task<TEntity[]> RemoveByPredicateAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
        where TEntity : class
    {
        var entities = await FindManyAsync(predicate);

        DbContext.RemoveRange(entities.AsEnumerable());

        await DbContext.SaveChangesAsync();

        return entities;
    }

    protected async Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class
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

public class RepositoryServiceBase<TEntity> : RepositoryServiceBase where TEntity : IdEntity
{
    /// <inheritdoc />
    protected RepositoryServiceBase(EntitiesDbContext dbContext) : base(dbContext)
    {

    }

    protected async Task<TEntity?> FindByIdAsync(Guid id) => await FindOneAsync(GetByIdPredicate(id));

    protected async Task<TEntity?> RemoveByIdAsync(Guid id) =>
        (await RemoveByPredicateAsync(GetByIdPredicate(id))).SingleOrDefault();

    protected static Expression<Func<TEntity, bool>> GetByIdPredicate(Guid id)
    {
        return x => x.Id == id;
    }
}

public abstract class RepositoryServiceBase<TEntity, TOutDto> : RepositoryServiceBase<TEntity>
    where TEntity : IdEntity
    where TOutDto : class
{
    private Func<TEntity, TOutDto>? _entityToOutDtoMappingFunc;

    /// <inheritdoc />
    protected RepositoryServiceBase(EntitiesDbContext dbContext) : base(dbContext)
    {
        
    }

    public async Task<TOutDto?> GetByIdAsync(Guid id)
    {
        return await ApplyFilter(GetByIdPredicate(id))
            .Select(EntityToOutDtoMappingExpression)
            .SingleOrDefaultAsync();
    }

    public async Task<TOutDto?> DeleteByIdAsync(Guid id)
    {
        var entity = await RemoveByIdAsync(id);

        if (entity is null)
            return null;

        return EntityToOutDtoMappingFunc(entity);
    }

    protected abstract Expression<Func<TEntity, TOutDto>> EntityToOutDtoMappingExpression { get; }

    protected Func<TEntity, TOutDto> EntityToOutDtoMappingFunc =>
        _entityToOutDtoMappingFunc ??= EntityToOutDtoMappingExpression.Compile();
}

public abstract class
    RepositoryServiceBase<TEntity, TOutDto, TCreateDto, TUpdateDto> : RepositoryServiceBase<TEntity, TOutDto>
    where TEntity : IdEntity
    where TOutDto : class
    where TCreateDto : class
    where TUpdateDto : class
{
    /// <inheritdoc />
    protected RepositoryServiceBase(EntitiesDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<TOutDto> CreateAsync(TCreateDto dto)
    {
        var entity = MapCreationDtoToEntity(dto);

        DbContext.Add(entity);

        await DbContext.SaveChangesAsync();

        return EntityToOutDtoMappingFunc(entity);
    }

    public async Task<TOutDto?> UpdateByIdAsync(Guid id, TUpdateDto dto)
    {
        var entity = await FindByIdAsync(id);

        if (entity is null)
            return null;

        UpdateEntityFromDto(entity, dto);

        await DbContext.SaveChangesAsync();

        return EntityToOutDtoMappingFunc(entity);
    }

    protected abstract TEntity MapCreationDtoToEntity(TCreateDto dto);

    protected abstract void UpdateEntityFromDto(TEntity entity, TUpdateDto dto);
}

public abstract class
    RepositoryServiceBase<TEntity, TOutDto, TCreateDto, TUpdateDto, TFindDto> : RepositoryServiceBase<TEntity, TOutDto,
        TCreateDto, TUpdateDto>
    where TEntity : IdEntity
    where TOutDto : class
    where TCreateDto : class
    where TUpdateDto : class
    where TFindDto : class
{
    /// <inheritdoc />
    protected RepositoryServiceBase(EntitiesDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<TOutDto[]> FindAsync(TFindDto dto)
    {
        var query = ApplyFindFilter(DbContext.Set<TEntity>(), dto);

        return await query
            .Select(EntityToOutDtoMappingExpression)
            .ToArrayAsync();
    }

    protected abstract IQueryable<TEntity> ApplyFindFilter(IQueryable<TEntity> query, TFindDto dto);
}