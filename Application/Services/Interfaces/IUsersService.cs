using Application.Dtos;

namespace Services.Interfaces
{
    /// <summary>
    /// The users service interface.
    /// </summary>
    public interface IUsersService
    {
        /// <summary>Registers the user asynchronous.</summary>
        /// <param name="userDto">The user dto.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<UserDto> RegisterUserAsync(RegisterUserDto userDto);

        /// <summary>
        /// Gets the user by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user object.</returns>
        Task<UserDto> GetUserByIdAsync(int id);

        /// <summary>
        /// Gets all users asynchronous.
        /// </summary>
        /// <returns>All users.</returns>
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
