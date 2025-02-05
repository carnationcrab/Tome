using Tome.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register services (mocked services)
builder.Services.AddScoped<UniverseService>();
builder.Services.AddScoped<CharacterService>();
builder.Services.AddScoped<EventService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
