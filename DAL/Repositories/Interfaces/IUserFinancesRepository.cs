using Domain.Models;

namespace DAL.Repositories.Interfaces
{
    /// <summary>
    /// The user finances repository interface.
    /// </summary>
    public interface IUserFinancesRepository
    {
        /// <summary>
        /// Gets the user finance by user identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user finance.</returns>
        Task<UserFinance> GetUserFinanceByUserIdAsync(int userId);

        /// <summary>
        /// Updates the balance asynchronous.
        /// </summary>
        /// <param name="userFinance">The user finance.</param>
        Task UpdateBalanceAsync(UserFinance userFinance);

        /// <summary>
        /// Creates the user finance asynchronous.
        /// </summary>
        /// <param name="userFinance">The user finance.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task CreateUserFinanceAsync(UserFinance userFinance, CancellationToken cancellationToken);
    }
}