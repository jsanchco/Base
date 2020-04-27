namespace SGI.Domain.Profiles
{
    #region Using

    using AutoMapper;
    using SGI.Domain.Entities;
    using SGI.Domain.Models;
    using Helpers;

    #endregion

    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(
                    dest => dest.birthdate,
                    opt => opt.MapFrom(src => src.Birthdate.ToStringEU()))
                .ForMember(
                    dest => dest.roleName,
                    opt => opt.MapFrom(src => src.Role.Name));
        }
    }
}
