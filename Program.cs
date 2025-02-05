using Microsoft.EntityFrameworkCore;
using Tome.Data;
using Tome.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// Register PostgreSQL Database Context
builder.Services.AddDbContext<TomeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<UniverseService>();
builder.Services.AddScoped<CharacterService>();
builder.Services.AddScoped<EventService>();

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
