﻿using Application.Dtos;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// The user validator class.
    /// </summary>
    public class UserValidator : AbstractValidator<RegisterUserDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidator" /> class.
        /// </summary>
        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email address is required.").EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
