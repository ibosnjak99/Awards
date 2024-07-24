using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
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
        /// <param name="cancellationToken"></param>
        /// <returns>The registered user dto.</returns>
        [HttpPost]
        public async Task<UserDto> RegisterUser([FromBody] RegisterUserDto userDto, CancellationToken cancellationToken)
        {
            return await usersService.RegisterUserAsync(userDto, cancellationToken);
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

        /// <summary>Gets users by registration date asynchronous.</summary>
        /// <param name="date">The date.</param>
        /// <returns>Users registered on specified date.</returns>
        [HttpGet("date")]
        public async Task<IEnumerable<UserDto>> GetUsersByRegistrationDateAsync([FromQuery] DateTime date)
        {
            return await this.usersService.GetUsersByRegistrationDateAsync(date);
        }
    }
}