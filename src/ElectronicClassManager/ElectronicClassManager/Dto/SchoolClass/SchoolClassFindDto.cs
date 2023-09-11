namespace ElectronicClassManager.Dto.SchoolClass;

public record SchoolClassFindDto
{
    public int? Number { get; set; }
    public string? Letter { get; set; }
    public int? StartYear { get; set; }
    public string? Description { get; set; }
}