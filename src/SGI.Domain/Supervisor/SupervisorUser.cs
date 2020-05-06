namespace SGI.Domain.Supervisor
{
    #region Using

    using Domain.Helpers;
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
        public bool UserExists(int id)
        {
            return _userRepository.UserExists(id);
        }

        public UserViewModel GetUserById(int id)
        {
            var result = _mapper.Map<UserViewModel>(_userRepository.GetById(id));

            return result;
        }

        public UserPatch GetUserPatchById(int id)
        {
            var result = _mapper.Map<UserPatch>(_userRepository.GetById(id));

            return result;
        }

        public async Task<QueryResult<UserViewModel>> GetAllUsersAsync(int skip = 0, int take = 0, string orderBy = null, string filter = null)
        {
            var users = _userRepository.GetAll();

            if (!string.IsNullOrEmpty(filter))
                users = users
                    .Where(x =>
                        x.Name.ToLower().RemoveAccentsWithNormalization().Contains(filter) ||
                        x.Surname.ToLower().RemoveAccentsWithNormalization().Contains(filter) ||
                        x.Role.Name.ToLower().RemoveAccentsWithNormalization().Contains(filter));

            if (!string.IsNullOrEmpty(orderBy))
            {
                var orderBySplit = orderBy.Split(" ");
                var ascending = orderBySplit.Length > 1 ? false : true;
                orderBy = orderBySplit[0];

                switch (orderBy)
                {
                    case "name":
                        users = ascending ? users.OrderBy(x => x.Name) : users.OrderByDescending(x => x.Name);
                        break;
                    case "surname":
                        users = ascending ? users.OrderBy(x => x.Surname) : users.OrderByDescending(x => x.Surname);
                        break;
                    case "birthDate":
                        users = ascending ? users.OrderBy(x => x.Birthdate) : users.OrderByDescending(x => x.Birthdate);
                        break;
                }
            }

            var count = users.Count();

            if (skip != 0)
                users = users.Skip(skip);

            if (take != 0)
                users = users.Take(take);

           var result = _mapper.Map<List<UserViewModel>>(await users.ToListAsync());

            return new QueryResult<UserViewModel> { Items = result, Count = count };
        }

        public async Task<QueryResult<UserViewModel>> GetUsersByRolesAsync(IEnumerable<int> ids)
        {
            var result = _mapper.Map<IEnumerable<UserViewModel>>(await _userRepository.GetByRoles(ids).ToListAsync());

            return new QueryResult<UserViewModel> { Items = result.ToList(), Count = result.Count() };
        }

        public async Task<UserViewModel> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<TransactionResult<UserViewModel>> AddUserAsync(UserViewModel newUserViewModel)
        {
            if (newUserViewModel == null)
            {
                throw new ArgumentNullException(nameof(newUserViewModel));
            }

            if (newUserViewModel.roleId == 0)
            {
                throw new ArgumentNullException(nameof(newUserViewModel));
            }

            var user = _mapper.Map<User>(newUserViewModel);
            var result = await _userRepository.AddAsync(user);

            return result.Result && result.Item != null
                ? new TransactionResult<UserViewModel> { Result = result.Result, Item = _mapper.Map<UserViewModel>(result.Item) }
                : new TransactionResult<UserViewModel> { Result = result.Result, Item = null };
        }

        public async Task<bool> UpdateUserAsync(UserViewModel userViewModel)
        {
            if (userViewModel.roleId == 0)
            {
                throw new Exception("El usuario debe tener un Role definido");
            }

            var user = _mapper.Map<User>(userViewModel);
            var userFind = _userRepository.GetById(user.Id);
            user.Password = userFind.Password;

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }
    }
}
