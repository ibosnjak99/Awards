﻿using Domain.Enums;

namespace Application.Dtos
{
    /// <summary>
    /// The award class.
    /// </summary>
    public class AwardDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

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
        /// Gets or sets the type of the period.
        /// </summary>
        /// <value>
        /// The type of the period.
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
