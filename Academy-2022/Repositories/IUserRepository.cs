using Academy_2022.Models;
using Academy_2022.Models.Dto;

namespace Academy_2022.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User? GetById(int id);
        User Create(UserDto userDto);
        User? Update(int id, UserDto userDto);
        bool Delete(int id);
    }
}
