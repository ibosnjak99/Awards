using Application.Dtos;
using Application.Services.Interfaces;
using AutoMapper;
using DAL.Repositories.Interfaces;
using Domain.Models;

namespace Application.Services
{
    /// <summary>
    /// The user finances service.
    /// </summary>
    public class UserFinancesService : IUserFinancesService
    {
        private readonly IUserFinancesRepository userFinancesRepository;
        private readonly IMapper mapper;

        /// <summary>Initializes a new instance of the <see cref="UserFinancesService" /> class.</summary>
        /// <param name="userFinancesRepository">The user finances repository.</param>
        /// <param name="mapper">The mapper.</param>
        public UserFinancesService(IUserFinancesRepository userFinancesRepository, IMapper mapper)
        {
            this.userFinancesRepository = userFinancesRepository;
            this.mapper = mapper;
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
            return await userFinancesRepository.GetUserFinanceByUserIdAsync(userId);
        }

        /// <summary>
        /// Updates the balance asynchronous.
        /// </summary>
        /// <param name="userFinanceDto">The user finance dto.</param>
        public async Task UpdateBalanceAsync(UserFinanceDto userFinanceDto)
        {
            var userFinance = await userFinancesRepository.GetUserFinanceByUserIdAsync(userFinanceDto.UserId);

            userFinance.Balance += userFinanceDto.Amount;
            await userFinancesRepository.UpdateBalanceAsync(userFinance);
        }
    }
}
