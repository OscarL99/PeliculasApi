using PeliculasApi.Models;
using PeliculasApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MovieStoreDatabaseSettings>(builder.Configuration.GetSection("cursonetcore"));
builder.Services.AddSingleton<MoviesService>();


builder.Services.AddControllers().AddJsonOptions( options=>options.JsonSerializerOptions.PropertyNamingPolicy=null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
