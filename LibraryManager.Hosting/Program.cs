using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using DataAccessLayer.Repository;
using BusinessObjects.Entity;
using DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Configure database context
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseInMemoryDatabase("LibraryDb")
);

// Register generic repository for all entity types
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Register services
builder.Services.AddTransient<ICatalogManager, CatalogManager>();

// Configure JSON options to handle circular references
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library Manager API",
        Version = "v1",
        Description = "A comprehensive REST API for managing libraries, books, and authors"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Manager API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
