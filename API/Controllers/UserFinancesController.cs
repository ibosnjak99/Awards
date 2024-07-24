using Application.Dtos;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    /// <summary>
    /// The user finances controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserFinancesController : ControllerBase
    {
        private readonly IUserFinancesService userFinancesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFinancesController"/> class.
        /// </summary>
        /// <param name="userFinancesService">The user finances service.</param>
        public UserFinancesController(IUserFinancesService userFinancesService)
        {
            this.userFinancesService = userFinancesService;
        }

        /// <summary>
        /// Gets the user finance by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user finance.</returns>
        [HttpGet("{userId}")]
        public async Task<UserFinance> GetUserFinance(int userId)
        {
            return await userFinancesService.GetUserFinanceByUserIdAsync(userId);
        }

        /// <summary>
        /// Updates the balance.
        /// </summary>
        /// <param name="userFinanceDto">The user finance dto.</param>
        [HttpPost("update-balance")]
        public async Task UpdateBalance([FromBody] UserFinanceDto userFinanceDto)
        {
            await userFinancesService.UpdateBalanceAsync(userFinanceDto);
        }
    }
}
