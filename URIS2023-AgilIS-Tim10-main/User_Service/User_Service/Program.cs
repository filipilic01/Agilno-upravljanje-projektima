using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using User_Service.Auth;
using User_Service.Data;
using User_Service.Models;
using User_Service.Models.OtherModelServices;
using User_Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoggerService, LoggerService>();

builder.Services.AddScoped<IServiceCall<BacklogItemDTO>, ServiceCall<BacklogItemDTO>>();
builder.Services.AddScoped<IServiceCall<FAQSectionDTO>, ServiceCall<FAQSectionDTO>>();
builder.Services.AddScoped<IServiceCall<ProductBacklogDTO>, ServiceCall<ProductBacklogDTO>>();
builder.Services.AddScoped<IServiceCall<ProjectDTO>, ServiceCall<ProjectDTO>>();
builder.Services.AddScoped<IServiceCall<SprintBacklogDTO>, ServiceCall<SprintBacklogDTO>>();
builder.Services.AddScoped<IServiceCall<TeamDTO>, ServiceCall<TeamDTO>>();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddSingleton<IConfiguration>(configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var secret = configuration["ApplicationSettings:JWT_Secret"].ToString();
// Inside ConfigureServices method
var tokenKey = Encoding.UTF8.GetBytes(secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


builder.Services.AddSingleton<IJwtAuthManager>(new JwtAuthManager(secret,configuration));

var app = builder.Build();
app.UseCors("CorsPolicy");
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
