using Domain.Models;

namespace DAL.Repositories.Interfaces
{
    /// <summary>
    /// The winners repository interface.
    /// </summary>
    public interface IWinnersRepository
    {
        /// <summary>Adds the winner asynchronous.</summary>
        /// <param name="winner">The winner.</param>
        Task AddWinnerAsync(Winner winner);
    }
}
