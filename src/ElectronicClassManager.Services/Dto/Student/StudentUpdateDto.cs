using ElectronicClassManager.Services.Dto.Person;

namespace ElectronicClassManager.Services.Dto.Student;

public record StudentUpdateDto : PersonUpdateDto
{
    public required Entities.SchoolClass SchoolClass { get; set; }
}