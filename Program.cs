//using PizzaStore.DB;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;



// you create a builder, from there you create app instance
var builder = WebApplication.CreateBuilder(args);
// to enable db creation, set db connection string to migrate data model to sqlite db
// This code checks the configuration provider for a connection string named Pizzas. If it doesn't find one, it will use Data Source=Pizzas.db as the connection string. SQLite will map this string to a file.
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=Pizzas.db";

    
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddDbContext<PizzaDb>(options => options.UseInMemoryDatabase("items"));
//replace in memory to db
builder.Services.AddSqlite<PizzaDb>(connectionString);
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
// app.MapGet("/pizzas", () => PizzaDB.GetPizzas());
app.MapGet("/pizzas", async(PizzaDb db) => await db.Pizzas.ToListAsync());

// get: use route param
// app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));
app.MapGet("/pizza/{id}", async (PizzaDb db, int id) => await db.Pizzas.FindAsync(id));

// post: create resource product is sent into the lambda that handles the request.
// app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
app.MapPost("/pizza", async (PizzaDb db, Pizza pizza) =>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});

// put: update resource
// should send a posted body with a resource that contains changes. You want these changes applied to an existing resource on the server
// app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
app.MapPut("/pizza/{id}", async (PizzaDb db, Pizza updatepizza, int id) =>
{
      var pizza = await db.Pizzas.FindAsync(id);
      if (pizza is null) return Results.NotFound();
      pizza.Name = updatepizza.Name;
      pizza.Description = updatepizza.Description;
      await db.SaveChangesAsync();
      return Results.NoContent();
});

// delete: remove resource
// app.MapDelete("/products/{id}", (int id) => PizzaDB.RemovePizza(id));
app.MapDelete("/pizza/{id}", async (PizzaDb db, int id) =>
{
   var pizza = await db.Pizzas.FindAsync(id);
   if (pizza is null)
   {
      return Results.NotFound();
   }
   db.Pizzas.Remove(pizza);
   await db.SaveChangesAsync();
   return Results.Ok();
});

// Starts API
app.Run();

// run code with: dotnet run