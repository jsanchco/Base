namespace SGI.Domain.Supervisor
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using SGI.Domain.Models;

    #endregion

    public partial class Supervisor
    {
        public List<RoleViewModel> GetAllRole()
        {
            var roles = _roleRepository.GetAll();
            var result = _mapper.Map<IEnumerable<RoleViewModel>>(roles);
            return result.ToList();
        }

        public RoleViewModel GetRoleById(int id)
        {
            var role = _roleRepository.GetById(id);
            return _mapper.Map<RoleViewModel>(role);
        }

        public RoleViewModel AddRole(RoleViewModel newRoleViewModel)
        {
            var role = _mapper.Map<Role>(newRoleViewModel);
            _roleRepository.Add(role);

            return newRoleViewModel;
        }

        public bool UpdateRole(RoleViewModel roleViewModel)
        {
            var role = _mapper.Map<Role>(roleViewModel);

            return _roleRepository.Update(role);
        }

        public bool DeleteRole(int id)
        {
            return _roleRepository.Delete(id);
        }
    }
}
