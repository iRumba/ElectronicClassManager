using ElectronicClassManager.Entities;

namespace ElectronicClassManager.Dto.Person;

public record PersonCreateDto
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Patronymic { get; set; }

    public required Gender Gender { get; set; }

    public DateOnly? BirthDate { get; set; }
}