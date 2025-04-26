using Microsoft.EntityFrameworkCore;
using MovieFlix.Application.Classes;
using MovieFlix.Application.Interfaces;
using MovieFlix.Authentication.Classes;
using MovieFlix.Authentication.Interfaces;
using MovieFlix.Infrastructure;
using MovieFlix.Infrastructure.Classes;
using MovieFlix.Infrastructure.Interfaces;

Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder.WithOrigins("http://localhost:5173")
                          .WithMethods("GET", "POST", "PUT", "DELETE")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JWT Authentication
var jwtConfig = new JwtConfiguration(configuration);
builder.Services.AddSingleton<IJwtConfiguration>(jwtConfig);
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IPasswordService, PasswordService>();
builder.Services.AddJwtAuthentication(jwtConfig);


//DB stuff
builder.Services.AddDbContext<MovieDBContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("MovieFlix.API")));
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

//JWT Authentication

app.UseAuthentication();
app.UseCors("AllowReactApp");
app.UseAuthorization();


app.MapControllers();

app.Run();
