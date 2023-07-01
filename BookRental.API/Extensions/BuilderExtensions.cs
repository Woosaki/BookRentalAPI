using BookRental.Application.Dtos.AuthorDtos;
using BookRental.Application.Dtos.BookDtos;
using BookRental.Application.Dtos.UserDtos;
using BookRental.Application.Interfaces;
using BookRental.Application.Mapping;
using BookRental.Application.Services;
using BookRental.Application.Validators;
using BookRental.Domain.Entities;
using BookRental.Infrastructure;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System.Text;
using System.Text.Json.Serialization;

namespace BookRental.API.Extensions;

public static class BuilderExtensions
{
	public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
	{
		builder.Services.AddDbContext<BookRentalDbContext>(options =>
			options.UseSqlServer(
				builder.Configuration.GetConnectionString("DefaultConnectionString"),
				x => x.MigrationsAssembly("BookRental.Infrastructure")));

		return builder;
	}

	public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder)
	{
		builder.Logging.ClearProviders();
		builder.Logging.SetMinimumLevel(LogLevel.Trace);
		builder.Host.UseNLog();

		return builder;
	}

	public static WebApplicationBuilder AddControllers(this WebApplicationBuilder builder)
	{
		builder.Services.AddControllers()
		.AddJsonOptions(options =>
			options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

		return builder;
	}

	public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
	{
		builder.Services.AddScoped<IAuthorService, AuthorService>();
		builder.Services.AddScoped<IBookService, BookService>();
		builder.Services.AddScoped<IAccountService, AccountService>();

		builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

		builder.Services.AddAutoMapper(typeof(GeneralProfile).Assembly);

		return builder;
	}

	public static WebApplicationBuilder AddFluentValidation(this WebApplicationBuilder builder)
	{
		builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

		builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
		builder.Services.AddScoped<IValidator<CreateAuthorDto>, CreateAuthorDtoValidator>();
		builder.Services.AddScoped<IValidator<CreateBookDto>, CreateBookDtoValidator>();
		builder.Services.AddScoped<IValidator<ChangeRoleDto>, ChangeRoleDtoValidator>();

		return builder;
	}

	public static WebApplicationBuilder ConfigureAuthentication(this WebApplicationBuilder builder)
	{
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

		return builder;
	}

	public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
	{
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

		return builder;
	}
}
