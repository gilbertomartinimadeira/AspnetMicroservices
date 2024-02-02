
using Basket.API.Repositories;
using Discount.Grpc.Protos.Client;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = configuration["CacheSettings:ConnectionString"];
});

builder.Services.AddScoped<IBasketRepository,BasketRepository>();

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>( o =>{
    o.Address= new Uri("http://localhost:5287");
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}else {
    app.UseHttpsRedirection();
    app.UseAuthorization();
}



app.MapControllers();

app.Run();
