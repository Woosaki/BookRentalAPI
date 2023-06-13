//using BookRental.Application.Dtos;
//using BookRental.Domain.Entities;
//using BookRental.Infrastructure;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.EntityFrameworkCore;
//using System.Net;
//using System.Text.Json;

//namespace BookRental.IntegrationTests;

//public class AuthorsControllerTests : IClassFixture<WebApplicationFactory<Program>>
//{
//	private readonly WebApplicationFactory<Program> _factory;
//	private readonly HttpClient _client;

//	public AuthorsControllerTests(WebApplicationFactory<Program> factory)
//	{
//		_factory = factory
//			.WithWebHostBuilder(builder =>
//			{
//				builder.ConfigureServices(services =>
//				{
//					var dbContextOptions = services.SingleOrDefault(service =>
//						service.ServiceType == typeof(DbContextOptions<BookRentalDbContext>));

//					if (dbContextOptions != null)
//					{
//						services.Remove(dbContextOptions);
//					}

//					services.AddDbContext<BookRentalDbContext>(options =>
//					{
//						options.UseInMemoryDatabase($"InMemoryDb{Guid.NewGuid()}");
//					});
//				});
//			});

//		_client = _factory.CreateClient();

//		var author = new Author()
//		{
//			Id = 1,
//			Name = "Author 1",
//			Born = new DateTime(1999, 9, 30),
//			Books = new List<Book>()
//			{
//				new Book()
//				{
//					Id = 1,
//					Title = "Title 1",
//					PublicationYear = 2017,
//					Genre = new Genre() { Id = 1, Name = "Genre 1" }
//				},
//				new Book()
//				{
//					Id = 2,
//					Title = "Title 2",
//					PublicationYear = 2019,
//					Genre = new Genre() { Id = 2, Name = "Genre 2" }
//				}
//			}
//		};

//		SeedAuthor(author).Wait();
//	}

//	[Fact]
//	public async Task GetAll_ReturnsOkResult()
//	{
//		var result = await _client.GetAsync("/api/authors");

//		Assert.Equal(HttpStatusCode.OK, result.StatusCode);

//		var responseString = await result.Content.ReadAsStringAsync();
//		Assert.NotEmpty(responseString);

//		var authors = JsonSerializer.Deserialize<List<AuthorDto>>(responseString);
//		Assert.NotNull(authors);
//		Assert.True(authors.Any());
//	}

//	[Fact]
//	public async Task Get_WithValidId_ReturnsOkResult()
//	{
//		var result = await _client.GetAsync("/api/authors/2000");

//		Assert.Equal(HttpStatusCode.OK, result.StatusCode);
//	}

//	private async Task SeedAuthor(Author author)
//	{
//		using var scope = _factory.Services.CreateScope();
//		var dbContext = scope.ServiceProvider.GetRequiredService<BookRentalDbContext>();
//		await dbContext.Authors.AddAsync(author);
//		await dbContext.SaveChangesAsync();
//	}
//}