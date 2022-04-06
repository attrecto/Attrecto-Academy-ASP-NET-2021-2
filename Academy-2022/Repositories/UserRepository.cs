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
            return _applicationDbContext.Users
                .Include(x => x.Courses)
                .ToListAsync();
        }

        public Task<User?> GetByIdAsync(int id)
        {

            return _applicationDbContext.Users
                .Include(x => x.Courses)
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User> CreateAsync(CreateUserDto userDto)
        {
            var user = new Models.User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
                Role = userDto.Role,
                Courses = new List<Course>()
            };

            await _applicationDbContext.AddAsync(user);
            await _applicationDbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User?> UpdateAsync(UpdateUserDto userDto)
        {
            var user = await _applicationDbContext.Users
                .Include(x => x.Courses)
                .FirstOrDefaultAsync(user => user.Id == userDto.Id);
            
            if(user == null)
            {
                return null;
            }

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.Password = userDto.Password;
            user.Role = userDto.Role;

            await _applicationDbContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(user => user.Id == id);

            try
            {
                _applicationDbContext.Users.Remove(user);
                _applicationDbContext.SaveChangesAsync();
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
