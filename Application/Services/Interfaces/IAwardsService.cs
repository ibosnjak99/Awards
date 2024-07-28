using Application.Dtos;

namespace Application.Services.Interfaces
{
    /// <summary>
    /// The awards service interface.
    /// </summary>
    public interface IAwardsService
    {
        /// <summary>Creates the award asynchronous.</summary>
        /// <param name="awardDto">The award dto.</param>
        Task CreateAwardAsync(AwardCreateDto awardDto);

        /// <summary>Gets the awards by type asynchronous.</summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The collection of awards.
        /// </returns>
        Task<IEnumerable<AwardDto>> GetAwardsByTypeAsync(string type);

        /// <summary>Gets the awards asynchronous.</summary>
        /// <returns>
        /// The collection of awards.
        /// </returns>
        Task<IEnumerable<AwardDto>> GetAllAwardsAsync();

        /// <summary>Gets the total award amount by date asynchronous.</summary>
        /// <param name="date">The date.</param>
        Task<decimal> GetTotalAwardAmountByDateAsync(DateOnly date);

        /// <summary>Distributes the awards asynchronous.</summary>
        Task DistributeAwardsAsync();

        /// <summary>Gets the latest winner for specific award.</summary>
        /// <param name="awardId">The award identifier.</param>
        /// <returns>
        /// The user dto.
        /// </returns>
        Task<UserDto> GetLatestWinnerForSpecifiedAward(int awardId);
    }
}
