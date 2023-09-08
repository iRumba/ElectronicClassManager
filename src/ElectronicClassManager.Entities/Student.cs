namespace ElectronicClassManager.Entities;

public class Student : Person
{
    public required SchoolClass SchoolClass { get; set; }
}