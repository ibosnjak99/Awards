using Application.Dtos;
using DAL.Repositories.Interfaces;
using Services.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using Application.Validators;

namespace Services
{
    /// <summary>
    /// The users service.
    /// </summary>
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;
        private readonly IMapper mapper;
        private readonly IValidator<RegisterUserDto> validator;

        /// <summary>Initializes a new instance of the <see cref="UsersService" /> class.</summary>
        /// <param name="usersRepository">The users repository.</param>
        /// <param name="mapper"></param>
        public UsersService(IUsersRepository usersRepository, IMapper mapper, IValidator<RegisterUserDto> validator)
        {
            this.usersRepository = usersRepository;
            this.mapper = mapper;
            this.validator = validator;
        }

        /// <summary>Registers the user asynchronous.</summary>
        /// <param name="userDto">The user dto.</param>
        /// <returns>
        /// The user dto.
        /// </returns>
        public async Task<UserDto> RegisterUserAsync(RegisterUserDto userDto)
        {
            var validationResult = await this.validator.ValidateAsync(userDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = this.mapper.Map<User>(userDto);
            var createdUser = await this.usersRepository.RegisterUserAsync(user);

            return this.mapper.Map<UserDto>(createdUser);
        }

        /// <summary>
        /// Gets the user by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user object.</returns>
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await this.usersRepository.GetUserByIdAsync(id);
            return this.mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Gets all users asynchronous.
        /// </summary>
        /// <returns>All users.</returns>
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await this.usersRepository.GetAllUsersAsync();
            return users.Select(this.mapper.Map<UserDto>);
        }

        /// <summary>Gets the users by registration date asynchronous.</summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// Users registered on specified date.
        /// </returns>
        public async Task<IEnumerable<UserDto>> GetUsersByRegistrationDateAsync(DateTime date)
        {
            var users = await this.usersRepository.GetUsersByRegistrationDateAsync(date);
            return users.Select(this.mapper.Map<UserDto>);
        }
    }
}
