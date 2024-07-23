using Domain.Models;

namespace DAL.Repositories.Interfaces
{
    /// <summary>
    /// The awards repository interface.
    /// </summary>
    public interface IAwardsRepository
    {
        /// <summary>Creates the award asynchronous.</summary>
        /// <param name="award">The award.</param>
        Task CreateAwardAsync(Award award);

        /// <summary>Gets the awards by type asynchronous.</summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The collection of awards.
        /// </returns>
        Task<IEnumerable<Award>> GetAwardsByTypeAsync(string type);

        /// <summary>Gets the total award amount by date asynchronous.</summary>
        /// <param name="date">The date.</param>
        Task<decimal> GetTotalAwardAmountByDateAsync(DateTime date);
    }
}
