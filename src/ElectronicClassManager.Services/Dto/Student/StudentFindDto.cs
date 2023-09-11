using ElectronicClassManager.Services.Dto.Person;
using ElectronicClassManager.Services.Dto.SchoolClass;

namespace ElectronicClassManager.Services.Dto.Student;

public record StudentFindDto : PersonFindDto
{
    public required  string ClassPseudoName { get; set; }
}