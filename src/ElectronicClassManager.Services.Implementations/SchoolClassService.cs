using System.Linq.Expressions;
using ElectronicClassManager.Entities;
using ElectronicClassManager.Entities.Storage;
using ElectronicClassManager.Services.Dto.SchoolClass;
using Microsoft.EntityFrameworkCore;

namespace ElectronicClassManager.Services.Implementations;

public class SchoolClassService : ISchoolClassService
{
    private readonly EntitiesDbContext _dbContext;

    public SchoolClassService(EntitiesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<SchoolClassOutDto?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<SchoolClass>()
            .Where(x => x.Id == id)
            .Select(ProjectFromEntityExpr())
            .SingleOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<SchoolClassOutDto?> GetByPseudoNameAsync(string pseudoName)
    {
        return await _dbContext.Set<SchoolClass>()
            .Where(x => x.PseudoName == pseudoName)
            .Select(ProjectFromEntityExpr())
            .SingleOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<SchoolClassOutDto> CreateAsync(SchoolClassCreateDto dto)
    {
        var entity = ProjectToEntity()(dto);
        _dbContext.Add(entity);

        await _dbContext.SaveChangesAsync();

        return ProjectFromEntity(entity);
    }

    /// <inheritdoc />
    public async Task<SchoolClassOutDto?> UpdateByIdAsync(Guid id, SchoolClassUpdateDto dto)
    {
        var entity = await _dbContext.Set<SchoolClass>()
            .SingleOrDefaultAsync(x => x.Id == id);

        if (entity is null)
            return null;

        entity.Description = dto.Description;

        await _dbContext.SaveChangesAsync();

        return ProjectFromEntity(entity);
    }

    /// <inheritdoc />
    public async Task<SchoolClassOutDto?> UpdateByPseudoNameAsync(string pseudoName, SchoolClassUpdateDto dto)
    {
        var entity = await _dbContext.Set<SchoolClass>()
            .SingleOrDefaultAsync(x => x.PseudoName == pseudoName);

        if (entity is null)
            return null;

        entity.Description = dto.Description;

        await _dbContext.SaveChangesAsync();

        return ProjectFromEntity(entity);
    }

    /// <inheritdoc />
    public async Task<SchoolClassOutDto[]> FindAsync(SchoolClassFindDto dto)
    {
        var query = _dbContext.Set<SchoolClass>().AsQueryable();

        if (!string.IsNullOrWhiteSpace(dto.Letter))
            query = query.Where(x => x.Letter == dto.Letter);

        if (!string.IsNullOrWhiteSpace(dto.Description))
            query = query.Where(x => x.Description!.Contains(dto.Description!));

        if (dto.Number is not null)
            query = query.Where(x => x.Number == dto.Number);

        if (dto.StartYear is not null)
            query = query.Where(x => x.StartYear == dto.StartYear);

        return await query.Select(ProjectFromEntityExpr())
            .ToArrayAsync();
    }

    /// <inheritdoc />
    public async Task DeleteByPseudoNameAsync(string pseudoName)
    {
        var entity = await _dbContext.Set<SchoolClass>().SingleOrDefaultAsync(x => x.PseudoName == pseudoName);

        if (entity is not null)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }

    /// <inheritdoc />
    public async Task<bool> CanUsePseudoNameAsync(string pseudoName)
    {
        return await _dbContext.Set<SchoolClass>()
            .AllAsync(x => x.PseudoName != pseudoName);
    }

    private static Expression<Func<SchoolClass, SchoolClassOutDto>> ProjectFromEntityExpr()
    {
        return x => new SchoolClassOutDto
        {
            Id = x.Id,
            PseudoName = x.PseudoName,
            Description = x.Description,
            Number = x.Number,
            Letter = x.Letter,
            StartYear = x.StartYear
        };
    }

    private static readonly Func<SchoolClass, SchoolClassOutDto> ProjectFromEntity = ProjectFromEntityExpr().Compile();

    private static Func<SchoolClassCreateDto, SchoolClass> ProjectToEntity()
    {
        return x => new SchoolClass
        {
            PseudoName = x.PseudoName,
            Description = x.Description,
            Number = x.Number,
            Letter = x.Letter,
            StartYear = x.StartYear
        };
    }
}
