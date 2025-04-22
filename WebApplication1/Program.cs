using System.Text.Json;
using System.Text.Json.Serialization;
using WebApplication1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Регистрация репозиториев
builder.Services.AddSingleton<IAnimalRepository, InMemoryAnimalRepository>();
builder.Services.AddSingleton<IEnclosureRepository, InMemoryEnclosureRepository>();
builder.Services.AddSingleton<IFeedingScheduleRepository, InMemoryFeedingScheduleRepository>();

// Регистрация сервисов
builder.Services.AddSingleton<IAnimalTransferService, AnimalTransferService>();  
builder.Services.AddSingleton<IFeedingOrganizationService, FeedingOrganizationService>(); 
builder.Services.AddSingleton<IZooStatisticsService, ZooStatisticsService>(); 

builder.Services.AddLogging(config =>
{
    config.AddConsole(); 
});

// Добавление Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Добавление авторизации
builder.Services.AddAuthorization();

// Добавление контроллеров
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

// Использование Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();