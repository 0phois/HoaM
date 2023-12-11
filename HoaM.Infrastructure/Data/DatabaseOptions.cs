namespace HoaM.Infrastructure.Data
{
    public enum ProviderType { InMemory, PostgreSQL, SQLServer, SQLite }

    public sealed class DatabaseOptions
    {
        /// <summary>
        /// Determines if to use Data Protection when storing sensitive data 
        /// </summary>
        public bool ProtectSensitiveData { get; set; }

        /// <summary>
        /// Type of database provider
        /// </summary>
        public ProviderType ProviderType { get; set; }

        /// <summary>
        /// Database connection string
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// The name of the assembly where the Migrations folder resides
        /// </summary>
        public string MigrationsAssembly { get; set; } = string.Empty;
    }
}
