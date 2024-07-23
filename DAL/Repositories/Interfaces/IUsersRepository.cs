using Domain;

namespace DAL.Repositories.Interfaces
{
    /// <summary>
    /// The users repository interface.
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>Registers the user asynchronous.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The user object.
        /// </returns>
        Task<User> RegisterUserAsync(User user);

        /// <summary>Gets the user by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The user object.
        /// </returns>
        Task<User> GetUserByIdAsync(int id);

        /// <summary>Gets all users asynchronous.</summary>
        /// <returns>
        /// All users.
        /// </returns>
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
