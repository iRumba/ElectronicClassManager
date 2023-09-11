using ElectronicClassManager.Services.Dto.Person;

namespace ElectronicClassManager.Services.Dto.Student;

public record StudentOutDto : PersonOutDto
{
    public required string ClassPseudoName { get; set; }
}