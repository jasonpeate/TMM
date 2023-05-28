using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMM.Database;
using TMM.Logic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TMMDbContext>(opt => opt.UseInMemoryDatabase("Student"));
builder.Services.AddSingleton<ICustomerHelper>(new CustomerHelper());
// Add services to the container.

var app = builder.Build();


app.MapGet("/Customers", async (TMMDbContext db, ICustomerHelper ch) =>
{
    return Results.Ok(ch.GetCustomers(db,false));
});

app.MapGet("/Customers/ActiveOnly", async (TMMDbContext db, ICustomerHelper ch) =>
{
    return Results.Ok(ch.GetCustomers(db, true));
});

app.MapGet("/Customer", async (int ID,TMMDbContext db, ICustomerHelper ch) =>
{
    return Results.Ok(ch.GetCustomer(db, ID));
});


app.Run();