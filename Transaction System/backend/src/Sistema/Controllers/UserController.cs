using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.DataClassification;
using Sistema.Models;
using Sistema.Repositories.Interfaces;

namespace Sistema.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;   
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllUsers()
        {
            List<UserModel> users = await _userRepository.GetAll();

            return Ok(users);
        } 
        
        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserModel>>> GetUserById(int id)
        {
            UserModel user = await _userRepository.GetById(id);
            return Ok(user);
        }
        
        [HttpPost]
        public async Task<ActionResult<UserModel>> AddUser([FromBody] UserModel userModel)
        {
            UserModel user = await _userRepository.AddUser(userModel);

            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<UserModel>> UpdateUser([FromBody] UserModel userModel, int id)
        {
            userModel.Id = id;

            UserModel user = await _userRepository.UpdateUser(userModel, id);

            return Ok(user);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> DeleteUser(int id)
        {
            bool deleted = await _userRepository.DeleteUser(id);

            return Ok(deleted);
        }

    }
}
