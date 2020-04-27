namespace SGI.Domain.Profiles
{
    #region Using

    using AutoMapper;
    using SGI.Domain.Entities;
    using SGI.Domain.Models;

    #endregion

    public class RolesProfiles : Profile
    {
        public RolesProfiles()
        {
            CreateMap<Role, RoleViewModel>();
        }
    }
}
