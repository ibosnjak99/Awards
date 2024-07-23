using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;

namespace API.Controllers
{
    /// <summary>
    /// The users controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController
    {
        private readonly IUsersService usersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="usersService">The users service.</param>
        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        /// <summary>Registers the user.</summary>
        /// <param name="userDto">The user dto.</param>
        /// <returns>The registered user dto.</returns>
        [HttpPost]
        public async Task<UserDto> RegisterUser([FromBody] RegisterUserDto userDto)
        {
            return await usersService.RegisterUserAsync(userDto);
        }

        /// <summary>
        /// Gets the user by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user dto.</returns>
        [HttpGet("{id}")]
        public async Task<UserDto> GetUserById(int id)
        {
            return await this.usersService.GetUserByIdAsync(id);
        }

        /// <summary>
        /// Gets all users asynchronous.
        /// </summary>
        /// <returns>All users.</returns>
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            return await this.usersService.GetAllUsersAsync();
        }
    }
}
