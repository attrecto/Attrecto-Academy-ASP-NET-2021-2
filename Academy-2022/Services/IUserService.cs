using Academy_2022.Models;

namespace Academy_2022.Services
{
    public interface IUserService
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
