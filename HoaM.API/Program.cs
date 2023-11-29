using HoaM.API.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>() //TODO - create ApplicationUser
                .AddEntityFrameworkStores<IdentityDbContext<IdentityUser>>();

builder.Services.Configure<IdentityOptions>(builder.Configuration.GetSection(nameof(IdentityOptions)));
builder.Services.AddDbContext<IdentityDbContext<IdentityUser>>(options => options.UseInMemoryDatabase("db")); //TODO - remove EF InMemory dependency 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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