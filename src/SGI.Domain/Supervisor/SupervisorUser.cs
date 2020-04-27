namespace SGI.Domain.Supervisor
{
    #region Using

    using System.Collections.Generic;
    using Entities;
    using Domain.Helpers;
    using System.Linq;
    using SGI.Domain.Models;

    #endregion

    public partial class Supervisor
    {
        public QueryResult<UserViewModel> GetAllUsers(int skip = 0, int take = 0, string orderBy = null, string filter = null, List<int> roles = null)
        {
            var users = _userRepository.GetAll();

            if (roles != null)
                users = users.Where(x => roles.Contains(x.RoleId));

            if (!string.IsNullOrEmpty(filter))
                users = users
                    .Where(x =>
                        Searcher.RemoveAccentsWithNormalization(x.Name.ToLower()).Contains(filter) ||
                        Searcher.RemoveAccentsWithNormalization(x.Surname?.ToLower()).Contains(filter) ||
                        Searcher.RemoveAccentsWithNormalization(x.Role.Name.ToLower()).Contains(filter));

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

            if (skip != 0)
                users = users.Skip(skip);

            if (take != 0)
                users = users.Take(take);

            var result = _mapper.Map<IEnumerable<UserViewModel>>(users);

            return new QueryResult<UserViewModel> { Count = result.Count(), Items = result.ToList() };
        }

        public UserViewModel GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            return _mapper.Map<UserViewModel>(user);
        }

        public UserViewModel AddUser(UserViewModel newUserViewModel)
        {
            var user = _mapper.Map<User>(newUserViewModel);
            _userRepository.Add(user);

            return newUserViewModel;
        }

        public bool UpdateUser(UserViewModel userViewModel)
        {
            var user = _mapper.Map<User>(userViewModel);

            return _userRepository.Update(user);
        }

        public bool DeleteUser(int id)
        {
            return _userRepository.Delete(id);
        }
    }
}
