using Academy_2022.Models;
using Academy_2022.Models.Dto;

namespace Academy_2022.Repositories
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task<Course> CreateAsync(int authorId, CreateCourseDto courseDto);
        Task<Course?> UpdateAsync(UpdateCourseDto courseDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsIds(List<int> ids);
    }
}
