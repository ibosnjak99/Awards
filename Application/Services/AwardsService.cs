using Application.Dtos;
using Application.Services.Interfaces;
using AutoMapper;
using DAL.Repositories.Interfaces;
using Domain.Enums;
using Domain.Models;

namespace Application.Services
{
    public class AwardsService : IAwardsService
    {
        private readonly IAwardsRepository awardsRepository;
        private readonly IMapper mapper;

        public AwardsService(IAwardsRepository awardsRepository, IMapper mapper)
        {
            this.awardsRepository = awardsRepository;
            this.mapper = mapper;
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

        /// <summary>Gets the total award amount by date asynchronous.</summary>
        /// <param name="date">The date.</param>
        public async Task<decimal> GetTotalAwardAmountByDateAsync(DateTime date)
        {
            return await awardsRepository.GetTotalAwardAmountByDateAsync(date);
        }
    }
}
