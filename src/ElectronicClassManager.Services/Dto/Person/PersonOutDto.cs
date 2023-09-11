using ElectronicClassManager.Entities;

namespace ElectronicClassManager.Services.Dto.Person;

public record PersonOutDto : IdEntityOutDto
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Patronymic { get; set; }

    public required Gender Gender { get; set; }

    public DateOnly? BirthDate { get; set; }
}