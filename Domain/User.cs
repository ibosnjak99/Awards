namespace Domain
{
    /// <summary>
    /// The user class.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the personal id.
        /// </summary>
        /// <value>
        /// The personal id.
        /// </value>
        public int PID { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public required string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public required string LastName { get; set; }

        /// <summary>
        /// Gets or sets the registration date.
        /// </summary>
        /// <value>
        /// The registration date.
        /// </value>
        public DateTime RegistrationDate { get; set; }
    }
}
