using System.Linq.Expressions;
using ElectronicClassManager.Entities;

namespace ElectronicClassManager.Services;

public interface ISchoolClassService
{
    public Task<SchoolClass?> GetByIdAsync(Guid id);
    public Task<SchoolClass?> GetByPseudoNameAsync(string pseudoName);
    public Task<SchoolClass> CreateAsync(SchoolClass entity);
    public Task<SchoolClass?> UpdateAsync(SchoolClass entity);
    public Task<SchoolClass[]> FindAsync(IEnumerable<Expression<Func<SchoolClass, bool>>> filters);

    public Task<SchoolClass?> DeleteByPseudoNameAsync(string pseudoName);

    public Task<bool> CanUsePseudoNameAsync(string pseudoName);
}