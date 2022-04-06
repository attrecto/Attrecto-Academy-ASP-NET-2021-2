using Academy_2022.Data;
using Academy_2022.Models;
using Academy_2022.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Academy_2022.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CourseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<List<Course>> GetAllAsync()
        {
            return _applicationDbContext.Courses
                .Include(x => x.Students)
                .ToListAsync();
        }

        public Task<Course?> GetByIdAsync(int id)
        {

            return _applicationDbContext.Courses
                .Include(x => x.Students)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> CreateAsync(int authorId, CreateCourseDto courseDto)
        {
            var course = new Models.Course
            {
                Title = courseDto.Title,
                Description = courseDto.Description,
                Url = courseDto.Url,
                AuthorId = authorId,
                Students = new List<User>()
            };

            await _applicationDbContext.AddAsync(course);
            await _applicationDbContext.SaveChangesAsync();

            return course;
        }

        public async Task<Course?> UpdateAsync(UpdateCourseDto courseDto)
        {
            var course = await _applicationDbContext.Courses
                .Include(x => x.Students)
                .FirstOrDefaultAsync(c => c.Id == courseDto.Id);

            if(course == null)
            {
                return null;
            }

            course.Title = courseDto.Title;
            course.Description = courseDto.Description;
            course.Url = courseDto.Url;

            await _applicationDbContext.SaveChangesAsync();

            return course;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _applicationDbContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
            try
            {
                _applicationDbContext.Courses.Remove(course);
                await _applicationDbContext.SaveChangesAsync();
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        public async Task<bool> ExistsIds(List<int> ids)
        {
            return (await _applicationDbContext.Courses.Where(x => ids.Contains(x.Id)).CountAsync()) == ids.Count;
        }
    }
}
