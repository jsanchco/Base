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
        private readonly IAuthenticateRepository _authenticateRepository;

        public Supervisor()
        {
        }

        public Supervisor(
            IMapper mapper,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IAuthenticateRepository authenticateRepository)
        {
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));

            _roleRepository = roleRepository ??
                throw new ArgumentNullException(nameof(roleRepository));

            _authenticateRepository = authenticateRepository ??
                throw new ArgumentNullException(nameof(authenticateRepository));
        }
    }
}
