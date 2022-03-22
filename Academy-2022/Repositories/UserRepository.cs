using Academy_2022.Data;
using Academy_2022.Models;
using Academy_2022.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Academy_2022.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<List<User>> GetAllAsync()
        {
            return _applicationDbContext.Users.ToListAsync();
        }

        public Task<User?> GetByIdAsync(int id)
        {

            return _applicationDbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User> CreateAsync(UserDto userDto)
        {
            var user = new Models.User
            {
                Email = userDto.Email
            };

            var addUser = await _applicationDbContext.AddAsync(user);
            var saveUser = await _applicationDbContext.SaveChangesAsync();

            return user;
        }

        public User? Update(int id, UserDto userDto)
        {
            var user = _applicationDbContext.Users.FirstOrDefault(user => user.Id == id);
            if(user == null)
            {
                return null;
            }

            user.Email = userDto.Email;

            _applicationDbContext.SaveChanges();

            return user;
        }

        public bool Delete(int id)
        {
            var user = _applicationDbContext.Users.FirstOrDefault(user => user.Id == id);

            try
            {
                _applicationDbContext.Remove(user);
                _applicationDbContext.SaveChanges();
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }
    }
}
