namespace Application.Dtos
{
    /// <summary>
    /// The user finance dto.
    /// </summary>
    public class UserFinanceDto
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the amount to be updated.
        /// </summary>
        /// <value>
        /// The amount to be added or subtracted from the balance.
        /// </value>
        public decimal Amount { get; set; }
    }
}
