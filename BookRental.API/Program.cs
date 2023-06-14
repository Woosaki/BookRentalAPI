using BookRental.API.Extensions;
using BookRental.Application.Interfaces;
using BookRental.Application.Mapping;
using BookRental.Application.Services;
using BookRental.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookRentalDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnectionString"),
		x => x.MigrationsAssembly("BookRental.Infrastructure")));

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();


builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddAutoMapper(typeof(GeneralProfile).Assembly);
builder.Services.AddControllers().AddJsonOptions(options =>
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
