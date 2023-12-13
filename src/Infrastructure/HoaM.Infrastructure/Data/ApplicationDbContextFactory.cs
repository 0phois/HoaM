using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace HoaM.Infrastructure.Data
{
    public sealed class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true)
                .Build();

            var options = configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>() ?? new();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(options.ConnectionString, contextBuilder => contextBuilder.MigrationsAssembly(options.MigrationsAssembly))
                .ReplaceService<IValueConverterSelector, TypelyValueConverterSelector>();

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
