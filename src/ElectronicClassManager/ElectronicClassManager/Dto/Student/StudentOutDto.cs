using ElectronicClassManager.Dto.Person;

namespace ElectronicClassManager.Dto.Student;

public record StudentOutDto : PersonOutDto
{
    public required string ClassPseudoName { get; set; }
}