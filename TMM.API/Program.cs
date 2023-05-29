using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMM.Database;
using TMM.Logic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TMMDbContext>(opt => opt.UseInMemoryDatabase("TMM"), ServiceLifetime.Singleton);
builder.Services.AddSingleton<ICustomerHelper, CustomerHelper>();
// Add services to the container.

var app = builder.Build();


app.MapGet("/Customers", async (ICustomerHelper ch) =>
{
    return Results.Ok(ch.GetCustomers(false));
});

app.MapGet("/Customers/ActiveOnly", async (ICustomerHelper ch) =>
{
    return Results.Ok(ch.GetCustomers(true));
});

app.MapGet("/Customer", async (int ID, ICustomerHelper ch) =>
{
    return Results.Ok(ch.GetCustomer(ID));
});


app.Run();