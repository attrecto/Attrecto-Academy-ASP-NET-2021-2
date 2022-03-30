using Academy_2022.Models;
using Academy_2022.Models.Dto;

namespace Academy_2022.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;

        public AccountService(IUserService userService)
        {
            _userService = userService;
        }

        public User? Login(LoginDto loginDto)
        {
            var user = _userService.GetByEmail(loginDto.Email);
            if (user == null || user.Password != loginDto.Password)
            {
                return null;
            }

            return user;
        }
    }
}
