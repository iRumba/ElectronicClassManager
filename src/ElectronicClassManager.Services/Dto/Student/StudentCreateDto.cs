using ElectronicClassManager.Services.Dto.Person;

namespace ElectronicClassManager.Services.Dto.Student;

public record StudentCreateDto : PersonCreateDto
{
    public required Entities.SchoolClass SchoolClass { get; set; }
}