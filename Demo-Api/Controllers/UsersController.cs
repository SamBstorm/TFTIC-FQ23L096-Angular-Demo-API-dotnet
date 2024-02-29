using Demo_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> _users = new List<User>
        {
            new User(){Id= 1, Email = "samuel.legrain@bstorm.be",Password = "Test1234="},
            new User(){Id= 2, Email = "admin@admin",Password = "Administr@t0r"}

        };

        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            return Ok(_users.ToArray());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            User? user = _users.SingleOrDefault(u => u.Id == id);
            if (user is null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost()]
        public IActionResult Post(User data)
        {
            int idmax = _users.Max(u => u.Id) + 1;
            data.Id = idmax;
            _users.Add(data);
            return Created("/users/"+data.Id, data.Id);
        }

        [HttpPost("CheckLogin")]
        public IActionResult CheckLogin(User data)
        {
            User? user = _users.SingleOrDefault(u => u.Email == data.Email && u.Password == data.Password);
            if (user is null)
                return NotFound();
            return Ok(user);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, User data)
        {
            User? oldUser = _users.SingleOrDefault(u => u.Id == id);
            if (oldUser is null)
                return NotFound();
            oldUser.Email = data.Email;
            oldUser.Password = data.Password;
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User? oldUser = _users.SingleOrDefault(u => u.Id == id);
            if (oldUser is null)
                return NotFound();
            _users.Remove(oldUser);
            return Delete(id);
        }
    }
}
