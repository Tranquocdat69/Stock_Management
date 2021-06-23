using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stock_Management.Models;
using Stock_Management.Repository;

namespace Stock_Management.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UserAPIController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [HttpGet]
        [Route("get_all_users")]

        public async Task<IActionResult> GetAllUsers()
        {
            List<TbUser> result = await _userRepository.GetAllUsers();
            return Ok(result);
        }

        [HttpPost]
        [Route("create_user")]

        public async Task<IActionResult> Create([FromBody] TbUser data)
        {
            int affectedRow = await _userRepository.CreateUser(data);
            if (affectedRow > 0)
            {
                return Ok(affectedRow);
            }
            else
            {
                return BadRequest("Không thêm được");
            }
        }

    }
}