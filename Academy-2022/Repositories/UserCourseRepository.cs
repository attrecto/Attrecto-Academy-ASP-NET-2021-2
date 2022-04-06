using Academy_2022.Data;
using Academy_2022.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy_2022.Repositories
{
    public class UserCourseRepository : IUserCourseRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserCourseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<List<CourseUser>> GetByUserIdAsync(int userId)
        {
            return _applicationDbContext.CourseUsers
                .Where(cu => cu.UserId == userId)
                .ToListAsync();
        }

        public async Task<CourseUser> CreateAsync(int userId, int courseId)
        {
            var courseUser = new Models.CourseUser
            {
                CourseId = courseId,
                UserId = userId
            };

            await _applicationDbContext.AddAsync(courseUser);
            await _applicationDbContext.SaveChangesAsync();

            return courseUser;
        }

        public async Task<bool> DeleteAsync(int userId, int courseId)
        {
            var courseUser = await _applicationDbContext.CourseUsers
                .FirstOrDefaultAsync(cu => cu.UserId == userId && cu.CourseId == courseId);

            try
            {
                _applicationDbContext.CourseUsers.Remove(courseUser);
                await _applicationDbContext.SaveChangesAsync();
                
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
