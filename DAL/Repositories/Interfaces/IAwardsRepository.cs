using Domain;
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

        /// <summary>Gets the awards asynchronous.</summary>
        /// <returns>
        /// The collection of awards.
        /// </returns>
        Task<IEnumerable<Award>> GetAllAwardsAsync();

        /// <summary>
        /// Gets the total award amount by date asynchronous.
        /// </summary>
        /// <param name="date">The date.</param>
        Task<decimal> GetTotalAwardAmountByDateAsync(DateOnly date);

        /// <summary>
        /// Sets the award to finished asynchronous.
        /// </summary>
        /// <param name="awardId">The award identifier.</param>
        Task SetAwardToFinishedAsync(int awardId);

        /// <summary>Gets the latest winner for specific award.</summary>
        /// <param name="awardId">The award identifier.</param>
        /// <returns>The user.</returns>
        Task<User> GetLatestWinnerForSpecifiedAward(int awardId);
    }
}
