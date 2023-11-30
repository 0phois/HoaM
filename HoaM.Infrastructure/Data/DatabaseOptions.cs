namespace HoaM.Infrastructure.Data
{
    public enum ProviderType { InMemory, PostgreSQL, SQLServer, SQLite }
 
    public class DatabaseOptions
    {
        public ProviderType ProviderType { get; set; }
        public string ConnectionString { get; set; } = string.Empty;
        public string MigrationsAssembly { get; set; } = string.Empty;
    }
}
