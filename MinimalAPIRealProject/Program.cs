using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MinimalAPIRealProject.DB;
using MinimalAPIRealProject.Endpoints;
using MinimalAPIRealProject.FluentValidation;
using MinimalAPIRealProject.Models.Config;
using MinimalAPIRealProject.Models.Entity;
using MinimalAPIRealProject.Repository;
using MinimalAPIRealProject.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region DI
builder.Services.Configure<DbConfigRecord>(builder.Configuration.GetSection($"DbConfiguration")); //Prod baza- Prod; Test baza- Test
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection($"Jwt")); //Prod baza- Prod; Test baza- Test
builder.Services.AddScoped<DbConnect>();
builder.Services.AddScoped<DbOperation>();
builder.Services.AddScoped<IOperationRepository, OperationRepository>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<JwtProvider>();
#endregion

#region Authentication and Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();
#endregion

var app = builder.Build();

#region Swagger
app.UseSwagger();
app.UseSwaggerUI();
#endregion

#region Minimal API Extension Endpoints
//app.UseBookEndpoints(); //Taner Saydam(Udemy)
app.MapGroup("/api/Book/").MapBookEndpoints(); //Patrick God
#endregion


app.Run();
