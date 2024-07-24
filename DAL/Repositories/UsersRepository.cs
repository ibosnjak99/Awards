using DAL.Repositories.Interfaces;
using Dapper;
using Domain;
using Domain.Models;
using Serilog;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// The users repository class.
    /// </summary>
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbConnection dbConnection;
        private readonly ILogger logger;
        private readonly IUserFinancesRepository userFinancesRepository;

        /// <summary>Initializes a new instance of the <see cref="UsersRepository" /> class.</summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="logger">The logger.</param>
        public UsersRepository(IDbConnection dbConnection, ILogger logger, IUserFinancesRepository userFinancesRepository)
        {
            this.dbConnection = dbConnection;
            this.logger = logger;
            this.userFinancesRepository = userFinancesRepository;
        }

        /// <summary>Registers the user asynchronous.</summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The user object.
        /// </returns>
        public async Task<User> RegisterUserAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                var sql = @"
                INSERT INTO Users (FirstName, LastName, Email, RegistrationDate)
                VALUES (@FirstName, @LastName, @Email, @RegistrationDate);
                SELECT CAST(SCOPE_IDENTITY() as int);";

                var command = new CommandDefinition(sql, new
                {
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.RegistrationDate
                }, cancellationToken: cancellationToken);

                var userPid = await dbConnection.QuerySingleAsync<int>(command);

                user.PID = userPid;
                this.logger.Information("Registered user {@User}", user);

                var userFinance = new UserFinance
                {
                    UserId = user.PID,
                    Balance = 0
                };
                await this.userFinancesRepository.CreateUserFinanceAsync(userFinance, cancellationToken);

                return user;
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
            try
            {
                var sql = "SELECT * FROM Users";
                var users = await dbConnection.QueryAsync<User>(sql);

                this.logger.Information("Retrieved all users");
                return users;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error retrieving all users");
                throw;
            }
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

        /// <summary>Gets the users by registration date asynchronous.</summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// Users registered on specified date.
        /// </returns>
        public async Task<IEnumerable<User>> GetUsersByRegistrationDateAsync(DateTime date)
        {
            try
            {
                var sql = "SELECT * FROM Users WHERE CAST(RegistrationDate AS DATE) = CAST(@Date AS DATE)";
                var users = await dbConnection.QueryAsync<User>(sql, new { Date = date });
                this.logger.Information("Retrieved users with registration date {Date}", date);
                return users;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error retrieving users with registration date {Date}", date);
                throw;
            }
        }
    }
}
