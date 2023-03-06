
using Microsoft.EntityFrameworkCore;
using ProductApi.DbModel;
using ProductApi.Interface;
using ProductApi.RabbitMQProduct;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
var factory = new ConnectionFactory() { HostName = "localhost" };
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMessageProducer, RabbitMQProducerProduct>();
builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

var app = builder.Build();

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
