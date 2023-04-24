using API.Services;
using Common.Options;

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
var argsWithPort = new[] { $"--urls=http://*:{port}" }.Concat(args).ToArray();

var builder = WebApplication.CreateBuilder(argsWithPort);

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowSpecificOrigins",
        builder =>
        {
            builder
                .WithOrigins(
                    "http://localhost:3000",
                    "https://localhost:3000",
                    "https://bt-weather-frontend.herokuapp.com"
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.Configure<WeatherApiOptions>(builder.Configuration.GetSection("WeatherApi"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IWeatherService, WeatherService>();

var app = builder.Build();

// Add CORS middleware to the pipeline
app.UseCors("AllowSpecificOrigins");

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
