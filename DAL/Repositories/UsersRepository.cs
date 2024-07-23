using DAL.Repositories.Interfaces;
using Dapper;
using Domain;
using System.Data;
using System.Data.Common;

namespace DAL
{
    /// <summary>
    /// The users repository class.
    /// </summary>
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbConnection dbConnection;

        public UsersRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        /// <summary>
        /// Registers the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The user object.</returns>
        public async Task<User> RegisterUserAsync(User user)
        {
            var sql = @"
            INSERT INTO Users (FirstName, LastName, RegistrationDate)
            VALUES (@FirstName, @LastName, @RegistrationDate);
            SELECT CAST(SCOPE_IDENTITY() as int);"
            ;

            var userPid = await dbConnection.QuerySingleAsync<int>(sql, new
            {
                user.FirstName,
                user.LastName,
                user.RegistrationDate
            });

            user.PID = userPid;
            return user;
        }

        /// <summary>
        /// Gets all users asynchronous.
        /// </summary>
        /// <returns>All users.</returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var sql = "SELECT * FROM Users";
            return await dbConnection.QueryAsync<User>(sql);
        }

        /// <summary>
        /// Gets the user by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user object.</returns>
        public async Task<User> GetUserByIdAsync(int id)
        {
            var sql = "SELECT * FROM Users WHERE PID = @Id";

            var user = await dbConnection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
            if (user == null)
            {
                throw new KeyNotFoundException($"User with specified id {id} does not exist.");
            }
            return user;
        }
    }
}
