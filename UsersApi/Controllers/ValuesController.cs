//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Data;

//namespace UsersApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ValuesController : ControllerBase
//    {
//        [Authorize]
//        [Route("getlogin")]
//        public IActionResult GetLogin()
//        {
//            return Ok($"Ваш логин: {User.Identity.Name}");
//        }

//        [Authorize(Roles = "admin")]
//        [Route("getrole")]
//        public IActionResult GetRole()
//        {
//            return Ok("Ваша роль: администратор");
//        }
//    }
//}
