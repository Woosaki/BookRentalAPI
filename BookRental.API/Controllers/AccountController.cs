using BookRental.Application.Dtos;
using BookRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.API.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly IAccountService _accountService;

	public AccountController(IAccountService accountService)
	{
		_accountService = accountService;
	}

	[HttpPost("register")]
	public async Task<ActionResult> Register([FromBody] RegisterUserDto dto)
	{
		await _accountService.RegisterAsync(dto);

		return Ok();
	}
}
