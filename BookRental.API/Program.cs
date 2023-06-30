using BookRental.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbContext();

builder.AddLogger();

builder.AddServices();

builder.AddControllers();

builder.AddFluentValidation();

builder.AddSwagger();

builder.ConfigureAuthentication();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

await app.SeedDatabaseAsync();

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
