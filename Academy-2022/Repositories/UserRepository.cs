using Academy_2022.Data;
using Academy_2022.Models;
using Academy_2022.Models.Dto;

namespace Academy_2022.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository()
        {
            _applicationDbContext = new ApplicationDbContext();
        }

        public IEnumerable<User> GetAll()
        {
            return _applicationDbContext.Users.ToList();
        }

        public User? GetById(int id)
        {

            return _applicationDbContext.Users.FirstOrDefault(user => user.Id == id);
        }

        public User Create(UserDto userDto)
        {
            var user = new Models.User
            {
                Email = userDto.Email
            };

            _applicationDbContext.Add(user);
            _applicationDbContext.SaveChanges();

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
