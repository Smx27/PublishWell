using JPS.Controllers;
using JPS.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishWell.API.Controllers.Users.DTO;

namespace PublishWell.API.Controllers.Users
{
    /// <summary>
    /// Users Controller
    /// </summary>
    /// <seealso cref="BaseAPIController" />
    [Authorize]
    [ApiController()]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UsersController : BaseAPIController
    {
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController" /> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <seealso cref="GetAllUser" />
        public UsersController(IUserRepository userRepository)
        {
         
            _userRepository = userRepository;   
        }
        
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user data.</param>
        /// <returns>The created user.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserDataDTO user)
        {
            await _userRepository.CreateAsync(user);
            // _logger.LogInformation($"User created successfully with UserName: {user.UserName}");
            return Ok(user);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>All users.</returns>
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(users);
        }

        /// <summary>
        /// Gets the user by id.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>The user.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByID(id);
            return Ok(user);
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user data.</param>
        /// <returns>The updated user.</returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDataDTO user)
        {
            await _userRepository.UpdateAsync(user);
            // _logger.LogInformation($"User updated successfully with UserName: {user.UserName}");
            return Ok(user);

        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteAsync(id);
            // _logger.LogInformation($"User deleted successfully with id: {id}");
            return NoContent();
        }

        //TODO : Update User role 
        //TODO : Add User Permissions
        
    }
}