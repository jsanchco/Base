namespace SGI.Domain.Supervisor
{
    #region Using

    using Domain.Helpers;
    using Entities;
    using SGI.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public partial class Supervisor
    {
        public bool UserExists(int id)
        {
            return _userRepository.UserExists(id);
        }

        public QueryResult<UserViewModel> GetAllUsers(int skip = 0, int take = 0, string orderBy = null, string filter = null)
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

           var result = _mapper.Map<IEnumerable<UserViewModel>>(users);

            return new QueryResult<UserViewModel> { Items = result.ToList(), Count = count };
        }

        public UserViewModel GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            return _mapper.Map<UserViewModel>(user);
        }

        public UserViewModel AddUser(UserViewModel newUserViewModel)
        {
            if (newUserViewModel == null)
            {
                throw new ArgumentNullException(nameof(newUserViewModel));
            }

            var user = _mapper.Map<User>(newUserViewModel);
            _userRepository.Add(user);
            newUserViewModel = _mapper.Map<UserViewModel>(user);

            return newUserViewModel;
        }

        public void UpdateUser(UserViewModel userViewModel)
        {
            var user = _mapper.Map<User>(userViewModel);

            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }
    }
}
