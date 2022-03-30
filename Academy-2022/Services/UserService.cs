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

        public User? GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }
    }
}
