using Academy_2022.Models;
using Academy_2022.Models.Dto;

namespace Academy_2022.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User> CreateAsync(CreateUserDto userDto);
        Task<User?> UpdateAsync(UpdateUserDto userDto);
        Task<bool> DeleteAsync(int id);
        Task<User?> GetByEmailAsync(string email);
    }
}
