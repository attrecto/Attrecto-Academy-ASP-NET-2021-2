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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserCourseRepository _userCourseRepository;

        public UsersController(
            IUserRepository userRepository,
            ICourseRepository courseRepository,
            IUserCourseRepository userCourseRepository)
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _userCourseRepository = userCourseRepository;
        }

        // GET: api/<UsersController>
        [HttpGet]
        [Authorize(Policy = "AdminOnlylPolicy")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IEnumerable<UserDto>> GetAsync()
        {
            var results = new List<UserDto>();
            var users = await _userRepository.GetAllAsync();

            foreach (var user in users)
            {
                results.Add(new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Courses = user.Courses?.Select(x => new MinimalCourseDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        Url = x.Url
                    }).ToList()
                });
            }

            return results;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<UserDto>> GetAsync(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!(User.IsInRole(Role.Admin.ToString().ToLower()) || User.IsInRole(Role.Admin.ToString())) && id != userId)
            {
                return Forbid();
            }

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Courses = user.Courses?.Select(x => new MinimalCourseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Url = x.Url
                }).ToList()
            };
        }

        // POST api/<UsersController>
        [HttpPost]
        [Authorize(Policy = "AdminOnlylPolicy")]
        [SwaggerResponse(201, "Created")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<ActionResult<UserDto>> Post([FromBody] CreateUserDto userDto)
        {
            // INPUT VALIDATION
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.CreateAsync(userDto);

            return Created("", new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Courses = user.Courses?.Select(x => new MinimalCourseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Url = x.Url
                }).ToList()
            });
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        [SwaggerOperation("Update user")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult<UserDto>> Put(int id, [FromBody] UpdateUserDto userDto)
        {
            // INPUT VALIDATION
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(userDto.Courses != null && userDto.Courses.Any() && !(await _courseRepository.ExistsIds(userDto.Courses)))
            {
                // Invalid courseId
                return BadRequest();
            }

            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!(User.IsInRole(Role.Admin.ToString().ToLower()) || User.IsInRole(Role.Admin.ToString())) && id != userId)
            {
                return Forbid();
            }

            userDto.Id = id;
            var user = await _userRepository.UpdateAsync(userDto);
            if (user == null)
            {
                return NotFound();
            }

            var userCourses = await _userCourseRepository.GetByUserIdAsync(id);
            // kurzusok hozza adasa
            foreach (var courseId in userDto.Courses)
            {
                if(!userCourses.Any(x => x.CourseId == courseId))
                {
                    await _userCourseRepository.CreateAsync(id, courseId);
                }
            }

            // kurzusok torlese
            foreach (var courseId in userCourses.Select(x => x.CourseId))
            {
                if (!userDto.Courses.Any(x => x == courseId)) 
                {
                    await _userCourseRepository.DeleteAsync(id, courseId);
                }
            }
            
            // courses lista befrissitese miatt lekerjuk ujra
            user = await _userRepository.GetByIdAsync(id);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Courses = user.Courses?.Select(x => new MinimalCourseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Url = x.Url
                }).ToList()
            };
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnlylPolicy")]
        [SwaggerResponse(204, "No Content")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _userRepository.DeleteAsync(id) 
                ? NoContent()
                : NotFound();
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public void Test()
        {
            List<string> names = new List<string> { "Maggie", "Mary", "John", "Rick" };

            /*
            IEnumerable<string> namesQuery = from name in names
                                             where name[0] == 'M'
                                             select name;
            */

            var namesQuery = names.Where(name => name[0] == 'M');

            foreach (var name in namesQuery)
            {
                Console.WriteLine(name);
            }
        }
    }
}
