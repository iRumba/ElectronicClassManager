using ElectronicClassManager.Services.Dto.Student;

namespace ElectronicClassManager.Services;

public interface IStudentService
{
    public Task<StudentOutDto?> GetByIdAsync(Guid id);
    public Task<StudentOutDto> CreateAsync(StudentCreateDto dto);
    public Task<StudentOutDto?> UpdateByIdAsync(Guid id, StudentUpdateDto dto);

    public Task<StudentOutDto[]> FindAsync(StudentFindDto dto);

    public Task<StudentOutDto?> DeleteByIdAsync(Guid id);
}