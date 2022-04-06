using Academy_2022.Models;
using Academy_2022.Models.Dto;
using Academy_2022.Repositories;
using Academy_2022.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academy_2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public AccountController(
            IAccountService accountService,
            ITokenService tokenService,
            IUserRepository userRepository)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            var user = await _accountService.LoginAsync(loginDto);
            if (user == null)
            {
                return StatusCode(401);
            }

            var token = _tokenService.CreateToken(user);

            return Ok(new TokenDto
            {
                Token = token
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.CreateAsync(new CreateUserDto
            {
                Email = data.Email,
                Name = data.Name,
                Password = data.Password,
                Role = Role.User.ToString()
            });

            return Created("", user);
        }
    }
}
