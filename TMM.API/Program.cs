using NLog;
using TMM.Database;
using TMM.Logic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ITMMDbContext,RealWorldDB>(ServiceLifetime.Singleton);
builder.Services.AddTransient<CustomerRepository>();
builder.Services.AddTransient<AddressRepository>();
builder.Services.AddTransient<NLog.ILogger>(a => LogManager.GetCurrentClassLogger());
builder.Services.AddTransient<ICustomerService, CustomerService>();


var app = builder.Build();


app.MapGet("/Customers", async (ICustomerService ch) =>
{
    return Results.Ok(ch.GetCustomers(false));
});

app.MapPost("/Customers/Add", async (CompleteCustomerModel data, ICustomerService ch) =>
{
    var _result = ch.AddCustomer(data);

    if (_result.Result)
    {
        return Results.Ok(_result.ID);
    }
    else
    {
        //TODO : return status code needs to be better here
        return Results.Problem(statusCode: 500, detail: _result.Message);
    }
});

app.MapGet("/Customers/ActiveOnly", async (ICustomerService ch) =>
{
    return Results.Ok(ch.GetCustomers(true));
});

app.MapGet("/Customer", async (int ID, ICustomerService ch) =>
{
    return Results.Ok(ch.GetCustomer(ID));
});

app.MapDelete("/Customer/DeleteAddress", async (int CustomerID, int AddressID, ICustomerService ch) =>
{
    ReponseResult result = ch.DeleteAddress(CustomerID, AddressID);

    if (result.Result)
    {
        return Results.Ok();
    } 
    else
    {
        //TODO : return status code needs to be better here
        return Results.Problem(statusCode:500 ,detail: result.Message);
    }    
});

app.MapPut("/Customer/UpdateMainAddress", async (int CustomerID, int AddressID, ICustomerService ch) =>
{
    ReponseResult result = ch.SetMainAddress(CustomerID, AddressID);

    if (result.Result)
    {
        return Results.Ok();
    }
    else
    {
        //TODO : return status code needs to be better here
        return Results.Problem(statusCode: 500, detail: result.Message);
    }
});

app.MapPut("/Customer/MarkAsInactive", async (int CustomerID, ICustomerService ch) =>
{
    ReponseResult result = ch.MarkCustomerAsInactive(CustomerID);

    if (result.Result)
    {
        return Results.Ok();
    }
    else
    {
        //TODO : return status code needs to be better here
        return Results.Problem(statusCode: 500, detail: result.Message);
    }
});

app.MapDelete("/Customer/Delete", async (int CustomerID, ICustomerService ch) =>
{
    ReponseResult result = ch.DeleteCustomer(CustomerID);

    if (result.Result)
    {
        return Results.Ok();
    }
    else
    {
        //TODO : return status code needs to be better here
        return Results.Problem(statusCode: 500, detail: result.Message);
    }
});

app.Run();