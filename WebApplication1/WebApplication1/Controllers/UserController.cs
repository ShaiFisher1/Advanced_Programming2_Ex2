using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //  return View();
        //}

        private static List<User> _users = new List<User>() { new User() { id="Shai Fisher",nickname="shaifisher1",password="shai1234567"} ,
                                                              new User() { id="Mor Siman Tov",nickname="morsimantov5",password="mor1234567"},
                                                              new User() { id="Emma Willson",nickname="emmawillson66",password="emma1234567" },
                                                              new User() { id="Noa Cohen",nickname="noacohen7",password="noa1234567"} };
        [HttpGet]
        public IEnumerable<User> Index() //get all users list
        {
            return _users;
        }


        [HttpGet("{UserName}")]
        public User Details(string? UserName) // get a specific User by Id
        {
            return _users.Where(x => x.id == UserName).FirstOrDefault();
        }

        [HttpPost]
        public void Create([Bind("UserName")] User user) // todo
        {
            _users.Add(user);
        }




    }
}
