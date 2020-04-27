namespace SGI.Domain.Supervisor
{
    #region Using

    using Repositories;
    using AutoMapper;
    using System;

    #endregion

    public partial class Supervisor : ISupervisor
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public Supervisor()
        {
        }

        public Supervisor(
            IMapper mapper,
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));

            _roleRepository = roleRepository ??
                throw new ArgumentNullException(nameof(roleRepository));
        }
    }
}
