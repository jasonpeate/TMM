using Microsoft.EntityFrameworkCore;
using TMM.Database;
using TMM.Logic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TMMDbContext>(opt => opt.UseInMemoryDatabase("Student"));
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/Customers", async (TMMDbContext db, ICustomerHelper ch) =>
{
    return ch.GetCustomers(db, false);
});

app.Run();