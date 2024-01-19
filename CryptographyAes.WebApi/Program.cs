using CryptographyAes.WebApi.Business;
using CryptographyAes.WebApi.Entities.Dto;
using CryptographyAes.WebApi.Business.ApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowedOrigins", policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1_0",
        Title = $"{Assembly.GetExecutingAssembly().GetName().Name}"
    });
});

var services = builder.Services.AddMemoryCache();

services.AddScoped<IAesService, AesService>();

services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
        .Where(e => !e.Key.StartsWith("request"))
            .ToDictionary(
                e => e.Key.Replace("$.", ""),
                e => e.Value.Errors.Select(x => x.ErrorMessage).ToArray()
            );

        var problemDetails = new DefaultResponse()
        {
            statusDescription = "Ocurrieron uno o más errores de validación.",
            status = false,
            data = errors
        };

        return new BadRequestObjectResult(problemDetails);
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use((context, next) =>
{
    context.Response.Headers.Add("Version", "1.0");
    return next();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
