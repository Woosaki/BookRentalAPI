using BookRental.Application.Interfaces;
using BookRental.Application.Dtos;
using BookRental.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookRental.UnitTests;

public class AuthorsControllerTests
{
	private readonly Mock<IAuthorService> _authorServiceMock;
	private readonly AuthorsController _authorsController;

	public AuthorsControllerTests()
	{

		_authorServiceMock = new Mock<IAuthorService>();
		_authorsController = new AuthorsController(_authorServiceMock.Object);
	}

	[Fact]
	public async Task GetAsync_ReturnsOk_WhenAuthorsExist()
	{
		_authorServiceMock.Setup(service =>
			service.GetAsync())
			.ReturnsAsync(new List<AuthorDto>() { new AuthorDto() });

		var result = await _authorsController.GetAllAsync();

		var actionResult = Assert.IsType<ActionResult<List<AuthorDto>>>(result);
		Assert.IsType<OkObjectResult>(actionResult.Result);
	}

	[Fact]
	public async Task GetByIdAsync_ReturnsOk_WhenAuthorExists()
	{
		_authorServiceMock.Setup(service =>
			service.GetByIdAsync(It.IsAny<int>()))
			.ReturnsAsync(new AuthorDto());

		var result = await _authorsController.GetAsync(1);

		var actionResult = Assert.IsType<ActionResult<AuthorDto>>(result);
		Assert.IsType<OkObjectResult>(actionResult.Result);
	}
}