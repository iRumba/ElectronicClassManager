using ElectronicClassManager.Entities;

namespace ElectronicClassManager.Services.Dto.Person;

public record PersonFindDto
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Patronymic { get; set; }

    public Gender? Gender { get; set; }

    public DateOnly? BirthDate { get; set; }
}