using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using WebApi.DBOperations;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Token:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenSecurity"])),
                ClockSkew = TimeSpan.Zero
            };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();

var app = builder.Build();

builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));

builder.Services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDbContext>());

using (var scope = app.Services.CreateScope()) { var services = scope.ServiceProvider; DataGenerator.Initialize(services); }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCustomExceptionBuilder();

app.MapControllers();

app.Run();
