namespace ElectronicClassManager.Entities;

public class SchoolClass : IdEntity
{
    public required int Number { get; set; } 
    public required string Letter { get; set; }
}