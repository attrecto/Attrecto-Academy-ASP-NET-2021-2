using Academy_2022.Models;
using Academy_2022.Repositories;

namespace Academy_2022.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _userRepository.GetByEmailAsync(email);
        }
    }
}
