// you create a builder, from there you create app instance
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// app instance has routing setup and can add middle
app.MapGet("/", () => "Hello World!");

// Starts API
app.Run();

// run code with: dotnet run