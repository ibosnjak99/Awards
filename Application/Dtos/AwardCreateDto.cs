using Domain.Enums;

namespace Application.Dtos
{
    /// <summary>
    /// The award create dto.
    /// </summary>
    public class AwardCreateDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the period type.
        /// </summary>
        /// <value>
        /// The period type.
        /// </value>
        public PeriodType PeriodType { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this award is recurring.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this award is recurring; otherwise, <c>false</c>.
        /// </value>
        public bool IsRecurring { get; set; }
    }
}
