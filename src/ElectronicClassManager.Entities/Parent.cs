namespace ElectronicClassManager.Entities;

public class Parent : Person
{
    public required Student Child { get; set; }
}