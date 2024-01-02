using PizzaStore.DB;
using Microsoft.OpenApi.Models;



// you create a builder, from there you create app instance
var builder = WebApplication.CreateBuilder(args);

    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "PizzaStore API", Description = "Making the Pizzas you love", Version = "v1" });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});
// app instance has routing setup and can add middle
// get: fetch a resource just route
app.MapGet("/pizzas", () => PizzaDB.GetPizzas());

// get: use route param
app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));

// post: create resource product is sent into the lambda that handles the request.
app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));

// put: update resource
// should send a posted body with a resource that contains changes. You want these changes applied to an existing resource on the server
app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));

// delete: remove resource
app.MapDelete("/products/{id}", (int id) => PizzaDB.RemovePizza(id));
// Starts API
app.Run();

// run code with: dotnet run