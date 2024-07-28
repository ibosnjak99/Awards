using DAL.Repositories.Interfaces;
using Dapper;
using Domain;
using Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
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
        private readonly ILogger logger;
        private readonly IMemoryCache cache;

        /// <summary>Initializes a new instance of the <see cref="UsersRepository" /> class.</summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="logger">The logger.</param>
        public UsersRepository(IDbConnection dbConnection, ILogger logger, IMemoryCache cache)
        {
            this.dbConnection = dbConnection;
            this.logger = logger;
            this.cache = cache;
        }

        /// <summary>
        /// Registers the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The user object.</returns>
        public async Task<User> RegisterUserAsync(User user)
        {
            try
            {
                var existingUser = await dbConnection.QuerySingleOrDefaultAsync<User>(
                    "SELECT * FROM Users WHERE Email = @Email", new { user.Email });

                if (existingUser != null)
                {
                    throw new DuplicateNameException($"A user with email {user.Email} already exists.");
                }

                var sql = @"
                    INSERT INTO Users (FirstName, LastName, RegistrationDate, Email)
                    VALUES (@FirstName, @LastName, @RegistrationDate, @Email);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var userPid = await dbConnection.QuerySingleAsync<int>(sql, new
                {
                    user.FirstName,
                    user.LastName,
                    user.RegistrationDate,
                    user.Email
                });

                user.PID = userPid;
                this.logger.Information("Registered user {@User}", user);
                return user;
            }
            catch (DuplicateNameException ex)
            {
                this.logger.Warning(ex, "Duplicate user registration attempt {@User}", user);
                throw;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error registering user {@User}", user);
                throw;
            }
        }

        /// <summary>
        /// Gets all users asynchronous.
        /// </summary>
        /// <returns>All users.</returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var cacheKey = "Users:All";
            if (!cache.TryGetValue(cacheKey, out IEnumerable<User> cachedUsers))
            {
                try
                {
                    var sql = "SELECT * FROM Users";
                    var users = await dbConnection.QueryAsync<User>(sql);
                    cachedUsers = users.ToList();

                    if (cachedUsers.Any())
                    {
                        cache.Set(cacheKey, cachedUsers, TimeSpan.FromDays(1));
                    }

                    this.logger.Information("Retrieved all users");
                }
                catch (Exception ex)
                {
                    this.logger.Error(ex, "Error retrieving all users");
                    throw;
                }
            }

            return cachedUsers;
        }

        /// <summary>
        /// Gets the user by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user object.</returns>
        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT * FROM Users WHERE PID = @Id";

                var user = await dbConnection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with specified id {id} does not exist.");
                }

                this.logger.Information("Retrieved user with id {Id}", id);
                return user;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error retrieving user with id {Id}", id);
                throw;
            }
        }

        /// <summary>Gets the random user asynchronous.</summary>
        /// <returns>
        /// The random user.
        /// </returns>
        public async Task<User> GetRandomUserAsync()
        {
            const string query = "SELECT TOP 1 * FROM Users ORDER BY NEWID()";
            return await this.dbConnection.QuerySingleOrDefaultAsync<User>(query);
        }

        /// <summary>Gets the users by registration date asynchronous.</summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// Users registered on specified date.
        /// </returns>
        public async Task<IEnumerable<User>> GetUsersByRegistrationDateAsync(DateTime date)
        {
            var cacheKey = $"Users:RegistrationDate:{date:yyyy-MM-dd}";
            if (!cache.TryGetValue(cacheKey, out IEnumerable<User> cachedUsers))
            {
                var sql = "SELECT * FROM Users WHERE CAST(RegistrationDate AS DATE) = CAST(@Date AS DATE)";
                var users = await dbConnection.QueryAsync<User>(sql, new { Date = date });
                cachedUsers = users.ToList();

                if (cachedUsers.Any())
                {
                    cache.Set(cacheKey, cachedUsers, TimeSpan.FromDays(1));
                }
            }

            return cachedUsers;
        }
    }
}
