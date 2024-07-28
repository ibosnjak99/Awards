namespace Domain.Models
{
    /// <summary>
    /// The winner class.
    /// </summary>
    public class Winner
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the award id.
        /// </summary>
        /// <value>
        /// The award id.
        /// </value>
        public int AwardId { get; set; }

        /// <summary>
        /// Gets or sets the date time awarded.
        /// </summary>
        /// <value>
        /// The date time awarded.
        /// </value>
        public DateTime DateTimeAwarded { get; set; }
    }
}
