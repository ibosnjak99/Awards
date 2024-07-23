using DAL.Repositories.Interfaces;
using Dapper;
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
                    INSERT INTO Awards (Name, Amount, PeriodType, DateCreated)
                    VALUES (@Name, @Amount, @PeriodType, @DateCreated);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                award.Id = await dbConnection.QuerySingleAsync<int>(sql, new
                {
                    award.Name,
                    award.Amount,
                    PeriodType = (int)award.PeriodType,
                    DateCreated = DateTime.Now
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

        /// <summary>Gets the total award amount by date asynchronous.</summary>
        /// <param name="date">The date.</param>
        public async Task<decimal> GetTotalAwardAmountByDateAsync(DateTime date)
        {
            try
            {
                var sql = @"
                    SELECT SUM(Amount) 
                    FROM Awards
                    WHERE CONVERT(date, DateCreated) = @Date";

                var totalAmount = await dbConnection.QuerySingleAsync<decimal>(sql, new { Date = date });

                this.logger.Information("Retrieved total award amount by date {Date}", date);
                return totalAmount;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Error retrieving total award amount by date {Date}", date);
                throw;
            }
        }
    }
}
