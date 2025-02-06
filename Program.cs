using Microsoft.EntityFrameworkCore;
using Tome.API.Data;
using Tome.API.Services;

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
builder.Services.AddScoped<CharacterTypeService>();
builder.Services.AddScoped<FieldService>();


// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Use CORS
app.UseCors("AllowAll");

// Middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
