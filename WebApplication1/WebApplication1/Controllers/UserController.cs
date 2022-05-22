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

        private static List<User> _users = new List<User>() { new User() { UserName="Shai Fisher",NickName="shaifisher1",Password="shai1234567", User_Photo="shaiImage"} ,
                                                              new User() { UserName="Mor Siman Tov",NickName="morsimantov5",Password="mor1234567", User_Photo="morImage"},
                                                              new User() { UserName="Emma Willson",NickName="emmawillson66",Password="emma1234567", User_Photo="emmaImage" },
                                                              new User() { UserName="Noa Cohen",NickName="noacohen7",Password="noa1234567", User_Photo="noaImage"} };
        [HttpGet]
        public IEnumerable<User> Index() //get all users list
        {
            return _users;
        }


        [HttpGet("{UserName}")]
        public User Details(string? UserName) // get a specific User by Id
        {
            return _users.Where(x => x.UserName == UserName).FirstOrDefault();
        }

        [HttpPost]
        public void Create([Bind("Body")] User user) // todo
        {
            _users.Add(user);
        }




    }
}
