using HoaM.API;
using HoaM.Application;
using HoaM.Domain;
using HoaM.Extensions.MediatR;
using HoaM.Infrastructure;
using HoaM.Infrastructure.Data;
using HoaM.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServices<ApplicationUser, ApplicationRole, ApplicationDbContext>(builder.Configuration)
                .ConfigureDbContext()
                .WithDefaultRepositories();

builder.Services.AddEndpointServices();

builder.Services.UseMediatR();
builder.Services.UseDefaultTimeProvider();

builder.Services.AddScoped<IMember, SystemUser>(); //TODO - review current user implementation

var app = builder.Build();

app.MapFeatureEndpoints()
   .MapIdentityApi<IdentityUser>().WithTags("Credentials");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

app.UseHealthChecks("/health");

app.UseAuthentication();
app.UseAuthorization();