using Academy_2022.Models;
using Academy_2022.Models.Dto;
using Academy_2022.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Academy_2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        // GET: api/<CourseController>
        [HttpGet]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IEnumerable<CourseDto>> GetAsync()
        {
            var results = new List<CourseDto>();
            var courses = await _courseRepository.GetAllAsync();

            foreach (var course in courses)
            {
                results.Add(new CourseDto
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    Url = course.Description,
                    Students = course.Students?.Select(x => new MinimalUserDto
                    {
                        Id = x.Id,
                        Email = x.Email,
                        Name = x.Name
                    }).ToList()
                });
            }

            return results;
        }

        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not found")]
        public async Task<ActionResult<CourseDto>> GetAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Url = course.Description,
                Students = course.Students?.Select(x => new MinimalUserDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    Name = x.Name
                }).ToList()
            };
        }

        // POST api/<CourseController>
        [HttpPost]
        [Authorize(Policy = "AdminOnlylPolicy")]
        [SwaggerResponse(201, "Created")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<ActionResult<Course>> Post([FromBody] CreateCourseDto courseDto)
        {
            // INPUT VALIDATION
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var course = await _courseRepository.CreateAsync(userId, courseDto);

            return Created("", new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Url = course.Description,
                Students = course.Students?.Select(x => new MinimalUserDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    Name = x.Name
                }).ToList()
            });
        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnlylPolicy")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<CourseDto>> Put(int id, [FromBody] UpdateCourseDto courseDto)
        {
            // INPUT VALIDATION
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            courseDto.Id = id;
            var course = await _courseRepository.UpdateAsync(courseDto);
            if (course == null)
            {
                return NotFound();
            }

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Url = course.Description,
                Students = course.Students?.Select(x => new MinimalUserDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    Name = x.Name
                }).ToList()
            };
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnlylPolicy")]
        [SwaggerResponse(204, "No content")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _courseRepository.DeleteAsync(id) 
                ? NoContent()
                : NotFound();
        }
    }
}
