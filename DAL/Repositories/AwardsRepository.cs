using DAL.Repositories.Interfaces;
using Dapper;
using Domain;
using Domain.Models;
using Serilog;
using System.Data;

namespace DAL.Repositories
{
    /// <summary>
    /// The awards repository.
    /// </summary>
    public class AwardsRepository : IAwardsRepository
    {
        private readonly IDbConnection dbConnection;
        private readonly ILogger logger;

        public AwardsRepository(IDbConnection dbConnection, ILogger logger)
        {
            this.dbConnection = dbConnection;
            this.logger = logger;
        }

        /// <summary>Creates the award asynchronous.</summary>
        /// <param name="award">The award.</param>
        public async Task CreateAwardAsync(Award award)
        {
            try
            {
                var sql = @"
                    INSERT INTO Awards (Name, Amount, PeriodType, StartDate, EndDate)
                    VALUES (@Name, @Amount, @PeriodType, @StartDate, @EndDate);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                award.Id = await dbConnection.QuerySingleAsync<int>(sql, new
                {
                    award.Name,
                    award.Amount,
                    PeriodType = (int)award.PeriodType,
                    award.StartDate,
                    award.EndDate
                });

                this.logger.Information("Created award {@Award}", award);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error creating award {@Award}", award);
                throw;
            }
        }

        /// <summary>Gets the awards by type asynchronous.</summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The collection of awards.
        /// </returns>
        public async Task<IEnumerable<Award>> GetAwardsByTypeAsync(string type)
        {
            try
            {
                var sql = "SELECT * FROM Awards WHERE PeriodType = @Type";
                var awards = await dbConnection.QueryAsync<Award>(sql, new { Type = type });

                this.logger.Information("Retrieved awards with type {Type}", type);
                return awards;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error retrieving awards with type {Type}", type);
                throw;
            }
        }

        /// <summary>Gets the awards asynchronous.</summary>
        /// <returns>
        /// The collection of awards.
        /// </returns>
        public async Task<IEnumerable<Award>> GetAllAwardsAsync()
        {
            try
            {
                var sql = "SELECT * FROM Awards";
                var awards = await dbConnection.QueryAsync<Award>(sql);

                this.logger.Information("Retrieved awards");
                return awards;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error retrieving awards");
                throw;
            }
        }

        /// <summary>Gets the total award amount by date asynchronous.</summary>
        /// <param name="date">The date.</param>
        public async Task<decimal> GetTotalAwardAmountByDateAsync(DateOnly date)
        {
            try
            {
                const string sql = @"
                    SELECT COALESCE(SUM(a.Amount), 0) AS TotalAmount
                    FROM Winners w
                    INNER JOIN Awards a ON w.AwardId = a.Id
                    WHERE CONVERT(date, w.DateTimeAwarded) = @Date";

                var totalAmount = await dbConnection.QuerySingleAsync<decimal>(sql, new { Date = date.ToString("yyyy-MM-dd") });

                this.logger.Information("Retrieved total award amount by date {Date}", date);
                return totalAmount;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error retrieving total award amount by date {Date}", date);
                throw;
            }
        }

        /// <summary>
        /// Sets the award to finished asynchronous.
        /// </summary>
        /// <param name="awardId">The award identifier.</param>
        public async Task SetAwardToFinishedAsync(int awardId)
        {
            try
            {
                var sql = @"
                    UPDATE Awards
                    SET IsFinished = 1
                    WHERE Id = @AwardId";

                await dbConnection.ExecuteAsync(sql, new { AwardId = awardId });
                this.logger.Information("Set award with id {AwardId} to finished", awardId);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error setting award with id {AwardId} to finished", awardId);
                throw;
            }
        }

        /// <summary>Gets the latest winner for specific award.</summary>
        /// <param name="awardId">The award identifier.</param>
        /// <returns>The user.</returns>
        public async Task<User> GetLatestWinnerForSpecifiedAward(int awardId)
        {
            try
            {
                var sql = @"
                    SELECT TOP 1 u.*
                    FROM Winners w
                    INNER JOIN Users u ON w.UserId = u.PID
                    WHERE w.AwardId = @AwardId
                    ORDER BY w.DateTimeAwarded DESC";

                var user = await dbConnection.QuerySingleOrDefaultAsync<User>(sql, new { AwardId = awardId });

                if (user != null)
                {
                    this.logger.Information("Retrieved latest winner for award with id {AwardId}", awardId);
                }
                else
                {
                    this.logger.Information("No winners found for award with id {AwardId}", awardId);
                }

                return user;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error retrieving latest winner for award with id {AwardId}", awardId);
                throw;
            }
        }
    }
}
