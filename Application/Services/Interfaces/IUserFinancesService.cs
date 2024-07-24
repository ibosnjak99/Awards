using Application.Dtos;
using Domain.Models;

namespace Application.Services.Interfaces
{
    /// <summary>
    /// The user finances service interface.
    /// </summary>
    public interface IUserFinancesService
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
        /// <param name="userFinanceDto">The user finance dto.</param>
        Task UpdateBalanceAsync(UserFinanceDto userFinanceDto);
    }
}
