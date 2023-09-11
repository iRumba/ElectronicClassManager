using System.Linq.Expressions;
using ElectronicClassManager.Entities;
using ElectronicClassManager.Entities.Storage;
using Microsoft.EntityFrameworkCore;

namespace ElectronicClassManager.Services.Implementations;

public class SchoolClassService : ServiceBase<SchoolClass>, ISchoolClassService
{
    public SchoolClassService(EntitiesDbContext dbContext) : base(dbContext)
    {
    }

    /// <inheritdoc />
    public async Task<SchoolClass?> GetByPseudoNameAsync(string pseudoName)
    {
        return await FindOneAsync(x => x.PseudoName == pseudoName);
    }

    /// <inheritdoc />
    public async Task<SchoolClass?> DeleteByPseudoNameAsync(string pseudoName)
    {
        var entity = await GetByPseudoNameAsync(pseudoName);

        if (entity is null)
        {
            return null;
        }

        DbContext.Remove(entity);
        await DbContext.SaveChangesAsync();

        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> CanUsePseudoNameAsync(string pseudoName)
    {
        return await DbContext.Set<SchoolClass>()
            .AllAsync(x => x.PseudoName != pseudoName);
    }

    //private static Expression<Func<SchoolClass, SchoolClassOutDto>> ProjectFromEntityExpr()
    //{
    //    return x => new SchoolClassOutDto
    //    {
    //        Id = x.Id,
    //        PseudoName = x.PseudoName,
    //        Description = x.Description,
    //        Number = x.Number,
    //        Letter = x.Letter,
    //        StartYear = x.StartYear
    //    };
    //}

    //private static readonly Func<SchoolClass, SchoolClassOutDto> ProjectFromEntity = ProjectFromEntityExpr().Compile();

    //private static Func<SchoolClassCreateDto, SchoolClass> ProjectToEntity()
    //{
    //    return x => new SchoolClass
    //    {
    //        PseudoName = x.PseudoName,
    //        Description = x.Description,
    //        Number = x.Number,
    //        Letter = x.Letter,
    //        StartYear = x.StartYear
    //    };
    //}
}
