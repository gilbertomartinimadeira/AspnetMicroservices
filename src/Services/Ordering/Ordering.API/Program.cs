using EventBus.Messages.Common;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Ordering.API.EventBusConsumer;
using Ordering.API.Extensions;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<BasketCheckoutConsumer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderContext>(options => 
{
    var connString = builder.Configuration["ConnectionStrings:ConnectionString"];
    options.UseSqlServer(connString);
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddMassTransit(config  => {
    
    config.UsingRabbitMq( (context, configurator) => {
        var hostAddress = builder.Configuration["EventBusSettings:HostAddress"];
        configurator.Host(hostAddress);
        configurator.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c => {
            c.ConfigureConsumer<BasketCheckoutConsumer>(context);
        });
    });
    config.AddConsumer<BasketCheckoutConsumer>();
});




var app = builder.Build();
app.MigrateDatabase<OrderContext>( (context, service) => {

    var logger = service.GetService<ILogger<OrderContextSeed>>();

    OrderContextSeed.SeedAsync(context, logger).Wait();
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
