using Academy_2022.Models;
using Academy_2022.Models.Dto;

namespace Academy_2022.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User> CreateAsync(UserDto userDto);
        User? Update(int id, UserDto userDto);
        bool Delete(int id);
        User? GetByEmail(string email);
    }
}
