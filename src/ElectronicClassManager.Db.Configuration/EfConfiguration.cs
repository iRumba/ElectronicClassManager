namespace ElectronicClassManager.Db.Configuration;

public class EfConfiguration
{
    public required string Address { get; set; }
    public required int Port { get; set; }
    public required string Db { get; set; }
    public required string User { get; set; }
    public required string Password { get; set; }
}