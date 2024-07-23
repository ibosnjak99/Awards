using Application.Dtos;
using AutoMapper;
using Domain;
using Domain.Models;

namespace Application
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>Initializes a new instance of the <see cref="MapperProfile" /> class.</summary>
        public MapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<UserDto, User>();
            CreateMap<AwardCreateDto, Award>();
        }
    }
}
