using MassTransit;
using Microsoft.EntityFrameworkCore;
using UserService.Datas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddMassTransit(configurator =>

       configurator.UsingRabbitMq((context, _configure) =>
       {
           _configure.Host(builder.Configuration["RabbitMQ"]);
       }));

builder.Services.AddDbContext<UserApiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MySQL"));
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
