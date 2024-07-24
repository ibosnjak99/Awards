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
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// The registered user dto.
        /// </returns>
        Task<UserDto> RegisterUserAsync(RegisterUserDto userDto, CancellationToken cancellationToken);

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

        /// <summary>Gets the users by registration date asynchronous.</summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// Users registered on specified date.
        /// </returns>
        Task<IEnumerable<UserDto>> GetUsersByRegistrationDateAsync(DateTime date);
    }
}