namespace Application.Dtos
{
    /// <summary>
    /// The register user dto.
    /// </summary>
    public class RegisterUserDto
    {
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
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets the date of registration.
        /// </summary>
        /// <value>
        /// The date of registration.
        /// </value>
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
