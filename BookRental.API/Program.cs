using BookRental.API.Extensions;
using BookRental.Application.Dtos.UserDtos;
using BookRental.Application.Interfaces;
using BookRental.Application.Mapping;
using BookRental.Application.Services;
using BookRental.Application.Validators;
using BookRental.Domain.Entities;
using BookRental.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.OpenApi.Models;
using BookRental.Application.Dtos.AuthorDtos;
using BookRental.Application.Dtos.BookDtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookRentalDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnectionString"),
		x => x.MigrationsAssembly("BookRental.Infrastructure")));

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();
builder.Services.AddControllers()
	.AddJsonOptions(options =>
		options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg => 
{
	cfg.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Issuer"],
		IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
	};
});

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<CreateAuthorDto>, CreateAuthorDtoValidator>();
builder.Services.AddScoped<IValidator<CreateBookDto>, CreateBookDtoValidator>();

builder.Services.AddAutoMapper(typeof(GeneralProfile).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		Scheme = "bearer"
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetService<BookRentalDbContext>();
await BookRentalDbContextSeed.SeedAsync(context);

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseErrorHandling();

app.MapControllers();

app.Run();

public partial class Program { }
