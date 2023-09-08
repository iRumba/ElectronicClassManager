namespace ElectronicClassManager.Dto;

public record SchoolClassCreateDto
{
    public required int Number { get; set; }
    public required string Letter { get; set; }
    public string? Description { get; set; }
    public required int StartYear { get; set; }
}