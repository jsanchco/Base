namespace SGI.Domain.Supervisor
{
    #region Using

    using Entities;
    using Microsoft.EntityFrameworkCore;
    using SGI.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    public partial class Supervisor
    {
        public bool RoleExists(int id)
        {
            return _roleRepository.RoleExists(id);
        }

        public async Task<QueryResult<RoleViewModel>> GetAllRoles(int skip, int take)
        {
            var roles = _roleRepository.GetAll();
            var count = roles.Count();
            if (skip != 0)
                roles = roles.Skip(skip);

            if (take != 0)
                roles = roles.Take(take);

            var result = _mapper.Map<List<RoleViewModel>>(await roles.ToListAsync());

            return new QueryResult<RoleViewModel> { Items = result, Count= count};
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
