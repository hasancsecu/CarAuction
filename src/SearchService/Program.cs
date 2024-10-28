using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

await DB.InitAsync("SearchDb", MongoClientSettings
    .FromConnectionString(builder.Configuration.GetConnectionString("MongoDbConnection")));

await DB.Index<Item>()
    .Key(x => x.Make, KeyType.Text)
    .Key(x => x.Model, KeyType.Text)
    .Key(x => x.Color, KeyType.Text)
    .CreateAsync();

app.Run();