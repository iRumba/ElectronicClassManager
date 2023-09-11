using ElectronicClassManager.Entities;
using System.Linq.Expressions;

namespace ElectronicClassManager.Services;

public interface IStudentService
{
    public Task<Student?> GetByIdAsync(Guid id);
    public Task<Student> CreateAsync(Student dto);
    public Task<Student> UpdateAsync(Student dto);

    public Task<Student[]> FindAsync(params Expression<Func<Student, bool>>[] filters);

    public Task<Student?> DeleteByIdAsync(Guid id);
}