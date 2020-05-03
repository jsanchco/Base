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

        public async Task<QueryResult<RoleViewModel>> GetAllRolesAsync(int skip, int take)
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

        public async Task<RoleViewModel> GetRoleByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            return _mapper.Map<RoleViewModel>(role);
        }

        public async Task<TransactionResult<RoleViewModel>> AddRoleAsync(RoleViewModel newRoleViewModel)
        {
            if (newRoleViewModel == null)
            {
                throw new ArgumentNullException(nameof(newRoleViewModel));
            }

            var role = _mapper.Map<Role>(newRoleViewModel);
            var result = await _roleRepository.AddAsync(role);

            return result.Result && result.Item != null
                ? new TransactionResult<RoleViewModel> { Result = result.Result, Item = _mapper.Map<RoleViewModel>(result.Item) }
                : new TransactionResult<RoleViewModel> { Result = result.Result, Item = null };
        }

        public async Task<bool> UpdateRoleAsync(RoleViewModel roleViewModel)
        {
            var role = _mapper.Map<Role>(roleViewModel);

            return await _roleRepository.UpdateAsync(role);
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            return await _roleRepository.DeleteAsync(id);
        }
    }
}
