using Application.Dtos;
using Application.Services.Interfaces;
using AutoMapper;
using DAL.Repositories.Interfaces;
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
            var award = mapper.Map<Award>(awardDto);
            await awardsRepository.CreateAwardAsync(award);
        }

        /// <summary>Gets the awards by type asynchronous.</summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The collection of awards.
        /// </returns>
        public async Task<IEnumerable<Award>> GetAwardsByTypeAsync(string type)
        {
            return await awardsRepository.GetAwardsByTypeAsync(type);
        }

        /// <summary>Gets the total award amount by date asynchronous.</summary>
        /// <param name="date">The date.</param>
        public async Task<decimal> GetTotalAwardAmountByDateAsync(DateTime date)
        {
            return await awardsRepository.GetTotalAwardAmountByDateAsync(date);
        }
    }
}
