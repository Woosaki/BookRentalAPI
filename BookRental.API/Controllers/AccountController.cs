using BookRental.Application.Dtos.UserDtos;
using BookRental.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

	[HttpPost("login")]
	public async Task<ActionResult> Login([FromBody] LoginUserDto dto)
	{
		string token = await _accountService.GenerateJwt(dto);

		return Ok(token);
	}

	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<List<UserDto>>> GetAll()
	{
		var accounts = await _accountService.GetAllAsync();

		return Ok(accounts);
	}

	[HttpPatch("ChangeRole")]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<UserDto>> ChangeRole([FromBody] ChangeRoleDto dto)
	{
		var user = await _accountService.ChangeRole(dto);

		return Ok(user);
	}
}
