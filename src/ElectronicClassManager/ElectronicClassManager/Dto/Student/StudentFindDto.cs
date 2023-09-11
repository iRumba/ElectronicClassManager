using ElectronicClassManager.Dto.Person;

namespace ElectronicClassManager.Dto.Student;

public record StudentFindDto : PersonFindDto
{
    public required  string ClassPseudoName { get; set; }
}