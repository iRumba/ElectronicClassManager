using ElectronicClassManager.Services.Dto.SchoolClass;

namespace ElectronicClassManager.Services;

public interface ISchoolClassService
{
    public Task<SchoolClassOutDto?> GetByIdAsync(Guid id);
    public Task<SchoolClassOutDto?> GetByPseudoNameAsync(string pseudoName);
    public Task<SchoolClassOutDto> CreateAsync(SchoolClassCreateDto dto);
    public Task<SchoolClassOutDto?> UpdateByIdAsync(Guid id, SchoolClassUpdateDto dto);
    public Task<SchoolClassOutDto?> UpdateByPseudoNameAsync(string pseudoName, SchoolClassUpdateDto dto);

    public Task<SchoolClassOutDto[]> FindAsync(SchoolClassFindDto dto);

    public Task DeleteByPseudoNameAsync(string pseudoName);

    public Task<bool> CanUsePseudoNameAsync(string pseudoName);
}