using DAL.Repositories.Interfaces;
using Dapper;
using Domain.Models;
using Serilog;
using System.Data;

namespace DAL.Repositories
{
    /// <summary>
    /// The finances repository.
    /// </summary>
    public class UserFinancesRepository : IUserFinancesRepository
    {
        private readonly IDbConnection dbConnection;
        private readonly ILogger logger;

        public UserFinancesRepository(IDbConnection dbConnection, ILogger logger)
        {
            this.dbConnection = dbConnection;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the user finance by user identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// The user finance.
        /// </returns>
        public async Task<UserFinance> GetUserFinanceByUserIdAsync(int userId)
        {
            try
            {
                var sql = "SELECT * FROM UserFinance WHERE UserId = @UserId";
                var userFinance = await dbConnection.QuerySingleOrDefaultAsync<UserFinance>(sql, new { UserId = userId });

                this.logger.Information("Retrieved user finance for user {UserId}", userId);
                return userFinance;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error retrieving user finance for user {UserId}", userId);
                throw;
            }
        }

        /// <summary>
        /// Updates the balance asynchronous.
        /// </summary>
        /// <param name="userFinance">The user finance.</param>
        public async Task UpdateBalanceAsync(UserFinance userFinance)
        {
            try
            {
                var sql = @"
                    UPDATE UserFinance
                    SET Balance = @Balance
                    WHERE UserId = @UserId";

                await dbConnection.ExecuteAsync(sql, new
                {
                    userFinance.Balance,
                    userFinance.UserId
                });

                this.logger.Information("Updated balance for user {UserId}", userFinance.UserId);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error updating balance for user {UserId}", userFinance.UserId);
                throw;
            }
        }

        /// <summary>
        /// Creates the user finance asynchronous.
        /// </summary>
        /// <param name="userFinance">The user finance.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task CreateUserFinanceAsync(UserFinance userFinance, CancellationToken cancellationToken)
        {
            try
            {
                var sql = @"
                    INSERT INTO UserFinance (UserId, Balance)
                    VALUES (@UserId, @Balance);";

                await dbConnection.ExecuteAsync(sql, new
                {
                    userFinance.UserId,
                    userFinance.Balance
                });

                this.logger.Information("Created user finance for user {UserId}", userFinance.UserId);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error creating user finance for user {UserId}", userFinance.UserId);
                throw;
            }
        }
    }
}