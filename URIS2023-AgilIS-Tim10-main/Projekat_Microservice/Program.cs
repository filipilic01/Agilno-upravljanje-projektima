﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Projekat_Microservice.Data;
using Projekat_Microservice.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ILoggerService, LoggerService>();
builder.Services.AddScoped<IServiceCall, ServiceCall>();
var secret = configuration["ApplicationSettings:JWT_Secret"].ToString();
var key = Encoding.ASCII.GetBytes(secret);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.SaveToken = true;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                       ValidateIssuerSigningKey = true,
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });

builder.Services.AddSwaggerGen(c =>
{
    var securitySchema = new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securitySchema);

    var securityRequirement = new OpenApiSecurityRequirement
                {
                    {securitySchema, new[] {"Bearer"}}
                };

    c.AddSecurityRequirement(securityRequirement);

});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
        .RequireAuthenticatedUser().Build());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
