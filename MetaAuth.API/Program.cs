using FluentValidation.AspNetCore;
using MediatR;
using MetaAuth.API.Core.Features;
using MetaAuth.API.Middleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

const string mainPolicy = "MetaAuthPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: mainPolicy,
        policy =>
        {
            policy.WithOrigins("https://localhost:7235", "https://localhost:5001")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = builder.Environment.ApplicationName, Version = "v1" });
});

builder.Services.AddMediatR(x 
    => x.AsScoped(), typeof(Program));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.RegisterFeatures();

var app = builder.Build();

app.UseCors(mainPolicy);

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Environment.ApplicationName} v1"));
}

app.Run();