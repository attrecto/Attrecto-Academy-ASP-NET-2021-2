using Academy_2022.Models;

namespace Academy_2022.Repositories
{
    public interface IUserCourseRepository
    {
        Task<List<CourseUser>> GetByUserIdAsync(int userId);
        Task<CourseUser> CreateAsync(int userId, int courseId);
        Task<bool> DeleteAsync(int userId, int courseId);
    }
}
