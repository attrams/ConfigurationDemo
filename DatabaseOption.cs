namespace ConfigurationDemo;

public class DatabaseOption
{
    public const string SectionName = "Database";
    public const string SystemDatabaseSectionName = "System";
    public const string BusinessDatabaseSectionName = "Business";
    public string Type { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
}