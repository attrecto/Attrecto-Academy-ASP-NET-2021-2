using Academy_2022.Models;
using Academy_2022.Models.Dto;

namespace Academy_2022.Services
{
    public interface IAccountService
    {
        Task<User?> LoginAsync(LoginDto loginDto);
    }
}
