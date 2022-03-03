using Academy_2022.Models;
using Academy_2022.Models.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Academy_2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // adatbazis
        public static List<User> _users = new List<User>()
        {
                new User()
                {
                    Id = 1,
                            FirstName = "Varga",
                            LastName = "Oresztesz",
                            Age = 30
                        },
                        new User()
                {
                    Id = 2,
                    FirstName = "Kiss",
                    LastName = "Istvan",
                    Age = 10
                }
        };

        // ctor TAB TAB
        public UsersController()
        {

        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _users;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            // fore TAB TAB
            foreach (User user in _users)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }

            return NotFound();
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] UserDto user)
        {
            // INPUT VALIDATION
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _users.Add(new Models.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age
            });

            return Created("", user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] UserDto user)
        {
            // INPUT VALIDATION
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // for TAB TAB
            for (int i = 0; i < _users.Count; i++)
            {
                if(_users[i].Id == id)
                {
                    _users[i].FirstName = user.FirstName;
                    _users[i].LastName = user.LastName;
                    _users[i].Age = user.Age;

                    return Ok(_users[i]);
                }
            }

            return NotFound();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if(_users[i].Id == id)
                {
                    _users.Remove(_users[i]);

                    return NoContent();
                }
            }

            return NotFound();
        }
    }
}
