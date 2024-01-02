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
app.MapGet("/", () => "Hello World!");

// Starts API
app.Run();

// run code with: dotnet run