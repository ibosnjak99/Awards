using Application.Dtos;
using Application.Services.Interfaces;
using AutoMapper;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Domain.Enums;
using Domain.Models;
using Serilog;

namespace Application.Services
{
    public class AwardsService : IAwardsService
    {
        private readonly IAwardsRepository awardsRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IUserFinancesRepository userFinancesRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public AwardsService(
            IAwardsRepository awardsRepository,
            IUsersRepository usersRepository,
            IUserFinancesRepository userFinancesRepository,
            IMapper mapper,
            ILogger logger)
        {
            this.awardsRepository = awardsRepository;
            this.usersRepository = usersRepository;
            this.userFinancesRepository = userFinancesRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>Creates the award asynchronous.</summary>
        /// <param name="awardDto">The award dto.</param>
        public async Task CreateAwardAsync(AwardCreateDto awardDto)
        {
            var award = this.mapper.Map<Award>(awardDto);

            switch (award.PeriodType)
            {
                case PeriodType.Hourly:
                    award.EndDate = award.StartDate.AddHours(1);
                    break;
                case PeriodType.Daily:
                    award.EndDate = award.StartDate.AddDays(1);
                    break;
                case PeriodType.Weekly:
                    award.EndDate = award.StartDate.AddDays(7);
                    break;
                case PeriodType.Monthly:
                    award.EndDate = award.StartDate.AddMonths(1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            await awardsRepository.CreateAwardAsync(award);
        }

        /// <summary>Gets the awards by type asynchronous.</summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The collection of awards.
        /// </returns>
        public async Task<IEnumerable<AwardDto>> GetAwardsByTypeAsync(string type)
        {
            var awards = await awardsRepository.GetAwardsByTypeAsync(type);
            return awards.Select(this.mapper.Map<AwardDto>);
        }

        /// <summary>
        /// Gets the awards asynchronous.
        /// </summary>
        /// <returns>
        /// The collection of awards.
        /// </returns>
        public async Task<IEnumerable<AwardDto>> GetAllAwardsAsync()
        {
            var awards = await awardsRepository.GetAllAwardsAsync();
            return awards.Select(this.mapper.Map<AwardDto>);
        }

        /// <summary>Gets the total award amount by date asynchronous.</summary>
        /// <param name="date">The date.</param>
        public async Task<decimal> GetTotalAwardAmountByDateAsync(DateTime date)
        {
            return await awardsRepository.GetTotalAwardAmountByDateAsync(date);
        }

        /// <summary>Distributes the awards asynchronous.</summary>
        public async Task DistributeAwardsAsync()
        {
            var awards = await this.awardsRepository.GetAllAwardsAsync();
            var currentTime = DateTime.Now;

            foreach (var award in awards)
            {
                if (award.IsFinished) continue;

                DateTime nextDistributionDate = award.StartDate;
                bool isTimeToDistribute = false;

                switch (award.PeriodType)
                {
                    case PeriodType.Hourly:
                        nextDistributionDate = award.StartDate.AddHours(1);
                        if (currentTime >= nextDistributionDate)
                        {
                            isTimeToDistribute = true;
                        }
                        break;
                    case PeriodType.Daily:
                        nextDistributionDate = award.StartDate.AddDays(1);
                        if (currentTime >= nextDistributionDate)
                        {
                            isTimeToDistribute = true;
                        }
                        break;
                    case PeriodType.Weekly:
                        nextDistributionDate = award.StartDate.AddDays(7);
                        if (currentTime >= nextDistributionDate)
                        {
                            isTimeToDistribute = true;
                        }
                        break;
                    case PeriodType.Monthly:
                        nextDistributionDate = award.StartDate.AddMonths(1);
                        if (currentTime >= nextDistributionDate)
                        {
                            isTimeToDistribute = true;
                        }
                        break;
                }

                if (isTimeToDistribute)
                {
                    var users = await this.usersRepository.GetAllUsersAsync();
                    var randomUser = users.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                    if (randomUser != null)
                    {
                        var randomUserFinance = await this.userFinancesRepository.GetUserFinanceByUserIdAsync(randomUser.PID);
                        if (randomUserFinance != null)
                        {
                            randomUserFinance.Balance += award.Amount;
                            await userFinancesRepository.UpdateBalanceAsync(randomUserFinance);
                        }
                    }

                    if (!award.IsRecurring)
                    {
                        award.IsFinished = true;
                        await this.awardsRepository.SetAwardToFinishedAsync(award.Id);
                    }

                    this.logger.Information("Distributed {Amount} award to user {UserId}", award.Amount, randomUser?.PID);
                }
            }
        }
    }
}
