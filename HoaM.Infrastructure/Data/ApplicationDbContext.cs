using HoaM.Domain;
using HoaM.Domain.Features;
using HoaM.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;
using System.Text.Json;

namespace HoaM.Infrastructure.Data
{
    public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, AssociationMemberId>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            base.OnModelCreating(builder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Occurrence>().HaveConversion<JsonConverter<Occurrence>>();
            configurationBuilder.Properties<Schedule>().HaveConversion<JsonConverter<Schedule>>();

            base.ConfigureConventions(configurationBuilder);
        }
    }

    internal sealed class JsonConverter<T> : ValueConverter<T, string> where T : class
    {
        public JsonConverter() : base(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<T>(v, new JsonSerializerOptions())) { }
    }
}
