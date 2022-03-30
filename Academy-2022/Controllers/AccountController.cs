using Academy_2022.Models.Dto;
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

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _accountService.Login(loginDto);
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
    }
}
