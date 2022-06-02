using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OnlyFundsAPI.DataAccess.Interfaces;

namespace OnlyFundsAPI.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await userRepository.GetUsers());
        }
    }
}