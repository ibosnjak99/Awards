using System.Data;
using System.Threading.Tasks;
using Dapper;
using DAL.Repositories.Interfaces;
using Domain.Models;

namespace DAL.Repositories
{
    /// <summary>
    /// The winners repository.
    /// </summary>
    public class WinnersRepository : IWinnersRepository
    {
        private readonly IDbConnection dbConnection;

        /// <summary>Initializes a new instance of the <see cref="WinnersRepository" /> class.</summary>
        /// <param name="dbConnection">The database connection.</param>
        public WinnersRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        /// <summary>Adds the winner asynchronous.</summary>
        /// <param name="winner">The winner.</param>
        public async Task AddWinnerAsync(Winner winner)
        {
            const string sql = "INSERT INTO Winners (UserId, AwardId, DateTimeAwarded) VALUES (@UserId, @AwardId, @DateTimeAwarded)";
            await dbConnection.ExecuteAsync(sql, new { winner.UserId, winner.AwardId, winner.DateTimeAwarded });
        }
    }
}
