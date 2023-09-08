namespace ElectronicClassManager.Entities;

public class Person : IdEntity
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Patronymic { get; set; }

    public required Gender Gender { get; set; }

    public DateOnly? BirthDate { get; set; }
}
