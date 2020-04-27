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
        public bool RoleExists(int id)
        {
            return _roleRepository.RoleExists(id);
        }

        public List<RoleViewModel> GetAllRoles()
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
            if (newRoleViewModel == null)
            {
                throw new ArgumentNullException(nameof(newRoleViewModel));
            }

            var role = _mapper.Map<Role>(newRoleViewModel);
            _roleRepository.Add(role);
            newRoleViewModel = _mapper.Map<RoleViewModel>(role);

            return newRoleViewModel;
        }

        public void UpdateRole(RoleViewModel roleViewModel)
        {
            var role = _mapper.Map<Role>(roleViewModel);

            _roleRepository.Update(role);
        }

        public void DeleteRole(int id)
        {
            _roleRepository.Delete(id);
        }
    }
}
