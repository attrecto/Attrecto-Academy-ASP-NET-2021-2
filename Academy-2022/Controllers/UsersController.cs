using Academy_2022.Models;
using Academy_2022.Models.Dto;
using Academy_2022.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Academy_2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController()
        {
            _userRepository = new UserRepository();
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userRepository.GetAll();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] UserDto userDto)
        {
            // INPUT VALIDATION
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.Create(userDto);

            return Created("", user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] UserDto userDto)
        {
            // INPUT VALIDATION
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.Update(id, userDto);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return _userRepository.Delete(id) 
                ? NoContent()
                : NotFound();
        }

        [HttpGet("test")]
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
