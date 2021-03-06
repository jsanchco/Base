﻿namespace SGI.Domain.Profiles
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
                    dest => dest.roleName,
                    opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<UserViewModel, User>()
                 .ForMember(
                    dest => dest.Birthdate,
                    opt => opt.MapFrom(src => src.birthdate.RemoveTime()));

            CreateMap<User, UserPatch>();

            CreateMap<UserPatch, UserViewModel>();
        }
    }
}
