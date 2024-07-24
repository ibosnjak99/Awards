using Application.Dtos;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// The awards controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController
    {
        private readonly IAwardsService awardsService;

        /// <summary>Initializes a new instance of the <see cref="AwardsController" /> class.</summary>
        /// <param name="awardsService">The awards service.</param>
        public AwardsController(IAwardsService awardsService)
        {
            this.awardsService = awardsService;
        }

        /// <summary>Creates the award.</summary>
        /// <param name="awardDto">The award dto.</param>
        [HttpPost]
        public async Task CreateAward([FromBody] AwardCreateDto awardDto)
        {
            await this.awardsService.CreateAwardAsync(awardDto);
        }

        /// <summary>Gets the type of the awards by.</summary>
        /// <param name="type">The type.</param>
        [HttpGet("{type}")]
        public async Task<IEnumerable<AwardDto>> GetAwardsByType(string type)
        {
            return await this.awardsService.GetAwardsByTypeAsync(type);
        }

        /// <summary>Gets the awards by date.</summary>
        /// <param name="date">The date.</param>
        [HttpGet("search")]
        public async Task<decimal> GetAwardsByDate([FromQuery] DateTime date)
        {
            return await this.awardsService.GetTotalAwardAmountByDateAsync(date);
        }
    }
}
