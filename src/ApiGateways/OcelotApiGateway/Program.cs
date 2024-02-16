using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();


IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("ocelot.json")
                            .Build();

builder.Services.AddOcelot(configuration);


var app = builder.Build();

app.UseOcelot().Wait();

app.MapGet("/", () => "Hello World!");

app.Run();
