using BattleshipsGameAPI.Services;
using BattleshipsGameAPI.Services.Options;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPlayerRepository, JsonPlayerRepository>();
builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.Configure<JsonPlayerRepositoryOptions>(
builder.Configuration.GetSection(nameof(JsonPlayerRepositoryOptions)));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
