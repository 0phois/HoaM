using HoaM.API.Features;
using HoaM.Infrastructure;
using HoaM.Extensions.MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServices<IdentityUser>(builder.Configuration)
                .ConfigureDbContext<IdentityDbContext<IdentityUser>>(ctxBuilder: options => options.UseInMemoryDatabase("sample_db")); //TODO - remove EF InMemory dependency 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.UseMediatR();

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